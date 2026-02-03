namespace FunBooksAndVideos.Domain.Models;

public record Video(string Name, decimal Price) : Product(Name, Price);