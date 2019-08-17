using System;
using System.Collections.Generic;
using System.Linq;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Catalog.Web.Infrastructure.Services
{
    /// <summary>
    /// Cloud Service
    /// </summary>
    public class CloudService : ICloudService
    {
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <summary>
        /// // Calabonga: update summary (2019-08-17 03:02 CloudService)
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CloudService(IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// // Calabonga: update summary (2019-08-17 02:56 CloudService)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TagCloud> CreateCloud()
        {
            const int clusterCount = 10;
            var tags = _unitOfWork.GetRepository<Tag>()
                .GetAll()
                .Include(x => x.ProductTags)
                .ThenInclude(x => x.Tag)
                .Distinct().ToList();

            var tagsClouds = tags
                .Select(x => new { x.Id, x.Name, Total = x.ProductTags.Count })
                .Select(x => new TagCloud { Id = x.Id, TotalCount = x.Total, Name = x.Name });

            var tagClouds = tagsClouds as TagCloud[] ?? tagsClouds.ToArray();
            var totalCount = tagClouds.Count();
            var tagsCloud = tagClouds.OrderBy(x => x.TotalCount).ToArray();
            var clusters = new List<List<TagCloud>>();
            if (totalCount > 0)
            {
                var min = tagsCloud.Min(c => c.TotalCount);
                var max = tagsCloud.Max(c => c.TotalCount) + min;
                var completeRange = max - min;
                var groupRange = completeRange / (double)clusterCount;
                var cluster = new List<TagCloud>();
                var currentRange = min + groupRange;
                for (var i = 0; i < totalCount; i++)
                {
                    while (tagsCloud[i].TotalCount > currentRange)
                    {
                        clusters.Add(cluster);
                        cluster = new List<TagCloud>();
                        currentRange += groupRange;
                    }
                    cluster.Add(tagsCloud[i]);
                }
                clusters.Add(cluster);
            }
            var items = new List<TagCloud>();
            foreach (var t in clusters)
            {
                items.AddRange(t
                    .Select(item => new TagCloud
                    {
                        Id = item.Id,
                        Name = item.Name,
                        TotalCount = item.TotalCount
                    }));
            }
            var result = items.OrderBy(x => x.Name);

            return result;
        }
    }
}

