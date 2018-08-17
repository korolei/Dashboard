using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OfaSoaWithStsServer.Data;
using OfaSoaWithStsServer.Models;
using OfaSoaWithStsServer.Services.Certificate;
using Serilog;

namespace OfaSoaWithStsServer
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
				
            Environment = env;

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //1. Setup Certificate store
            var cert = SetupCertStore();

            //2. Setup CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", o => o
                    .WithOrigins(Configuration["ClientAddress"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });

            //3. Add database
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //4. Add Identity Server
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddSigningCredential(cert)
                .AddConfigurationStore(options =>
                { 
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)); 
                })
                .AddOperationalStore(options =>
                { 
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)); 
     
                    // this enables automatic token cleanup. this is optional. 
                    //options.EnableTokenCleanup = true; 
                    //options.TokenCleanupInterval = 30; 
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService<ApplicationUser>>();

            //from command line run this:
            //dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o
            //Migrations/IdentityServer/PersistedGrantDb

            //dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o
            //Migrations/IdentityServer/ConfigurationDb

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //5. Setup IIS options
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IAntiforgery antiforgery)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //IdentityServerDatabaseInitialization.InitializeDbData(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Security configurations
            app.UseCors("AllowSpecificOrigins");

            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                //.ScriptSources(s => s.Self()).ScriptSources(s => s.UnsafeEval())
                //.StyleSources(s => s.UnsafeInline())
            );
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseXfo(xfo => xfo.Deny());
            app.UseXContentTypeOptions();
            app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseAuthentication();
 
            app.Use(async (context, next) =>
            {
                string path = context.Request.Path.Value;
                if (path != null && !path.ToLower().Contains("/api"))
                {
                    // XSRF-TOKEN used by angular in the $http if provided
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false, Secure = true });
                }
                await next();
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }        
        
        private X509Certificate2 SetupCertStore()
        {
            var useLocalCertStore = Convert.ToBoolean(Configuration["UseLocalCertStore"]);
            var certificateThumbprint = Configuration["CertificateThumbprint"];

            X509Certificate2 cert;

            if (Environment.IsProduction())
            {
                if (useLocalCertStore)
                {
                    using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
                    {
                        store.Open(OpenFlags.ReadOnly);
                        var certs = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
                        cert = certs[0];
                        store.Close();
                    }
                }
                else
                {
                    // Azure deployment, will be used if deployed to Azure
                    var vaultConfigSection = Configuration.GetSection("Vault");
                    var keyVaultService = new KeyVaultCertificateService(vaultConfigSection["Url"], vaultConfigSection["ClientId"], vaultConfigSection["ClientSecret"]);
                    cert = keyVaultService.GetCertificateFromKeyVault(vaultConfigSection["CertificateName"]);
                }
            }
            else
            {
                cert = new X509Certificate2(Path.Combine(Environment.ContentRootPath, "OfaSoaStsServer.pfx"), "");
            }

            return cert;
        }
    }
}
