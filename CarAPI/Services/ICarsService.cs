using CarAPI.Entities;
using System.Collections.Generic;

namespace CarAPI.Services
{
    public interface ICarsService
    {
        bool AddCar(Car car);
        List<Car> GetAll();
        Car? GetCarById(int id);
        bool Remove(int carId);
    }
}
