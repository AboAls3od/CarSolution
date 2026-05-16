using CarAPI.Entities;
using CarAPI.Repositories;
using CarAPI.Services;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace CarAPiTests
{
    public class CarsServiceTests
    {
        private readonly Mock<ICarsRepository> _mockCarsRepo;
        private readonly CarsService _carsService;

        public CarsServiceTests()
        {
            _mockCarsRepo = new Mock<ICarsRepository>();
            _carsService = new CarsService(_mockCarsRepo.Object);
        }

        [Fact]
        public void GetAll_ReturnsAllCars_Mock()
        {
            // Arrange

            var carsList = new List<Car>
            {
                new Car(1, CarType.Audi, 30),
                new Car(2, CarType.BMW, 50)
            };
            _mockCarsRepo.Setup(repo => repo.GetAllCars()).Returns(carsList);

            // Act
            var result = _carsService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetCarById_ExistingId_ReturnsCar_Mock()
        {
            // Arrange
            var expectedCar = new Car(1, CarType.Audi, 30);
            _mockCarsRepo.Setup(repo => repo.GetCarById(1)).Returns(expectedCar);

            // Act
            var result = _carsService.GetCarById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void AddCar_ValidCar_ReturnsTrue_Mock()
        {
            // Arrange
            var newCar = new Car(3, CarType.BMW, 100);
            _mockCarsRepo.Setup(repo => repo.AddCar(newCar)).Returns(true);

            // Act
            var result = _carsService.AddCar(newCar);

            // Assert
            Assert.True(result);
        }
    }
}
