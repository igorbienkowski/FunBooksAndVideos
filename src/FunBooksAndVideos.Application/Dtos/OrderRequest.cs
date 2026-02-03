namespace FunBooksAndVideos.Application.Dtos;

public record OrderRequest(long CustomerId, List<OrderItemRequest> Items);