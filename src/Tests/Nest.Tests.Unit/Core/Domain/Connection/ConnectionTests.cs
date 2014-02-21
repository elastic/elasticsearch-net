using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using NUnit.Framework;
using System;
using System.Collections.Specialized;

namespace Nest.Tests.Unit.Domain.Connection
{
    using System.Net;

    public class TestConnection : HttpConnection
    {
        public TestConnection(IConnectionSettingsValues settings)
            : base(settings) { }

        public HttpWebRequest GetConnection(string path, string method)
        {
            return base.CreateConnection(path, method);
        }
    }

    [TestFixture]
    public class ConnectionTests : BaseJsonTests
    {
        [Test]
        public void CanCreateConnectionWithCustomQueryStringParameters()
        {
            // Arrange
            var uri = new Uri("http://localhost");
            var query = new NameValueCollection { { "authToken", "ABCDEFGHIJK" } };
            var connectionSettings = new ConnectionSettings(uri, "index").SetGlobalQueryStringParameters(query);
            var connection = new TestConnection(connectionSettings);

            // Act
            var req = connection.GetConnection("", "GET");

            // Assert
	        Assert.AreEqual(req.Address.ToString(), "http://localhost/?authToken=ABCDEFGHIJK");
        }

        [Test]
        public void CanCreateConnectionWithPathAndCustomQueryStringParameters()
        {
            // Arrange
            var uri = new Uri("http://localhost:9000");
            var query = new NameValueCollection { { "authToken", "ABCDEFGHIJK" } };
			var connectionSettings = new ConnectionSettings(uri, "index").SetGlobalQueryStringParameters(query);            
            var connection = new TestConnection(connectionSettings);

            // Act
            var req = connection.GetConnection("index/", "GET");

            // Assert
	        Assert.AreEqual(req.Address.ToString(), "http://localhost:9000/index/?authToken=ABCDEFGHIJK");
        }


		[Test]
		public void SendStringAsJsonBody()
		{
			var jsonAsString = "{ \"json_as_a_string\" : true}";
			var result = this._client.Raw.Bulk(jsonAsString, qs => qs
				.Replication(ReplicationOptions.Async)
				.Refresh(true)
			);
			StringAssert.EndsWith(":9200/_bulk?replication=async&refresh=true", result.RequestUrl);
			Assert.AreEqual(jsonAsString, result.Request);
		}

		[Test]
		public void SendAnonymousObjectAsJsonBody()
		{
            var jsonAsString = string.Format("{{{0}  \"json_as_a_string\": true{0}}}", System.Environment.NewLine);
			var result = this._client.Raw.Bulk(
				new { json_as_a_string = true }
				, qs => qs
					.Replication(ReplicationOptions.Async)
					.Refresh(true)
			);
			StringAssert.EndsWith(":9200/_bulk?replication=async&refresh=true", result.RequestUrl);
			Assert.AreEqual(jsonAsString, result.Request);
		}
    }
}