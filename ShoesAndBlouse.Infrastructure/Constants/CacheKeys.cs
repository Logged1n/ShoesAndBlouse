namespace ShoesAndBlouse.Infrastructure.Constants;

public static class CacheKeys
{
    public static readonly Func<int, string> ProductById = productId => $"product-{productId}";
    public static readonly Func<int, string> CartByUserId = userId => $"cart-{userId}";
}