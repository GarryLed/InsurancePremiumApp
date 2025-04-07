using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.WebForms.Pages
{
    public class EquipmentQuoteModel : PageModel
    {
        private readonly InsuranceService _insuranceService; 

        public EquipmentQuoteModel(IDiscountService discountService) // Constructor with dependency injection
        {
            _insuranceService = new InsuranceService(discountService); // create a new instance of the InsuranceService class
        }

        [BindProperty] // BindProperty attribute to bind the property to the form
        [Range(1, 120, ErrorMessage = "Please enter a valid age.")]
        public int Age { get; set; } // Property to hold the age of the user

        [BindProperty]
        public string GameMode { get; set; } = "casual"; // Property to hold the game mode selected by the user (defaults to casual) 

        public double Premium { get; set; } // Property to hold the calculated premium

        public bool HasResult { get; set; } = false; // Property to indicate if the result is available

        public void OnPost() //Method to handle the form submission 
        {
            if (!ModelState.IsValid)
            {
                HasResult = false;
                return;
            }

            Premium = _insuranceService.CalcPremium(Age, GameMode); // Call the CalcPremium method of the InsuranceService class to calculate the premium
            HasResult = true;
        }
    }
}
