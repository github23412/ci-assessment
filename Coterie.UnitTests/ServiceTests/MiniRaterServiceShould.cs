using System.Collections.Generic;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;
using NUnit.Framework;

namespace Coterie.UnitTests
{
    public class MiniRaterTests : MiniRaterServiceTestsBase
    {

        [Test]
        public void ProjectRate_WithDefaultRequest_ReturnsEmptyResponseWithNoPremiums()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "",
                Revenue = 0m,
                States = new List<string> { }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);
            
            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(0));
        }

        [Test]
        public void ProjectRate_WithNullStates_ReturnsExceptionResponse()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 100m,
                States = null
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(false));
            Assert.That(actual, Is.AssignableTo<ExceptionResponse>());
        }

        [Test]
        public void ProjectRate_WithInvalidState_ReturnsEmptyResponseWithPremiums()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 100m,
                States = new List<string> { "VA" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(1));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(0));
        }

        [Test]
        public void ProjectRate_WithInvalidBusiness_ReturnsEmptyResponseWithPremiums()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "testbusiness",
                Revenue = 100m,
                States = new List<string> { "TX" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(1));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(0));
        }

        [Test]
        public void ProjectRate_WithNullBusiness_ReturnsExceptionResponse()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = null,
                Revenue = 100m,
                States = new List<string> { "TX" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(false));
            Assert.That(actual, Is.AssignableTo<ExceptionResponse>());
        }

        [Test]
        public void ProjectRate_WithNoRevenue_ReturnsResponseWithDefaultRevenue()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 0m,
                States = new List<string> { "TX" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(1));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(4.715m));
        }

        [Test]
        public void ProjectRate_WithSmallRevenue_ReturnsResponseWithDefaultRevenue()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 100m,
                States = new List<string> { "TX" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(1));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(4.715m));
        }

        [Test]
        public void ProjectRate_WithValidShortStateRequest_ReturnsSuccessfulResultWithPremiums()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 100000m,
                States = new List<string> { "TX" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(1));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(471.5m));
        }

        [Test]
        public void ProjectRate_WithValidLongStateRequest_ReturnsSuccessfulResultWithPremiums()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 100000m,
                States = new List<string> { "texas" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(1));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(471.5m));
        }

        [Test]
        public void ProjectRate_WithInvalidMultiStateRequest_ReturnsMultipleEmptyResponse()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 100000m,
                States = new List<string> { "VA", "CA" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(2));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(0m));
            Assert.That((actual as MiniRaterResponse).Premiums[1].Premium, Is.EqualTo(0m));
        }

        [Test]
        public void ProjectRate_WithValidMultiStateRequest_ReturnsSuccessfulResultWithPremiums()
        {
            // Arrange
            var request = new MiniRaterRequest()
            {
                Business = "programmer",
                Revenue = 100000m,
                States = new List<string> { "TX", "FL" }
            };

            // Act
            var actual = MiniRaterService.ProjectRate(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.IsSuccessful, Is.EqualTo(true));
            Assert.That(actual, Is.AssignableTo<MiniRaterResponse>());
            Assert.That((actual as MiniRaterResponse).Premiums.Count, Is.EqualTo(2));
            Assert.That((actual as MiniRaterResponse).Premiums[0].Premium, Is.EqualTo(471.5m));
            Assert.That((actual as MiniRaterResponse).Premiums[1].Premium, Is.EqualTo(600m));
        }
    }
}