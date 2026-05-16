using CarAPI.Entities;
using CarAPI.Repositories;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace CarAPiTests
{
    public class CarsRepositoryTests
    {

        private readonly Mock<InMemoryContext> _mockContext;

        private readonly CarsRepository _carsRepository;

        public CarsRepositoryTests()
        {
            _mockContext = new Mock<InMemoryContext>();
            _carsRepository = new CarsRepository(_mockContext.Object);
        }



        [Fact]
        public void GetAllCars_ReturnsAllCars_Mock()
        {
            // Arrange
            var mockCars = new List<Car>
            {
                new Car(1, CarType.Audi, 30),
               
            };
            _mockContext.Setup(c => c.Cars).Returns(mockCars);

            // Act
            var result = _carsRepository.GetAllCars();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }



        [Fact]
        public void GetCarById_ExistingId_ReturnsCar_Mock()
        {
            // Arrange
            var mockCars = new List<Car>
            {
                new Car(1, CarType.Audi, 30),
                new Car(2, CarType.BMW, 50)
            };
            _mockContext.Setup(c => c.Cars).Returns(mockCars);

            // Act
            var result = _carsRepository.GetCarById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void AddCar_ValidCar_ReturnsTrue_Mock()
        {
            // Arrange
            var mockCars = new List<Car>();
            _mockContext.Setup(c => c.Cars).Returns(mockCars);
            var newCar = new Car(3, CarType.Audi, 60);

            // Act
            var result = _carsRepository.AddCar(newCar);

            // Assert
            Assert.True(result);
            Assert.Single(_mockContext.Object.Cars);
        }
    }
}
