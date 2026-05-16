using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories;
using System.Collections.Generic;

namespace CarAPI.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly IOwnersRepository _ownerRepository;
        private readonly ICarsRepository _carsRepository;
        private readonly IPaymentService _paymentService;

        public OwnersService(
            IOwnersRepository ownersRepository,
            ICarsRepository carsRepository,
            IPaymentService paymentService
            )
        {
            _ownerRepository = ownersRepository;
            _carsRepository = carsRepository;
            _paymentService = paymentService;
        }

        public List<Owner> GetOwners()
        {
            return _ownerRepository.GetAllOwners();
        }

        public bool AddOwner(Owner owner)
        {
            return _ownerRepository.AddOwner(owner);
        }


        public Owner? GetById(int id)
        {
            return _ownerRepository.GetOwnerById(id);
        }

        public string BuyCar(BuyCarInput input)
        {
            if (input.Amount <= 0)
                return "Amount must be positive";

            var car = _carsRepository.GetCarById(input.CarId);
            if (car == null)
                return "Car doesn't exist";
            
            if (car.Owner != null)
                return "Car is already sold";

            var owner = _ownerRepository.GetOwnerById(input.OwnerId);
            if (owner == null)
                return "Owner doesn't exist";

            if (owner.Car != null)
                return "Owner already has a car";

            // Process payment
            var paymentResult = _paymentService.Pay(input.Amount);
            
            // Note: Current implementations of IPaymentService (CashService) return "Success" or similar.
            // We'll assume any result containing "Failure" or "Error" is a failure for this logic.
            if (paymentResult.Contains("Failure", System.StringComparison.OrdinalIgnoreCase) || 
                paymentResult.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return $"Payment failed: {paymentResult}";

            // Complete transaction
            owner.Car = car;
            car.Owner = owner;

            return $"Successfull: Car {input.CarId} bought by {owner.Name}. Payment result: {paymentResult}";
        }
    }
}
