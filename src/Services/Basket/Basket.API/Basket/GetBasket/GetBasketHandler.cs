namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{

    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // dodo: get basket form database

        return new GetBasketResult(new ShoppingCart("new"));
    }
}
