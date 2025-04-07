using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.WebForms.Pages
{
    public class EquipmentQuoteModel : PageModel
    {
        private readonly InsuranceService _insuranceService;

        public EquipmentQuoteModel(IDiscountService discountService)
        {
            _insuranceService = new InsuranceService(discountService);
        }

        [BindProperty]
        [Range(1, 120, ErrorMessage = "Please enter a valid age.")]
        public int Age { get; set; }

        [BindProperty]
        public string GameMode { get; set; } = "casual";

        public double Premium { get; set; }

        public bool HasResult { get; set; } = false;

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                HasResult = false;
                return;
            }

            Premium = _insuranceService.CalcPremium(Age, GameMode);
            HasResult = true;
        }
    }
}
