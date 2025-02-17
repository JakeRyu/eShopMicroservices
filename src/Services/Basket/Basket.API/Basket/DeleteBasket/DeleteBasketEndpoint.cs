namespace Basket.API.Basket.DeleteBasket;

// public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{userName}", async (string userName, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteBasketCommand(userName);
            
            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete a basket")
        .WithDescription("Delete a basket by user name");
    }
}