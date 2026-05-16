using System;
using System.Collections.Generic;
using System.Linq;

namespace CarAPI.Payment
{
    public class CardService : IPaymentService
    {
        public string Pay(double amount)
        {
            // Logic
            return $"Amount: {amount} is paid through Card";
        }
    }
}
