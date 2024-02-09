using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace snglrtycrvtureofspce.Core.Filters;

/// <inheritdoc />
public class PathPrefixInsertDocumentFilter : IDocumentFilter
{
    private readonly string _pathPrefix;

    /// <summary>
    /// Constructor with params
    /// </summary>
    /// <param name="prefix"></param>
    public PathPrefixInsertDocumentFilter(string prefix)
    {
        _pathPrefix = prefix;
    }

    /// <inheritdoc />
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths.Keys.ToList();
        foreach (var path in paths)
        {
            var pathToChange = swaggerDoc.Paths[path];
            swaggerDoc.Paths.Remove(path);
            swaggerDoc.Paths.Add($"{_pathPrefix}{path}", pathToChange);
        }
    }
}