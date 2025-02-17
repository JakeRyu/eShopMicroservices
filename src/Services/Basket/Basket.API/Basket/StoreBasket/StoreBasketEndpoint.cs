namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets", async (StoreBasketResult request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            
            var result = await sender.Send(command, cancellationToken);
            
            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/baskets/{response.UserName}", response);
        })
        .WithName("Store basket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store basket")
        .WithDescription("Store basket in database");
    }
}