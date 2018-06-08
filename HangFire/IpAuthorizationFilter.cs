using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Hangfire.Dashboard;
using Microsoft.Owin;

namespace HangFire
{
    /// <summary>
    ///     IpAuthorizationFilter.
    /// </summary>
    public class IpAuthorizationFilter : IAuthorizationFilter
    {
        [SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Local")]
        private string[] AdminIps { get; } = (ConfigurationManager.AppSettings["AdminIps"] ?? "").Split(',');

        [SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Local")]
        private string[] AllowedIps { get; } = { "116.228.10.242" };

        #region IAuthorizationFilter Members

        /// <summary>
        ///     Authorizes the specified owin environment.
        /// </summary>
        /// <param name="owinEnvironment">The owin environment.</param>
        /// <returns>System.Boolean.</returns>
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            // In case you need an OWIN context, use the next line,
            // `OwinContext` class is the part of the `Microsoft.Owin` package.
            OwinContext context = new OwinContext(owinEnvironment);

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return context.Authentication.User.Identity.IsAuthenticated;
            return this.IpIsAuthorized(context);
        }

        #endregion IAuthorizationFilter Members

        private bool IpIsAuthorized(OwinContext context)
        {
            string ip = context.Request.RemoteIpAddress;

            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            if (ip == "127.0.0.1" || ip == "::1" || this.AllowedIps.Contains(ip) || this.AdminIps.Contains(ip))
                return true;

            return ip == context.Request.LocalIpAddress;
        }

        //ConfigManager.AdminIps;
    }
}