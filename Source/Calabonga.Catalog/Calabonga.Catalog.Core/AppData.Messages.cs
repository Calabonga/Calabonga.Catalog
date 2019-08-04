namespace Calabonga.Catalog.Core
{
    public static partial class AppData
    {
        /// <summary>
        /// Common messages
        /// </summary>
        public static class Messages
        {
            /// <summary>
            /// "User successfully registered"
            /// </summary>
            public static string UserSuccessfullyRegistered => "User successfully registered";

            /// <summary>
            /// "User name is required";
            /// </summary>
            public static string UserNameRequired => "User name is required";
            
            /// <summary>
            ///  "Min value is 1"; 
            /// </summary>
            public static string RatingMinRequired => "Min value is 1"; 
            
            /// <summary>
            /// "Max value is 5";
            /// </summary>
            public static string RatingMaxRequired => "Max value is 5";

            /// <summary>
            ///  "Review content required";
            /// </summary>
            public static string ReviewContentRequired => "Review content required";

            /// <summary>
            /// "Access Denied"
            /// </summary>
            public static string AccessDenied => "Access Denied";

            /// <summary>
            /// "UserName should have at least 5 characters";
            /// </summary>
            public static string UserNameMinLength => "UserName should have at least 5 characters";

            /// <summary>
            ///  "Product identifier is required";
            /// </summary>
            public static string ProductIdentifierRequired => "Product identifier is required";
        }
    }
}
