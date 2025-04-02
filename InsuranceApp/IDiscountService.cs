using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp
{
    /// <summary>
    /// Interface for Discount Service (decouples the Discount Service from the Insurance Service)
    /// </summary>
    public interface IDiscountService
    {
        double GetDiscount();
    }
}
