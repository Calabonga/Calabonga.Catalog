using System;
using System.Collections.Generic;
using System.Linq;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Catalog.Web.Infrastructure.Services
{
    /// <summary>
    /// Tag service
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Returns <see cref="ProductTag"/> collection by names of the tags 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="tagsAsString"></param>
        /// <returns></returns>
        IEnumerable<ProductTag> GetTagsFromString(Guid productId, string tagsAsString);

        /// <summary>
        /// Returns string names for tags
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        string[] GetStringFromTags(Guid productId);
    }

    /// <inheritdoc />
    public class TagService : ITagService
    {
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public TagService(IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public IEnumerable<ProductTag> GetTagsFromString(Guid productId, string tagsAsString)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var productTagRepository = _unitOfWork.GetRepository<ProductTag>();
            var tagRepository = _unitOfWork.GetRepository<Tag>();

            var all = tagsAsString.ToLower().Split(";", StringSplitOptions.RemoveEmptyEntries);
            var old = GetStringFromTags(productId);

            // calculate
            var toDelete = old.Except(all);
            var mask = all.Intersect(old);
            var toAddArray = all.Except(mask).ToArray();
            var toDeleteArray = toDelete as string[] ?? toDelete.ToArray();

            // delete operation
            if (toDeleteArray.Any())
            {
                foreach (var name in toDeleteArray)
                {
                    var tag = GetTagByName(name);
                    if (tag == null)
                    {
                        continue;
                    }

                    var map = GetMap(productId, tag.Id);
                    _unitOfWork.GetRepository<ProductTag>().Delete(map);
                    var total = (from p in productRepository.GetAll()
                                 from t in p.ProductTags
                                 where t.TagId == tag.Id
                                 select p).Count();

                    if (total == 1)
                    {
                        tagRepository.Delete(tag);
                    }
                }
            }

            // append operation
            if (toAddArray.Any())
            {
                foreach (var name in toAddArray)
                {
                    var tag = GetTagByName(name);
                    if (tag == null)
                    {
                        var newTag = new Tag{Name = name.Trim().ToLower()};
                        tagRepository.Insert(newTag);
                        var map = new ProductTag
                        {
                            ProductId = productId,
                            TagId = newTag.Id
                        };
                        productTagRepository.Insert(map);
                    }
                    else
                    {
                        var map = new ProductTag
                        {
                            ProductId = productId,
                            TagId = tag.Id
                        };
                        productTagRepository.Insert(map);
                    }
                }
            }

            _unitOfWork.SaveChanges();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                return new List<ProductTag>();
            }

            return productTagRepository.GetAll().Where(x => x.ProductId == productId);
        }

        /// <inheritdoc />
        public string[] GetStringFromTags(Guid productId)
        {
            var product = _unitOfWork.GetRepository<Product>().GetFirstOrDefault(predicate: x => x.Id == productId,
                include: i => i.Include(x => x.ProductTags).ThenInclude(x => x.Tag));

            if (product == null || !product.ProductTags.Any())
            {
                return new string[] { };
            }

            return product.ProductTags.Select(x => x.Tag).Select(x => x.Name.ToLower()).ToArray();
        }

        /// <summary>
        /// Returns Tag by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tag GetTagByName(string name)
        {
            return _unitOfWork.GetRepository<Tag>().GetFirstOrDefault(predicate: x => x.Name.ToLower() == name);
        }

        /// <summary>
        /// Returns Tag by name
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public ProductTag GetMap(Guid productId, Guid tagId)
        {
            return _unitOfWork.GetRepository<ProductTag>().GetFirstOrDefault(predicate: x => x.ProductId == productId && x.TagId == tagId);
        }
    }
}