using FunBooksAndVideos.Domain.Interfaces;
using FunBooksAndVideos.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FunBooksAndVideos.Infrastructure.Services;

public class MembershipService(ILogger<MembershipService> logger) : IMembershipService
{
    public void Activate(long customerId, MembershipType type)
    {
        logger.LogInformation("Processing BR1: Activating {MembershipType} for Customer {CustomerId}", type, customerId);
    }
}