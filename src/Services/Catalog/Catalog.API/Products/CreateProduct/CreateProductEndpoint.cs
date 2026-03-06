namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product", async (CreateProductRequest request, ISender sender) =>
            {
                //MediatR need command obj in order to trigger CH.
                var command = request.Adapt<CreateProductCommand>();  //Created request cmd mapped using Mapster

                var result = await sender.Send(command); // start MediatR andTrigger Handler

                var response = result.Adapt<CreateProductResponse>(); //Created Response and mapped using Mapster

                return Results.Created($"/products/{response.id}", response);
            })
                .WithName("CreateProduct") //Name of the Http Post Method
                .Produces<CreateProductResponse>(StatusCodes.Status201Created) //Response
                .ProducesProblem(StatusCodes.Status400BadRequest) //Error
                .WithSummary("Created Product") // Sumary of the Endpoint
                .WithDescription("Create product"); //Description of the endpoint
        }
    }
}
