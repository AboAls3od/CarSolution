using CarAPI.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarAPiTests.Fake
{
    internal class FakePaymentRespository : IPaymentService
    {
        public string Pay(double amount)
        {
            return "Success";
        }
    }
}
