namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogInvalidOperationException : InvalidOperationException
{
    public CatalogInvalidOperationException(string operationName, string reason)
        : base($"The {operationName} cannot be completed because {reason}") { }
    public CatalogInvalidOperationException(string operationName, string reason, Exception? exception)
        : base($"The {operationName} cannot be completed because {reason}", exception) { }
}