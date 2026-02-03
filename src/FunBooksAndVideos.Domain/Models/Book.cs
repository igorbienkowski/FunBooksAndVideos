namespace FunBooksAndVideos.Domain.Models;

public record Book(string Name, decimal Price, bool IsPhysical = true) : Product(Name, Price);