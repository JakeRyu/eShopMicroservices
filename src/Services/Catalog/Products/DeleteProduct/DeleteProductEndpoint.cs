namespace Catalog.API.Products.DeleteProduct;

// public record DeleteProductRequest(Guid Id);

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            
            var response = result.Adapt<DeleteProductResult>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResult>(200)
        .ProducesProblem(400)
        .WithName("Delete Product")
        .WithDescription("Delete product");
    }
}