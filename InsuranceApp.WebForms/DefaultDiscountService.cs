namespace InsuranceApp.WebForms
{
    public class DefaultDiscountService : IDiscountService
    {
        public double GetDiscount()
        {
            return 0.1; // 10% discount for gamers over 50
        }

    }
}
