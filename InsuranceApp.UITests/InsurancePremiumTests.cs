using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI; // For SelectElement

namespace InsuranceApp.UITests
{
    public class InsurancePremiumTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            // url 
            driver.Navigate().GoToUrl("http://localhost:5064/EquipmentQuote"); 
        }

        [Test]
        public void CalculatePremium_ValidCasual25_Returns5Euro()
        {
            
            driver.FindElement(By.Id("Age")).SendKeys("25");

            
            var dropdown = new SelectElement(driver.FindElement(By.Id("GameMode")));
            dropdown.SelectByValue("casual");

            // Click the Submit button 
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

           
            var result = driver.FindElement(By.CssSelector(".alert-info")).Text;

            // Assert 
            Assert.That(result, Does.Contain("€5.00"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
