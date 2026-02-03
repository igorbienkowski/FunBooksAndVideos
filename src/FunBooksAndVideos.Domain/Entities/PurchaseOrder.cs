using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Entities;

public class PurchaseOrder(long poId, long customerId, List<Product> items)
{
    public long PoId { get; init; } = poId;
    
    public long CustomerId { get; init; } = customerId;
    public List<Product> Items { get; init; } = items;

    public decimal TotalPrice => Items.Sum(x => x.Price);
}