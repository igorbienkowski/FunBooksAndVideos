using FunBooksAndVideos.Application.Processing;
using FunBooksAndVideos.Domain.Entities;
using FunBooksAndVideos.Domain.Interfaces;
using FunBooksAndVideos.Domain.Models;
using NSubstitute;

namespace FunBooksAndVideos.Tests.Unit.Processing;

public class OrderProcessorTests
{
    private readonly IMembershipService _membershipService = Substitute.For<IMembershipService>();
    private readonly IShippingService _shippingService = Substitute.For<IShippingService>();
    private readonly OrderProcessor _sut; // System Under Test

    public OrderProcessorTests()
    {
        _sut = new OrderProcessor(_membershipService, _shippingService);
    }

    [Fact]
    public void Process_Should_ActivateMembership_When_OrderContainsMembership()
    {
        // Arrange
        var customerId = 4567890L;
        var order = new PurchaseOrder(3344656, customerId, [
            new Membership("Book Club", 20.00m, MembershipType.BookClub)
        ]);

        // Act
        _sut.Process(order);

        // Assert
        _membershipService.Received(1).Activate(customerId, MembershipType.BookClub);
    }

    [Fact]
    public void Process_Should_GenerateShippingSlip_When_OrderContainsPhysicalBook()
    {
        // Arrange
        var order = new PurchaseOrder(3344656, 4567890, [
            new Book("The Girl on the train", 28.50m, IsPhysical: true)
        ]);

        // Act
        _sut.Process(order);

        // Assert
        _shippingService.Received(1).GenerateShippingSlip(order);
    }
}