using System;
using System.Text;
using Elasticsearch.Net;
using Nest;

namespace Tests.Core.Client
{
	public static class FixedResponseClient
	{
		public static IElasticClient Create(
			object response,
			int statusCode = 200,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			string contentType = RequestData.MimeType,
			Exception exception = null)
		{
			var settings = CreateConnectionSettings(response, statusCode, modifySettings, contentType, exception);
			return new ElasticClient(settings);
		}

		public static ConnectionSettings CreateConnectionSettings(
			object response,
			int statusCode = 200,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			string contentType = RequestData.MimeType,
			Exception exception = null)
		{
			var serializer = TestClient.Default.RequestResponseSerializer;
			byte[] fixedResult = null;
			if (response is string s) fixedResult = Encoding.UTF8.GetBytes(s);
			else if (contentType == RequestData.MimeType) fixedResult = serializer.SerializeToBytes(response);
			else fixedResult = Encoding.UTF8.GetBytes(response.ToString());

			var connection = new InMemoryConnection(fixedResult, statusCode, exception, contentType);
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var defaultSettings = new ConnectionSettings(connectionPool, connection)
				.DefaultIndex("default-index");
			var settings = (modifySettings != null) ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}

	}
}
