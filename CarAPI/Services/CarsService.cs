using CarAPI.Entities;
using CarAPI.Repositories;
using System.Collections.Generic;

namespace CarAPI.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsRepository;

        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public List<Car> GetAll()
        {
            return _carsRepository.GetAllCars();
        }

        public Car? GetCarById(int id)
        {
            return _carsRepository.GetCarById(id);
        }

        public bool AddCar(Car car)
        {
            return _carsRepository.AddCar(car);
        }

        public bool Remove(int carId)
        {
            return _carsRepository.Remove(carId);
        }
    }
}
