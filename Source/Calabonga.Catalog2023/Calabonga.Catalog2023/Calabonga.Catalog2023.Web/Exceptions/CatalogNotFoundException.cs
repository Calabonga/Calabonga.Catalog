namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogNotFoundException : Exception
{
    public CatalogNotFoundException(string entityName, string id)
        : base($"Item {entityName} with {id} not found") { }
    public CatalogNotFoundException(string entityName, string id, Exception? exception)
        : base($"Item {entityName} with {id} not found", exception) { }
}