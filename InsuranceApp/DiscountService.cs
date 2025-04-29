using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp
{
    class DiscountService : IDiscountService // Implementing the IDiscountService interface
    {
        public double GetDiscount()
        {
           
            return 0.9; // 10% discount for gamers over 50
        }
    }
    
}
