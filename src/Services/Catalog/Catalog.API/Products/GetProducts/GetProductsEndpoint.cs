namespace Catalog.API.Products.GetProducts
{
    //public record GetProductRequest();
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) => //Used MediatR ISender to send the query and get the result
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProductResponse>(); // then adapt(Map) the result using Mapster

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .WithSummary("Get All Products")
            .WithDescription("Get All Products");
        }
    }
}
