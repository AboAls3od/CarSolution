using CarAPI.Entities;
using CarAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarAPiTests.Fake
{
    internal class FakeCarsRepository : ICarsRepository
    {
        public bool AddCar(Car car)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public Car? GetCarById(int id)
        {
            return new Car(id, CarType.Audi, 0);
        }

        public bool Remove(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
