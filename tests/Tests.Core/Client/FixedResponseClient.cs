using System;
using System.Text;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Elastic.Clients.Elasticsearch;
using System.Collections.Generic;

namespace Tests.Core.Client
{
	public static class FixedResponseClient
	{
		public static IElasticClient Create(
			object response,
			int statusCode = 200,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> modifySettings = null,
			string contentType = RequestData.MimeType,
			Exception exception = null
		)
		{
			var settings = CreateConnectionSettings(response, statusCode, modifySettings, contentType, exception);
			return new ElasticClient(settings);
		}

		public static ElasticsearchClientSettings CreateConnectionSettings(
			object response,
			int statusCode = 200,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> modifySettings = null,
			string contentType = RequestData.MimeType,
			Exception exception = null,
			SerializerBase serializer = null
		)
		{
			serializer ??= TestClient.Default.RequestResponseSerializer;
			byte[] responseBytes;
			switch (response)
			{
				case string s:
					responseBytes = Encoding.UTF8.GetBytes(s);
					break;
				case byte[] b:
					responseBytes = b;
					break;
				default:
					{
						responseBytes = contentType == RequestData.MimeType
							? serializer.SerializeToBytes(response,
								TestClient.Default.ElasticsearchClientSettings.MemoryStreamFactory)
							: Encoding.UTF8.GetBytes(response.ToString());
						break;
					}
			}

			var headers = new Dictionary<string, IEnumerable<string>> { { "x-elastic-product", new[] { "Elasticsearch" } } };

			var connection = new InMemoryConnection(responseBytes, statusCode, exception, contentType, headers);
			var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
			var defaultSettings = new ElasticsearchClientSettings(nodePool, connection)
				.DefaultIndex("default-index");
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}
	}
}
