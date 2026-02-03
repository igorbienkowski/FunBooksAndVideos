namespace FunBooksAndVideos.Application.Dtos;

public record OrderItemRequest(string Name, string Type, decimal Price, bool? IsPhysical = null);