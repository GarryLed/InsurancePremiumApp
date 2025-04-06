using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceApp; // Namespace for the InsuranceApp project
using NUnit.Framework;
using Moq; // Mocking framework for creating mock objects

namespace InsuranceApp.Tests
{
    [TestFixture]
    class InsuranceServiceTest
    {

        
        private Mock<IDiscountService> _mockDiscountService; // Mock object for IDiscountService

        [SetUp]
        public void Setup()
        {
            _mockDiscountService = new Mock<IDiscountService>();
            _mockDiscountService.Setup(x => x.GetDiscount());
        }
        
        [Test]
        public void GetCalcPremium_Casual_Age15_Returns0Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);


            // Act
            var result = service.CalcPremium(15, "casual");
            // Assert
            Assert.That(result, Is.EqualTo(0.0));
        }

        [Test]
        public void GetCalcPremium_Casual_Age18_Returns5Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);

            // Act
            var result = service.CalcPremium(18, "casual");

            // Assert
            Assert.That(result, Is.EqualTo(5.0));
        }

        [Test]
        public void GetCalcPremium_Casual_Age31_Returns3_5Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(31, "casual");
            // Assert
            Assert.That(result, Is.EqualTo(2.5));
        }

        [Test]
        public void GetCalcPremium_Hardcore_Age30_Returns6Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(30, "hardcore");
            // Assert
            Assert.That(result, Is.EqualTo(6.0));
        }
        
        
    }
        
}
