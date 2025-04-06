using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp
{
   public class InsuranceService
    {
        private readonly IDiscountService _discountService; // Dependency injection of the discount service 

        // constructor with dependency injection passed in as a parameter 
        public InsuranceService(IDiscountService discountService)
        {
            _discountService = discountService; // here we assign the injected discount service to the private field
        }

        // Method to calculate the insurance premium
        public double CalcPremium(int age, string gameMode)
        {
            // variable to hold the premium
            double premium;

            // Calculate the base premium based on age
            if (gameMode == "casual")
            {
                if ((age >= 18) && (age <= 30))
                {
                    premium = 5.0;
                }
                else if (age >=31)
                {
                    premium = 2.5;
                }
                else
                {
                    premium = 0.0;
                }

            }
            else if (gameMode == "hardcore")
            {
                if ((age >= 18) && (age <= 35))
                {
                    premium = 6.0;
                }
                else if (age >= 36)
                {
                    premium = 5.0;
                }
                else
                {
                    premium = 0.0;
                }
            }
            else
            {
                premium = 0.0; 
            }

            // Decoupled the discount service from the insurance service class
            // Call the GetDiscount method from the discount service to apply a discount
            // instead of creating a new instance of the discount service within the insurance service class
            if (age >= 50)
            {
                premium *= _discountService.GetDiscount(); 
            }

            return premium; 
        }
    }
}
