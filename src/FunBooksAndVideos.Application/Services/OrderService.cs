using FunBooksAndVideos.Application.Dtos;
using FunBooksAndVideos.Application.Interfaces;
using FunBooksAndVideos.Application.Processing;
using FunBooksAndVideos.Domain.Entities;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Application.Services;

public class OrderService(OrderProcessor processor) : IOrderService
{
    public void CreateOrder(OrderRequest request)
    {
        var domainItems = request.Items.Select(item => item.Type.ToLower() switch
        {
            "membership" => new Membership(item.Name, item.Price, MembershipType.BookClub) as Product,
            "book" => new Book(item.Name, item.Price, item.IsPhysical ?? true),
            _ => new Video(item.Name, item.Price)
        }).ToList();

        var order = new PurchaseOrder(3344656, request.CustomerId, domainItems);

        processor.Process(order);
    }
}