using Elasticsearch.Net;
using NUnit.Framework;
using Shared.Extensions;
using System;
using System.Collections.Specialized;

namespace Nest.Tests.Unit.Extensions
{
    [TestFixture]
    public class NameValueCollectionExtensions
    {
        [Test]
        public void CanCopyKeyValues()
        {
            // Arrange
            var source = new NameValueCollection { { "key1", "value1" }, { "key2", "value2" }, };
            var dest = new NameValueCollection();

            // Act
            source.CopyKeyValues(dest);

            // Assert
            Assert.IsTrue(dest["key1"] == "value1");
            Assert.IsTrue(dest["key2"] == "value2");
        }

        [Test]
        public void UnableToCopyKeyValues()
        {
            // Arrange
            var source = new NameValueCollection { { "key1", "value1" }, { "key2", "value2" }, };
            var dest = new NameValueCollection();

            // Act
            source.CopyKeyValues(dest);

            // Assert
            Assert.IsTrue(dest["key1"] == "value1");
            Assert.IsTrue(dest["key2"] == "value2");
        }

        [Test]
        public void CopyKeyValuesWithDuplicateKeyThrowException()
        {
            // Arrange
            var source = new NameValueCollection { { "key1", "value1" } };
            var dest = new NameValueCollection { { "key1", "value1" } };

            // Act            
            // Assert
            Assert.Throws<ApplicationException>(() => source.CopyKeyValues(dest));
        }

        [Test]
        public void CanCovertNameValueCollectionToQueryString()
        {
            // Arrange
            var queryCollection = new NameValueCollection { { "key1", "value1" }, { "key2", "value2" }, };

            // Act
            var queryString = queryCollection.ToQueryString();

            // Assert
            Assert.IsTrue(queryString == "?key1=value1&key2=value2");
        }

        [Test]
        public void ConvertEmptyNameValueCollectionToQueryStringReturnsEmptyString()
        {
            // Arrange
            var queryCollection = new NameValueCollection();

            // Act
            var queryString = queryCollection.ToQueryString();

            // Assert
            Assert.IsTrue(queryString == string.Empty);
        }
    }
}
