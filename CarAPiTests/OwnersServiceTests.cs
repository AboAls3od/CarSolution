using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories;
using CarAPI.Services;
using CarAPiTests.Fake;
using Moq;
using System.Text.RegularExpressions;

namespace CarAPiTests
{
    public class OwnersServiceTests
    {
        private readonly Mock<IOwnersRepository> _mockOwnersRepo;
        private readonly Mock<ICarsRepository> _mockCarsRepo;
        private readonly Mock<IPaymentService> _mockPaymentService;
        private readonly OwnersService _ownersService;
        public OwnersServiceTests()
        {
            _mockOwnersRepo = new Mock<IOwnersRepository>();
            _mockCarsRepo = new Mock<ICarsRepository>();
            _mockPaymentService = new Mock<IPaymentService>();
            _ownersService = new OwnersService(
                _mockOwnersRepo.Object,
    _mockCarsRepo.Object,
    _mockPaymentService.Object
                );
        }

        #region Tightly coupled

        [Fact]
        public void GetById_ExistingId1_NotNull()
        {
            // Arrange
            var context = new InMemoryContext();
            var ownerService = new OwnersService(
                new OwnersRepository(context),
    new CarsRepository(context),
    new CashService()
                );

            // Act
            var owner = ownerService.GetById(1);

            // Assert
            Assert.NotNull(owner);
        }

        // Intented to fail to show the problem of using real solution dependencies
        [Fact(Skip = "Will be always failing for demo")]
        public void BuyCar_ExistingCarId1OwnerId1_Successful_RealDependencies()
        {
            // Arrange
            var context = new InMemoryContext();
            var ownerService = new OwnersService(
                new OwnersRepository(context),
    new CarsRepository(context),
    new CashService()
                );
            var input = new BuyCarInput()
            {
                CarId = 1,
                OwnerId = 1,
                Amount = 100
            };

            // Act
            var result = ownerService.BuyCar(input);

            // Assert
            Assert.Contains("Successfull", result);

        }

        #endregion

        #region Fake

        [Fact]
        public void BuyCar_ExistingCarId1OwnerId1_Successful_Fake()
        {
            // Arrange
            var ownerService = new OwnersService(
                new FakeExistingOwnersRepository(),
    new FakeCarsRepository(),
    new FakePaymentRespository()
                );
            var input = new BuyCarInput()
            {
                CarId = 1,
                OwnerId = 1,
                Amount = 100
            };

            // Act
            var result = ownerService.BuyCar(input);

            // Assert
            Assert.Contains("Successfull", result);

        }

        [Fact]
        public void BuyCar_ExistingCarNotExistingOwner_OwnerDoesnotExist_Fake()
        {
            // Arrange
            var ownerService = new OwnersService(
                new FakeNotExistingOwnersRepository(),
    new FakeCarsRepository(),
    new FakePaymentRespository()
                );
            var input = new BuyCarInput()
            {
                CarId = 1,
                OwnerId = 1,
                Amount = 100
            };

            // Act
            var result = ownerService.BuyCar(input);

            // Assert
            Assert.Matches(new Regex("^Owner doesn't exist$"), result);

        }
        #endregion

        #region Mocking

        [Fact]
        public void BuyCar_ExistingCarId1OwnerId1_Successful_Mock()
        {
            // Arrange
            var input = new BuyCarInput()
            {
                CarId = 1,
                OwnerId = 1,
                Amount = 100
            };
            _mockCarsRepo.Setup(m => m.GetCarById(input.CarId)).Returns(new Car());
            _mockOwnersRepo.Setup(m => m.GetOwnerById(input.OwnerId)).Returns(new Owner());
            _mockPaymentService.Setup(m => m.Pay(input.Amount)).Returns("Success");

            // Act
            var result = _ownersService.BuyCar(input);

            // Assert
            Assert.Contains("Successfull", result);

        }

        #endregion
    }
}
