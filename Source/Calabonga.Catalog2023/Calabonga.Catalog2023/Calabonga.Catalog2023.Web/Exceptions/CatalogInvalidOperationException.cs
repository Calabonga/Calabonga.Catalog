namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogInvalidOperationException : InvalidOperationException
{
    public CatalogInvalidOperationException(string operationName, string reason)
        : base($"The {operationName} cannot be executed because {reason}") { }
    public CatalogInvalidOperationException(string operationName, string reason, Exception? exception)
        : base($"The {operationName} cannot be executed because {reason}", exception) { }
}