using FunBooksAndVideos.Domain.Interfaces;
using FunBooksAndVideos.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FunBooksAndVideos.Infrastructure.Services;

public class ShippingService(ILogger<ShippingService> logger) : IShippingService
{
    public void GenerateShippingSlip(PurchaseOrder order)
    {
        logger.LogInformation("Processing BR2: Generated shipping slip for Order {PoId}. Customer: {CustomerId}", order.PoId, order.CustomerId);
    }
}