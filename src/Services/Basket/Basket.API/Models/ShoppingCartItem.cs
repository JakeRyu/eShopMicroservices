﻿namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; } = default!;
    public string Colour { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
}