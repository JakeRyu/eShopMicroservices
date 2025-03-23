namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketReuest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout",
                async (CheckoutBasketReuest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<CheckoutBasketCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CheckoutBasketResponse>();

                    return Results.Ok(response);
                })
            .WithName("CheckoutBasket")
            .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Checkout Basket")
            .WithDescription("Checkout Basket");
    }
}