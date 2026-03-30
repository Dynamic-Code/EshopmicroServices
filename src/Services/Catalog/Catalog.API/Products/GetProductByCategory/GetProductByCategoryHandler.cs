namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetproductByCategoryResult>;
    public record GetproductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductByCategoryQuery, GetproductByCategoryResult>
    {
        public async Task<GetproductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {

            var result = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category)).ToListAsync(cancellationToken);
            return new GetproductByCategoryResult(result);
        }
    }

}
