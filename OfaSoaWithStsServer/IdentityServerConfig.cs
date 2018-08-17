﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace OfaSoaWithStsServer
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("scope_used_for_hybrid_flow", "Mvc Hybrid Client"),
                new ApiResource("scope_used_for_ceo_dashboard", "CEO Dashboard Client")
                {
                    ApiSecrets =
                    {
                        new Secret("hybrid_flow_secret".Sha256())
                    }
                },
                new ApiResource("native_api", "Native Client API")
                {
                    ApiSecrets =
                    {
                        new Secret("native_api_secret".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "ceo_dashboard",
                    ClientName = "CEO Dashboard",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientSecrets = { new Secret("hybrid_flow_secret".Sha256()) },

                    RedirectUris = { "https://localhost:44317/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44317/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44317/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile, 
                        //IdentityServerConstants.StandardScopes.Email,  
                        "scope_used_for_ceo_dashboard"
                    },
                    AlwaysSendClientClaims = true
                },// MVC client using hybrid flow
                new Client
                {
                    ClientId = "hybridclient",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("hybrid_flow_secret".Sha256()) },

                    RedirectUris = { "https://localhost:44381/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44381/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44381/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile, 
                        IdentityServerConstants.StandardScopes.OfflineAccess,  
                        "scope_used_for_hybrid_flow"
                    },
                    AlwaysSendClientClaims = true
                },
                new Client
                {
                    ClientId = "native.code",
                    ClientName = "Native Client (Code with PKCE)",

                    RedirectUris = { "http://127.0.0.1:45656" },
                    PostLogoutRedirectUris = { "http://127.0.0.1:45656" },

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowedScopes = { "openid", "profile", "email", "native_api" },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                 }
            };
        }
    }
}