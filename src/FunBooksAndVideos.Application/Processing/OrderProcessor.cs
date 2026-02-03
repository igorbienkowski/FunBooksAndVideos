namespace FunBooksAndVideos.Application.Processing;

using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;

public class OrderProcessor(
    IMembershipService membershipService, 
    IShippingService shippingService)
{
    public void Process(PurchaseOrder order)
    {
        foreach (var item in order.Items)
        {
            switch (item)
            {
                case Membership m:
                    membershipService.Activate(order.CustomerId, m.Type);
                    break;
                    
                case Book { IsPhysical: true }:
                case Video:
                    shippingService.GenerateShippingSlip(order);
                    break;
            }
        }
    }
}