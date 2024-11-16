using FluentAssertions;
using Moq;
using SnackHub.Production.Application.Models;
using SnackHub.Production.Application.UseCases;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Application.Tests.UseCases;

internal class ListProductionOrdersShould
{
    private Mock<IProductionOrderRepository> productionOrderRepositoryMock;
    private ListProductionOrders listProductionOrders;

    [SetUp]
    public void Setup()
    {
        productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        listProductionOrders = new ListProductionOrders(productionOrderRepositoryMock.Object);
    }

    [Test]
    public async Task List_Production_Orders()
    {
        #region Arrange

        productionOrderRepositoryMock.Setup(Setup => Setup.ListAllAsync()   )
            .ReturnsAsync(new List<ProductionOrder>
            {
                ProductionOrder.Factory.Create(Guid.NewGuid(),
                [
                    new ProductionOrderItem(Guid.NewGuid(), 3)
                ]),
                ProductionOrder.Factory.Create(Guid.NewGuid(),
                [
                    new ProductionOrderItem(Guid.NewGuid(), 2),
                    new ProductionOrderItem(Guid.NewGuid(), 1),
                ])
            });

        #endregion

        #region Act

        var response = await listProductionOrders.Get();

        #endregion

        #region Assert

        response.Count().Should().Be(2);

        response.First().Items.Count().Should().Be(1);

        response.Last().Items.Count().Should().Be(2);

        #endregion
    }
}
