using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Interfaces.Repositories;
using MidtownRestaurantSystem.Models;
using MidtownRestaurantSystem.Services;
using Moq;

namespace MidtownRestaurant.NUnitTests
{
    public class OrderServiceTests
    {
        private readonly IOrdersService _systemUnderTest;
        private readonly Mock<IOrdersRepository> _ordersRepository = new Mock<IOrdersRepository>();

        public OrderServiceTests()
        {
            _systemUnderTest = new OrdersService(_ordersRepository.Object);
        }

        [Test]
        public void CreateNewOrder_TableId_OrderID()
        {
            // Arrange
            int tableId = 1;
            int expectedOrderId = 123;
            _ordersRepository.Setup(x => x.GetOrdersByStatus(OrderStatus.Processing))
                .Returns(new List<Order>(){ new Order(expectedOrderId, tableId, 0, OrderStatus.Processing) });

            // Act
            int actualOrderId = _systemUnderTest.CreateNewOrder(tableId);

            // Assert
            Assert.AreEqual(expectedOrderId, actualOrderId);
            _ordersRepository.Verify(x => x.AddOrder(It.Is<Order>(obj => obj.TableID == 1)));
        }

        [Test]
        public void ValidateIfTableHasOrderWithStatus_TableIDAndOrderStatusProcessing_True()
        {
            //Arrange
            int tableID = 1;
            OrderStatus orderStatus = OrderStatus.Processing;
            int ID = 1;
            int completenessTimestamp = 1675013436;

            _ordersRepository.Setup(x => x.GetOrdersByStatus(OrderStatus.Processing))
                .Returns(new List<Order>(){
                    new Order(ID, tableID, completenessTimestamp, orderStatus),
                });

            //Act
            bool actual = _systemUnderTest.ValidateIfTableHasOrderWithStatus(tableID, orderStatus);

            //Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ValidateIfTableHasOrderWithStatus_TableIDAndOrderStatusProcessing_False()
        {
            //Arrange
            int tableIDInput = 1;
            int tableIDRetrieved = 2;
            OrderStatus orderStatus = OrderStatus.Processing;
            int ID = 1;
            int completenessTimestamp = 1675013436;

            _ordersRepository.Setup(x => x.GetOrdersByStatus(OrderStatus.Processing))
                .Returns(new List<Order>(){
                    new Order(ID, tableIDRetrieved, completenessTimestamp, orderStatus),
                });

            //Act
            bool actual = _systemUnderTest.ValidateIfTableHasOrderWithStatus(tableIDInput, orderStatus);

            //Assert
            Assert.IsFalse(actual);
        }
    }
}