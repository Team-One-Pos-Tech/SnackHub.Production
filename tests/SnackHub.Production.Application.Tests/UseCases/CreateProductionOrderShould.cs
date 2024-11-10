using FluentAssertions;
using Moq;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Production.Application.Models;
using SnackHub.Production.Application.UseCases;

namespace SnackHub.Application.Tests.UseCases;

internal class CreateProductionOrderShould
{

    [Test]
    public async Task Should_Create_Production_Order_Async()
    {
        #region Arrange
        var productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        var createProductionOrder = new CreateProductionOrder(productionOrderRepositoryMock.Object);

        var orderId = Guid.NewGuid();

        #endregion

        #region Act
        var response = await createProductionOrder.Execute(new CreateProductionOrderRequest(orderId));

        #endregion

        #region Assert
        response.IsValid.Should().BeTrue();

        productionOrderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ProductionOrder>()), Times.Once);
        #endregion
    }

    [Test]
    public async Task Should_Create_Production_Order_With_Items()
    {
        #region Arrange
        var productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        var createProductionOrder = new CreateProductionOrder(productionOrderRepositoryMock.Object);

        var orderId = Guid.NewGuid();

        var request = new CreateProductionOrderRequest(orderId)
        {
            Items = new List<ProductionOrderItemRequest>
            {
                new() {
                    Id = Guid.NewGuid(),
                    ProductName = "X-Bacon",
                    Quantity = 3
                }
            }
        };

        #endregion

        #region Act
        var response = await createProductionOrder.Execute(request);

        #endregion

        #region Assert
        response.IsValid.Should().BeTrue();

        productionOrderRepositoryMock.Verify(x => x.AddAsync(It.Is<ProductionOrder>(
                request => request.Items.Any(x => x.ProductName == "X-Bacon" && x.Quantity == 3)
            )), Times.Once);
        #endregion
    }
}
