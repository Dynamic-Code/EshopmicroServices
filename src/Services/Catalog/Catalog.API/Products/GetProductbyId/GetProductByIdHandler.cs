
namespace Catalog.API.Products.GetProductbyId
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentOperations session, ILogger<GetProductByIdQuery> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

            var product = await session.LoadAsync<Product>(query.id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(product);
        }
    }
}
