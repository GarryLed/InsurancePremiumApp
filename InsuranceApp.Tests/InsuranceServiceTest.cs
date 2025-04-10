﻿using System;
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
            _mockDiscountService.Setup(x => x.GetDiscount()).Returns(0.8); // discount
        }
        
        [Test]
        public void GetCalcPremium_Casual_Age25_Returns05Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);


            // Act
            var result = service.CalcPremium(25, "casual");
            // Assert
            Assert.That(result, Is.EqualTo(5.0));
        }

        [Test]
        public void GetCalcPremium_Casual_Age33_Returns2_5Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);

            // Act
            var result = service.CalcPremium(33, "casual");

            // Assert
            Assert.That(result, Is.EqualTo(2.5));
        }

        [Test]
        public void GetCalcPremium_Casual_Age15_Returns0Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(15, "casual");
            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetCalcPremium_Hardcore_Age25_Returns6Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(25, "hardcore");
            // Assert
            Assert.That(result, Is.EqualTo(6.0));
        }

        [Test]
        public void GetCalcPremium_Hardcore_Age45_Returns5Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(45, "hardcore");
            // Assert
            Assert.That(result, Is.EqualTo(5.0));
        }

        [Test]
        public void GetCalcPremium_Hardcore_Age12_Returns0Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(12, "hardcore");
            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetCalcPremium_Unknown_Age25_Returns0Euro()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(25, "unknown");
            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetCalcPremium_Casual_Age55_ReturnsAddedDiscount()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(55, "casual");
            // Assert
            Assert.That(result, Is.EqualTo(2.0)); // need to add the discount here
        }

        [Test]
        public void GetCalcPremium_Hardcore_Age55_ReturnsAddedDiscount()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(55, "hardcore");
            // Assert
            Assert.That(result, Is.EqualTo(4.0)); // need to add the discount here
        }

        [Test]
        public void GetCalcPremium_Casual_Age31_Returns2_5EuroEdgeCase()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(31, "casual");
            // Assert
            Assert.That(result, Is.EqualTo(2.5));
        }

        [Test]
        public void GetCalcPremium_Casual_Age55_ReturnsAddedDiscountEdgeCase()
        {
            // Arrange
            var service = new InsuranceService(_mockDiscountService.Object);
            // Act
            var result = service.CalcPremium(55, "hardcore");
            // Assert
            Assert.That(result, Is.EqualTo(4.0)); // need to add the discount here
        }


    }
        
}
