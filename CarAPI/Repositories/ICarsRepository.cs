using CarAPI.Entities;
using System.Collections.Generic;

namespace CarAPI.Repositories
{
    public interface ICarsRepository
    {
        List<Car> GetAllCars();
        Car? GetCarById(int id);
        bool AddCar(Car car);
        bool Remove(int carId);
    }
}
