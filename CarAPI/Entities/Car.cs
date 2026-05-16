using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarAPI.Entities
{

    public enum CarType
    {
        Audi,
        BMW
    }
    public class Car
    {
        public int Id { get; }
        public CarType Type { get; set; }

        public double Velocity { get; set; }
        public double Price { get; set; }

        public Car(int id, CarType carType, double initialVelocity)
        {
            Id = id;
            Type = carType;
            Velocity = initialVelocity;
        }
        public Car()
        {
            
        }

        public Owner Owner { get; set; }
    }
}
