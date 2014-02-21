using NUnit.Framework;
using Elasticsearch.Net;

namespace Nest.Tests.Unit.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void CanConvertQueryStringToNameValueCollectionWithQuestionMark()
        {
            // Arrange
            var queryString = "?test1=one&test2=two";

            // Act
            var queryCollection = queryString.ToNameValueCollection();

            // Assert
            Assert.IsTrue(queryCollection["test1"] == "one");
            Assert.IsTrue(queryCollection["test2"] == "two");
        }

        [Test]
        public void CanConvertQueryStringToNameValueCollectionWithoutQuestionMark()
        {
            // Arrange
            var queryString = "test1=testone&test2=testtwo";

            // Act
            var queryCollection = queryString.ToNameValueCollection();

            // Assert
            Assert.IsTrue(queryCollection["test1"] == "testone");
            Assert.IsTrue(queryCollection["test2"] == "testtwo");
        }
    }
}
