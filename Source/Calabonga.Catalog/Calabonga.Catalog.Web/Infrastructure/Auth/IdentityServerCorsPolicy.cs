﻿using System.Threading.Tasks;
using IdentityServer4.Services;

namespace Calabonga.Catalog.Web.Infrastructure.Auth
{
    /// <summary>
    /// Customized CORS policy for IdentityServer4
    /// </summary>
    public class IdentityServerCorsPolicy : ICorsPolicyService
    {
        /// <inheritdoc />
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return Task.FromResult(true);
        }

    }
}
