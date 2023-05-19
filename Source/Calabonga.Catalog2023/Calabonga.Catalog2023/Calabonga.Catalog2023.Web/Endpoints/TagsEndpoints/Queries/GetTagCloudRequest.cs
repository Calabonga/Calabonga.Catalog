using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.TagsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Catalog2023.Web.Endpoints.TagsEndpoints.Queries;

public record GetTagCloudRequest : IRequest<OperationResult<IEnumerable<TagCloud>>>;

public class GetTagCloudRequestHandler : IRequestHandler<GetTagCloudRequest, OperationResult<IEnumerable<TagCloud>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTagCloudRequestHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<OperationResult<IEnumerable<TagCloud>>> Handle(
        GetTagCloudRequest request,
        CancellationToken cancellationToken)
    {
        const int clusterCount = 10;
        var tags = await _unitOfWork.GetRepository<Tag>()
            .GetAll(
                disableTracking: true,
                include: i => i.Include(x => x.Products!),
                ignoreAutoIncludes: true
            )
            .Distinct()
            .ToListAsync(cancellationToken: cancellationToken);

        var tagsClouds = tags.ToList()
            .Select(x => new { x.Id, x.Name, Total = x.Products!.Count })
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

        return OperationResult.CreateResult<IEnumerable<TagCloud>>(result);
    }
}