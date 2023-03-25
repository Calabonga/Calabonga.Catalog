namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogDatabaseSaveException : Exception
{
    public CatalogDatabaseSaveException(string entityName)
        : base($"Saving data error for entity name {entityName}") { }

    public CatalogDatabaseSaveException(string entityName, Exception? exception)
        : base($"Saving data error for entity name {entityName}", exception) { }
}