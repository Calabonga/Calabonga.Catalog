namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogArgumentNullException : ArgumentNullException
{
    public CatalogArgumentNullException(string argumentName)
        : base($"The {argumentName} is NULL") { }
    public CatalogArgumentNullException(string argumentName, Exception? exception)
        : base($"The {argumentName} is NULL", exception) { }
}