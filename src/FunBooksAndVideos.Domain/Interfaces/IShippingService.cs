using FunBooksAndVideos.Domain.Entities;

namespace FunBooksAndVideos.Domain.Interfaces;

public interface IShippingService
{
    void GenerateShippingSlip(PurchaseOrder order);
}