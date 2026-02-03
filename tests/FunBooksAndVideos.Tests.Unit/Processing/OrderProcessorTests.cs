using FunBooksAndVideos.Domain.Entities;
using FunBooksAndVideos.Domain.Interfaces;
using FunBooksAndVideos.Domain.Models;
using NSubstitute;

namespace FunBooksAndVideos.Tests.Unit.Processing;

public class OrderProcessorTests
{
    private readonly IMembershipService _membershipService = Substitute.For<IMembershipService>();
    private readonly IShippingService _shippingService = Substitute.For<IShippingService>();
    
    [Fact]
    public void Process_Should_ActivateMembership_When_OrderContainsMembership()
    {
        // Arrange
        var order = new PurchaseOrder(3344656, 4567890, [
            new Membership("Book Club", 20.00m, MembershipType.BookClub)
        ]);

        // Act

        // Assert
        
        Assert.Fail("Fails until OrderProcessor is implemented in Application layer.");
    }
}