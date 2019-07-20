using System.Collections.Generic;

namespace Calabonga.Catalog.Core
{
    /// <summary>
    /// Static data container
    /// </summary>
    public static partial class AppData
    {
        /// <summary>
        /// "SystemAdministrator"
        /// </summary>
        public const string SystemAdministratorRoleName = "Administrator";

        /// <summary>
        /// Roles
        /// </summary>
        public static IEnumerable<string> Roles
        {
            get
            {
                yield return SystemAdministratorRoleName;
            }
        }

        /// <summary>
        /// IdentityServer4 path
        /// </summary>
        public const string AuthUrl = "/auth";
    }
}
