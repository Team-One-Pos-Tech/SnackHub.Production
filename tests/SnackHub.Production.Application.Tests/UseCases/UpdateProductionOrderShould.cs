using FluentAssertions;
using Moq;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;
using UpdateProductionOrderStatus = SnackHub.Production.Application.Models.Requests.UpdateProductionOrderStatus;

namespace SnackHub.Application.Tests.UseCases;

internal class UpdateProductionOrdersShould
{
    private Mock<IProductionOrderRepository> productionOrderRepositoryMock;
    private Production.Application.UseCases.UpdateProductionOrderStatus updateProductionOrders;

    [SetUp]
    public void Setup()
    {
        productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        updateProductionOrders = new Production.Application.UseCases.UpdateProductionOrderStatus(productionOrderRepositoryMock.Object);
    }

    [Test]
    public async Task Update_Production_Order()
    {
        #region Arrange

        var request = new UpdateProductionOrderStatus
        {
            OrderId = Guid.NewGuid()
        };

        var productionOrder = new ProductionOrder(Guid.NewGuid(), request.OrderId, [], ProductionOrderStatus.Received);

        productionOrderRepositoryMock.Setup(x => x.GetByOderIdAsync(request.OrderId))
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

        var request = new UpdateProductionOrderStatus
        {
            OrderId = Guid.NewGuid()
        };

        productionOrderRepositoryMock.Setup(x => x.GetByOderIdAsync(request.OrderId));

        #endregion

        #region Act

        var response = await updateProductionOrders.Execute(request);

        #endregion

        #region Assert

        response.IsValid.Should().BeFalse();

        response.Notifications.First().Key.Should().Be(nameof(request.OrderId));
        response.Notifications.First().Message.Should().Be("Production order not found!");

        #endregion
    }
}
