using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Interfaces;

public interface IMembershipService
{
    void Activate(long customerId, MembershipType type);
}