namespace Ordering.Domain.Models;

public class Customer
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}