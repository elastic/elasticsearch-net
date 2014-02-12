using NUnit.Framework;
using System;

namespace Nest.Tests.Unit.Extensions
{
    [TestFixture]
    public class UriExtensionsTests
    {
        [Test]
        public void CanGetUrlForUriWithPort()
        {
            // Arrange
            var uri = new Uri("http://localhost:8080");

            // Act
            var url = uri.ToUrlAndOverridePath("?test1=value1&test2=value2");

            // Assert
            Assert.IsTrue(url == "http://localhost:8080?test1=value1&test2=value2");
        }

        [Test]
        public void CanGetUrlForUriWithoutPort()
        {
            // Arrange
            var uri = new Uri("http://localhost/");

            // Act
            var url = uri.ToUrlAndOverridePath("?test1=value1&test2=value2");

            // Assert
            Assert.IsTrue(url == "http://localhost:80?test1=value1&test2=value2");
        }

    }
}
