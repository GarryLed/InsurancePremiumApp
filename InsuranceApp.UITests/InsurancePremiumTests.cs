using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI; 

namespace InsuranceApp.UITests
{
    [TestFixture]
    public class InsurancePremiumTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var options = new EdgeOptions();
            
            options.AddArgument("--enable-chromium");  

            driver = new EdgeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:5064/EquipmentQuote");
        }

        // Define the test cases for black box testing   
        public static IEnumerable<TestCaseData> BlackBoxTestCases()
        {
            yield return new TestCaseData("21", "casual", "€5.00").SetName("TC1_Casual_Age21_Premium5");
            yield return new TestCaseData("32", "casual", "€3.50").SetName("TC2_Casual_Age32_Premium3_5");
            yield return new TestCaseData("10", "casual", "€0.00").SetName("TC3_Casual_Age10_Premium0");
            yield return new TestCaseData("21", "hardcore", "€6.00").SetName("TC4_Hardcore_Age21_Premium6");
            yield return new TestCaseData("40", "hardcore", "€5.00").SetName("TC5_Hardcore_Age40_Premium5");
            yield return new TestCaseData("10", "hardcore", "€0.00").SetName("TC6_Hardcore_Age10_Premium0");
            yield return new TestCaseData("25", "randomABCDE", "€0.00").SetName("TC7_InvalidGameMode_Premium0");
            yield return new TestCaseData("55", "casual", "€0.35").SetName("TC8_Casual_Age55_Discount");
            yield return new TestCaseData("55", "hardcore", "€0.50").SetName("TC9_Hardcore_Age55_Discount");
            yield return new TestCaseData("25", "cahzhual", "€0.00").SetName("TC10_SpeltWrongCasual_Premium0");
            yield return new TestCaseData("25", "haardcorr", "€0.00").SetName("TC11_SpeltWrongHardcore_Premium0");
        }


        [Test, TestCaseSource(nameof(BlackBoxTestCases))]
        public void CalculatePremium_BlackBoxTests(string age, string gameMode, string expectedResult)
        {
            // Arrange
            driver.FindElement(By.Id("Age")).Clear();
            driver.FindElement(By.Id("Age")).SendKeys(age);

            var dropdown = new SelectElement(driver.FindElement(By.Id("GameMode")));
            if (gameMode == "casual" || gameMode == "hardcore")
            {
                dropdown.SelectByValue(gameMode);
            }
            else
            {
                ((IJavaScriptExecutor)driver).ExecuteScript($"document.getElementById('GameMode').value = '{gameMode}';");
            }

            // Act
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var result = driver.FindElement(By.CssSelector(".alert-info")).Text;

            // Assert
            Assert.That(result, Does.Contain(expectedResult));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
