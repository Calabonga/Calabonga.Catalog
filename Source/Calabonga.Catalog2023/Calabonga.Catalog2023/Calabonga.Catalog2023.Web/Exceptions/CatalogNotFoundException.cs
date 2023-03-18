namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogNotFoundException : Exception
{
    public CatalogNotFoundException(string message)
        : base(message){}
    public CatalogNotFoundException(string message, Exception? exception) 
        : base(message, exception){}
}