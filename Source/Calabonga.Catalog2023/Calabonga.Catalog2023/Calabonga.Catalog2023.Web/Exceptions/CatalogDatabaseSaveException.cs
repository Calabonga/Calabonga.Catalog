namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogDatabaseSaveException : Exception
{
    public CatalogDatabaseSaveException(string? message)
        : base(message) { }

    public CatalogDatabaseSaveException(string? message, Exception? exception)
        : base(message, exception) { }
}