namespace Catalog.API.Products.GetProductByCategor;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProducstByCategoryHandler(IDocumentSession session, ILogger<GetProducstByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryHandler");

        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        
        return new GetProductByCategoryResult(products);
    }
}