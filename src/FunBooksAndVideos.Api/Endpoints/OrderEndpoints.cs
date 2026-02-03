using FunBooksAndVideos.Application.Dtos;
using FunBooksAndVideos.Application.Interfaces;

namespace FunBooksAndVideos.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", (OrderRequest request, IOrderService orderService) => 
        {
            orderService.CreateOrder(request);
            return Results.Ok(new { Message = "Order processed successfully" });
        });
    }
}