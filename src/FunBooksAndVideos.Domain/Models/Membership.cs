namespace FunBooksAndVideos.Domain.Models;

public record Membership(string Name, decimal Price, MembershipType Type) : Product(Name, Price);