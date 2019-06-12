using AutoMapper;
using Calabonga.Catalog.Models.Base;

namespace Calabonga.Catalog.Web.Extensions
{
    /// <summary>
    /// Custom extension
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static void IgnoreAudit<TSource, TDestination>(this IMappingExpression<TSource, TDestination> source)
            where TDestination: Auditable
        {
            source.ForMember(x => x.CreatedAt, o => o.Ignore());
            source.ForMember(x => x.CreatedBy, o => o.Ignore());
            source.ForMember(x => x.UpdatedAt, o => o.Ignore());
            source.ForMember(x => x.UpdatedBy, o => o.Ignore());
        }
    }
}
