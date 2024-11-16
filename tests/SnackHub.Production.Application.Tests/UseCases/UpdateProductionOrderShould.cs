using FluentAssertions;
using MassTransit;
using Moq;
using SnackHub.Production.Application.UseCases;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Application.Tests.UseCases;

internal class UpdateProductionOrdersShould
{
    private Mock<IProductionOrderRepository> productionOrderRepositoryMock;
    private Mock<IPublishEndpoint> publicEndpoint;
    private Production.Application.UseCases.UpdateProductionOrderStatus updateProductionOrders;

    [SetUp]
    public void Setup()
    {
        productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        publicEndpoint = new Mock<IPublishEndpoint>();

        updateProductionOrders = new UpdateProductionOrderStatus(productionOrderRepositoryMock.Object, publicEndpoint.Object);
    }

    [Test]
    public async Task Update_Production_Order()
    {
        #region Arrange

        var request = new Production.Application.Models.Requests.UpdateStatusRequest
        {
            Id = Guid.NewGuid()
        };

        var productionOrder = new ProductionOrder(Guid.NewGuid(), request.Id, ProductionOrderStatus.Received);

        productionOrderRepositoryMock.Setup(x => x.GetByOderIdAsync(request.Id))
            .ReturnsAsync(productionOrder);

        #endregion

        #region Act

        var response = await updateProductionOrders.Execute(request);

        #endregion

        #region Assert

        response.IsValid.Should().BeTrue();

        productionOrderRepositoryMock.Verify(x => x.EditAsync(It.Is<ProductionOrder>(
            request => request.OrderId == productionOrder.OrderId && 
                request.Status == ProductionOrderStatus.Preparing
        )), Times.Once);

        #endregion
    }

    [Test]
    public async Task Validate_Production_Order_When_Does_Not_Exists()
    {
        #region Arrange

        var request = new Production.Application.Models.Requests.UpdateStatusRequest
        {
            Id = Guid.NewGuid()
        };

        productionOrderRepositoryMock.Setup(x => x.GetByOderIdAsync(request.Id));

        #endregion

        #region Act

        var response = await updateProductionOrders.Execute(request);

        #endregion

        #region Assert

        response.IsValid.Should().BeFalse();

        response.Notifications.First().Key.Should().Be(nameof(request.Id));
        response.Notifications.First().Message.Should().Be("Production order not found!");

        #endregion
    }
}
