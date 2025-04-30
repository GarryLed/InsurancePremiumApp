namespace InsuranceApp.WebForms
{
    // Implementation of the IDiscountService interface
    public class DefaultDiscountService : IDiscountService
    {
        public double GetDiscount()
        {
            return 0.9; // 10% discount for gamers over 50
        }

    }
}
