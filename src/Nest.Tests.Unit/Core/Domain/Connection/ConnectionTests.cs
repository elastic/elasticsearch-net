using NUnit.Framework;
using System;
using System.Collections.Specialized;

namespace Nest.Tests.Unit.Domain.Connection
{
    using System.Net;

    public class TestConnection : Nest.Connection
    {
        public TestConnection(IConnectionSettings settings)
            : base(settings) { }

        public HttpWebRequest GetConnection(string path, string method)
        {
            return base.CreateConnection(path, method);
        }
    }

    [TestFixture]
    public class ConnectionTests
    {
        [Test]
        public void CanCreateConnectionWithCustomQueryStringParameters()
        {
            // Arrange
            var uri = new Uri("http://localhost");
            var query = new NameValueCollection { { "authToken", "ABCDEFGHIJK" } };
            var connectionSettings = new ConnectionSettings(uri).SetGlobalQueryStringParameters(query);
            var connection = new TestConnection(connectionSettings);

            // Act
            var req = connection.GetConnection("/", "GET");

            // Assert
	        Assert.AreEqual(req.Address.ToString(), "http://localhost/?authToken=ABCDEFGHIJK");
        }

        [Test]
        public void CanCreateConnectionWithPathAndCustomQueryStringParameters()
        {
            // Arrange
            var uri = new Uri("http://localhost:9000");
            var query = new NameValueCollection { { "authToken", "ABCDEFGHIJK" } };
			var connectionSettings = new ConnectionSettings(uri).SetGlobalQueryStringParameters(query);            
            var connection = new TestConnection(connectionSettings);

            // Act
            var req = connection.GetConnection("/index/", "GET");

            // Assert
	        Assert.AreEqual(req.Address.ToString(), "http://localhost:9000/index/?authToken=ABCDEFGHIJK");
        }
    }
}