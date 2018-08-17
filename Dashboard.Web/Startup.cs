using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dashboard.Common.Configuration;
using Dashboard.Common.Logging;
using Dashboard.Common.Security;
using Dashboard.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Dashboard.Web
{
    public class Startup
    {
        private readonly string _stsServer;
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(config =>
                {
                    config.Path = "appSettings.json";
                    config.ReloadOnChange = true;
                })
                .AddJsonFile(options =>
                    {
                        options.Path = $"appsettings.{env.EnvironmentName}.json";
                        options.ReloadOnChange = true;
                        options.Optional = true;
                    }
                 )
                .AddEnvironmentVariables();

            var configSettings = new AppSettings();
            builder.Build().Bind(configSettings);

            var settingsBuilder = new ConfigurationBuilder()
                .AddCustomConfig(configSettings);
            Configuration = settingsBuilder.Build();

            Log.Logger = ApplicationLogging
                .GetSeriLogger(Configuration.AsEnumerable()
                    .ToDictionary(x => x.Key, x => x.Value));
            _stsServer = "https://localhost:44364";//Configuration["StsServer"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Facade.Configure.ConfigureServices(services);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder => builder
                    .WithOrigins("https://localhost:44364")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            services.AddMvcCore()
                .AddJsonFormatters(options => options.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddMemoryCache();

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = _stsServer;
            //        options.ApiName = "native_api";
            //        options.ApiSecret = "native_api_secret";
            //        options.RequireHttpsMetadata = true;
            //    });

            //services.AddAuthorization(options =>
            //    options.AddPolicy("protectedScope", policy =>
            //    {
            //        policy.RequireClaim("scope", "native_api");
            //    })
            //);

            services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = "Cookies";
                options.SignOutScheme = "OpenIdConnect";
                options.Authority = _stsServer;
                options.RequireHttpsMetadata = false;
                options.ClientId = "ceo_dashboard";
                options.ClientSecret = "hybrid_flow_secret";
                options.ResponseType = "code id_token";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("scope_used_for_ceo_dashboard");
                options.Scope.Add("profile");
                //options.Scope.Add("email");
                options.SaveTokens = true;
                // Set the correct name claim type
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                };
                options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated = tokenValidatedContext =>
                    {
                        var identity = tokenValidatedContext.Principal.Identity as ClaimsIdentity;
                        //var subjectClaim = identity.Claims.FirstOrDefault(x => x.Type == "sub");
                        //var newClaimsIdentity = new ClaimsIdentity(
                        //    tokenValidatedContext.Principal.Identity.AuthenticationType, "given_name", "role");
                        //newClaimsIdentity.AddClaim(subjectClaim);
                        return Task.FromResult(0);
                    },
                    OnUserInformationReceived = onUserInformationReceived =>
                    {
                        onUserInformationReceived.User.Remove("address");
                        return Task.FromResult(0);
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireWindowsProviderPolicy", OfaAuthorizationPolicy.GetRequireWindowsProviderPolicy());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Dashboard API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "DashboardAPI.xml"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            loggerFactory.AddConsole();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseCors("AllowSpecificOrigins");

            if (Debugger.IsAttached)
            {
                var option = new RewriteOptions();
                option.AddRedirect("^$", "swagger");
                app.UseRewriter(option);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        // ensure generic 500 status code on fault.
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            ConfigureAdditionalMiddleware(app);

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseMvc();
        }

        protected virtual void ConfigureAdditionalMiddleware(IApplicationBuilder app)
        {
            Mapper.Initialize(x =>
            {
                AutoMapperConfiguration.ConfigAction.Invoke(x);
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dashboard API v1");
            });

            //Registered before static files to always set header
            app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.Deny());

            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                .StyleSources(s => s.Self())
                .StyleSources(s => s.UnsafeInline())
                .FontSources(s => s.Self())
                .FormActions(s => s.Self())
                .FrameAncestors(s => s.Self())
                .ImageSources(s => s.Self())
                .ScriptSources(s => s.Self())
            );

        }
    }

    public class TestServerStartup : Startup
    {
        protected override void ConfigureAdditionalMiddleware(IApplicationBuilder app)
        {
            app.UseMiddleware<TestAuthenticationMiddleware>();
        }

        public TestServerStartup(IHostingEnvironment env) : base(env)
        {
        }
    }
    public class TestAuthenticationMiddleware
    {
        public const string TestingCookieAuthentication = "TestCookieAuthentication";
        public const string TestingHeader = "X-Integration-Testing";
        public const string TestingHeaderValue = "abcde-12345";

        private readonly RequestDelegate _next;

        public TestAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains(TestingHeader) &&
                context.Request.Headers[TestingHeader].First().Equals(TestingHeaderValue))
            {
                if (context.Request.Headers.Keys.Contains("my-name"))
                {
                    var name = 
                        context.Request.Headers["my-name"].First();
                    var id =
                        context.Request.Headers.Keys.Contains("my-id")
                            ? context.Request.Headers["my-id"].First() : "";
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.NameIdentifier, id),
                    }, TestingCookieAuthentication);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    context.User = claimsPrincipal;
                }
            }

            await _next(context);
        }
    }
}
