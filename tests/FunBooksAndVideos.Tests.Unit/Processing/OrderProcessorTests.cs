using NSubstitute;
using FluentAssertions;
using FunBooksAndVideos.Domain.Entities;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Interfaces;
using FunBooksAndVideos.Application.Processing;

namespace FunBooksAndVideos.Tests.Unit.Processing;

public class OrderProcessorTests
{
    private readonly IMembershipService _membershipService = Substitute.For<IMembershipService>();
    private readonly IShippingService _shippingService = Substitute.For<IShippingService>();
    private readonly OrderProcessor _sut;

    public OrderProcessorTests()
    {
        _sut = new OrderProcessor(_membershipService, _shippingService);
    }

    [Fact]
    public void Process_Should_ActivateMembership_When_OrderContainsBookClubMembership_BR1()
    {
        // Arrange
        var customerId = 4567890L;
        var order = new PurchaseOrder(3344656, customerId, [
            new Membership("Book Club", 5.00m, MembershipType.BookClub)
        ]);

        // Act
        _sut.Process(order);

        // Assert
        _membershipService.Received(1).Activate(customerId, MembershipType.BookClub);
        _shippingService.DidNotReceiveWithAnyArgs().GenerateShippingSlip(default!);
    }

    [Fact]
    public void Process_Should_GenerateShippingSlip_For_PhysicalBook_BR2()
    {
        // Arrange
        var order = new PurchaseOrder(3344656, 4567890, [
            new Book("The Girl on the train", 28.50m, IsPhysical: true)
        ]);

        // Act
        _sut.Process(order);

        // Assert
        _shippingService.Received(1).GenerateShippingSlip(order);
        _membershipService.DidNotReceiveWithAnyArgs().Activate(default, default);
    }

    [Fact]
    public void Process_Should_GenerateShippingSlip_For_AnyVideo_BR2()
    {
        // Arrange
        var order = new PurchaseOrder(3344656, 4567890, [
            new Video("Comprehensive First Aid Training", 15.00m)
        ]);

        // Act
        _sut.Process(order);

        // Assert
        _shippingService.Received(1).GenerateShippingSlip(order);
    }

    [Fact]
    public void Process_Should_NOT_GenerateShippingSlip_For_DigitalBook_BR2()
    {
        // Arrange
        var order = new PurchaseOrder(3344656, 4567890, [
            new Book("Digital Guide", 10.00m, IsPhysical: false)
        ]);

        // Act
        _sut.Process(order);

        // Assert
        _shippingService.DidNotReceiveWithAnyArgs().GenerateShippingSlip(default!);
        _membershipService.DidNotReceiveWithAnyArgs().Activate(default, default);
    }

    [Fact]
    public void Process_Should_HandleComplexOrder_FromRequirementScreen()
    {
        // Arrange
        var customerId = 4567890L;
        var order = new PurchaseOrder(3344656, customerId, [
            new Video("First Aid Training", 15.00m),          // BR2 -> Slip
            new Book("The Girl on the train", 28.50m, true), // BR2 -> Slip
            new Membership("Book Club", 5.00m, MembershipType.BookClub) // BR1 -> Activate
        ]);

        // Act
        _sut.Process(order);

        // Assert
        using (new FluentAssertions.Execution.AssertionScope())
        {
            _shippingService.Received(2).GenerateShippingSlip(order);
            _membershipService.Received(1).Activate(customerId, MembershipType.BookClub);
            
            order.Items.Should().HaveCount(3);
            order.TotalPrice.Should().Be(48.50m);
        }
    }

    [Fact]
    public void Process_Should_DoNothing_When_OrderIsEmpty()
    {
        // Arrange
        var order = new PurchaseOrder(1, 1, []);

        // Act
        _sut.Process(order);

        // Assert
        _shippingService.DidNotReceiveWithAnyArgs().GenerateShippingSlip(default!);
        _membershipService.DidNotReceiveWithAnyArgs().Activate(default, default);
    }
    
    [Fact]
    public void Process_Should_HandleLargeOrder_WithMultipleItemsOfSameType()
    {
        // Arrange
        var order = new PurchaseOrder(1, 1, [
            new Video("Video 1", 10m),
            new Video("Video 2", 10m),
            new Membership("M1", 5m, MembershipType.BookClub),
            new Membership("M2", 5m, MembershipType.VideoClub)
        ]);

        // Act
        _sut.Process(order);

        // Assert
        _shippingService.Received(2).GenerateShippingSlip(order);
        _membershipService.Received(2).Activate(Arg.Any<long>(), Arg.Any<MembershipType>());
    }

    [Fact]
    public void Process_Should_NotThrow_When_OrderHasNoActionableItems()
    {
        // Arrange
        var order = new PurchaseOrder(1, 1, [
            new Book("E-Book 1", 10m, IsPhysical: false),
            new Book("E-Book 2", 10m, IsPhysical: false)
        ]);

        // Act
        Action act = () => _sut.Process(order);

        // Assert
        act.Should().NotThrow();
        _shippingService.DidNotReceiveWithAnyArgs().GenerateShippingSlip(default!);
        _membershipService.DidNotReceiveWithAnyArgs().Activate(default, default);
    }
}