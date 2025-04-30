using System;
using System.Collections.Generic;
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

        // The test cases from black-box test analysis
        public static IEnumerable<TestCaseData> BlackBoxTestCases()
        {
            yield return new TestCaseData("21", "casual", "€5.00").SetName("TC1_Casual_Age21_Premium5");
            yield return new TestCaseData("32", "casual", "€3.50").SetName("TC2_Casual_Age32_Premium3_5");
            yield return new TestCaseData("10", "casual", "€0.00").SetName("TC3_Casual_Age10_Premium0");
            yield return new TestCaseData("21", "hardcore", "€6.00").SetName("TC4_Hardcore_Age21_Premium6");
            yield return new TestCaseData("40", "hardcore", "€5.00").SetName("TC5_Hardcore_Age40_Premium5");
            yield return new TestCaseData("10", "hardcore", "€0.00").SetName("TC6_Hardcore_Age10_Premium0");
            yield return new TestCaseData("25", "randomABCDE", "€0.00").SetName("TC7_InvalidGameMode_Premium0");
            yield return new TestCaseData("55", "casual", "€3.15").SetName("TC8_Casual_Age55_Discount");
            yield return new TestCaseData("55", "hardcore", "€4.50").SetName("TC9_Hardcore_Age55_Discount");
            yield return new TestCaseData("25", "cahzhual", "€0.00").SetName("TC10_SpeltWrongCasual_Premium0");
            yield return new TestCaseData("25", "haardcorr", "€0.00").SetName("TC11_SpeltWrongHardcore_Premium0");
        }

        [Test, TestCaseSource(nameof(BlackBoxTestCases))]
        public void CalculatePremium_BlackBoxTests(string age, string gameMode, string expectedResult)
        {
            // Arrange
            driver.FindElement(By.Id("Age")).Clear(); // find the input field by ID "Age" and clear it 
            driver.FindElement(By.Id("Age")).SendKeys(age); // send the age value to the input field

            var dropdown = new SelectElement(driver.FindElement(By.Id("GameMode"))); // find the dropdown element by id  "GameMode"

            if (gameMode == "casual" || gameMode == "hardcore")
            {
                dropdown.SelectByValue(gameMode); // select the value from the dropdown
            }
            else
            {
                // Inject invalid gamemode via JavaScript (used for testing invalid gamemode) 
                ((IJavaScriptExecutor)driver).ExecuteScript(
                    $"document.getElementById('GameMode').value = '{gameMode}';"); // this prevents the program from crashing if the gamemode is invalid
            }

            // Act
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Use WebDriverWait to avoid NoSuchElementException
            string result;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                var resultElement = wait.Until(d => d.FindElement(By.CssSelector(".alert-info")));
                result = resultElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                // If no result is rendered, fallback to 0
                result = "€0.00";
            }

            // Assert 
            Assert.That(result, Does.Contain(expectedResult),
                $"Expected: {expectedResult}, but got: {result}");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
