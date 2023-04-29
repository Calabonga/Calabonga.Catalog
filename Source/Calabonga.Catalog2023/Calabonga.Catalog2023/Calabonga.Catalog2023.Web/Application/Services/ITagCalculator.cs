using Calabonga.Catalog2023.Domain;

namespace Calabonga.Catalog2023.Web.Application.Services;

public interface ITagCalculator
{
    /// <summary>
    /// Tags processing
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    Task<TagCalculatorResult> ProcessTagsAsync(string[]? tags, Product entity, CancellationToken cancellationToken);
}

/// <summary>
/// Tags calculation result
/// </summary>
public readonly struct TagCalculatorResult
{
    public TagCalculatorResult(string[] toCreate, string[] toDelete)
    {
        ToCreate = toCreate;
        ToDelete = toDelete;
    }

    public TagCalculatorResult(Exception exception)
    {
        Exception = exception;
    }

    public string[]? ToCreate { get; }

    public string[]? ToDelete { get; }

    public Exception? Exception { get; }

    public bool Competed => Exception == null;
}