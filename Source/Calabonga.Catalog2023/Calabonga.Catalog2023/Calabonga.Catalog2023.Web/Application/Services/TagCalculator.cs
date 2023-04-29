using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Infrastructure;
using Calabonga.UnitOfWork;

namespace Calabonga.Catalog2023.Web.Application.Services;

public class TagCalculator : ITagCalculator
{
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

    public TagCalculator(
        IUnitOfWork<ApplicationDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Tags processing
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public async Task<TagCalculatorResult> ProcessTagsAsync(
        string[]? tags,
        Product? entity,
        CancellationToken cancellationToken)
    {
        if (tags == null)
        {
            return new TagCalculatorResult(new ArgumentNullException(nameof(tags)));
        }

        if (entity == null)
        {
            return new TagCalculatorResult(new ArgumentNullException(nameof(entity)));
        }

        var tagRepository = _unitOfWork.GetRepository<Tag>();

        var current = tags.ToArray();
        var old = tagRepository.GetAll(selector: x => x.Name.ToLower(), false).ToArray();

        var mask = current.Intersect(old);
        var toDelete = old.Except(current).ToArray();
        var toCreate = current.Except(mask).ToArray();

        if (toDelete.Any())
        {
            foreach (var name in toDelete)
            {
                var tag = await tagRepository.GetFirstOrDefaultAsync(
                    predicate: x => x.Name.ToLower() == name,
                    disableTracking: false);

                if (tag == null)
                {
                    continue;
                }

                var postRepository = _unitOfWork.GetRepository<Product>();
                var used = postRepository.GetAll(predicate: x => x.Tags!.Select(t => t.Name).Contains(tag.Name))
                    .ToArray();
                if (used.Length == 1)
                {
                    tagRepository.Delete(tag);
                }
            }
        }

        if (entity.Tags == null || !entity.Tags.Any())
        {
            entity.Tags = new List<Tag>();
        }

        foreach (var name in toCreate)
        {
            var tag = await tagRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == name);
            if (tag == null)
            {
                var t = new Tag { Name = name.Trim().ToLower() };
                await tagRepository.InsertAsync(t, cancellationToken);
                entity.Tags!.Add(t);
            }
            else
            {
                entity.Tags!.Add(tag);
            }
        }

        return new TagCalculatorResult(toCreate, toDelete);
    }
}