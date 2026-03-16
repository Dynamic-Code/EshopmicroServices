namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetproductByCategoryResult>;
    public record GetproductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) 
        : IQueryHandler<GetProductByCategoryQuery, GetproductByCategoryResult>
    {
        public async Task<GetproductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

            var result = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category)).ToListAsync(cancellationToken);
            return new GetproductByCategoryResult(result);
        }
    }

}
