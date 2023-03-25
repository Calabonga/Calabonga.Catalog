namespace Calabonga.Catalog2023.Web.Exceptions;

public class CatalogAccessDeniedException : Exception
{
    public CatalogAccessDeniedException() : base("Access denied") { }
}