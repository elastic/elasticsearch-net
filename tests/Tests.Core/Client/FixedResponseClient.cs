// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
			string contentType = null,
			Exception exception = null,
			bool productCheckSucceeds = true
		)
		{
			var settings = CreateConnectionSettings(response, statusCode, modifySettings, contentType, exception, productCheckSucceeds);
			return new ElasticClient(settings);
		}

		public static ConnectionSettings CreateConnectionSettings(
			object response,
			int statusCode = 200,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			string contentType = null,
			Exception exception = null,
			bool productCheckSucceeds = true
		)
		{
			contentType ??= RequestData.DefaultJsonMimeType;
			var serializer = TestClient.Default.RequestResponseSerializer;
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
					responseBytes = RequestData.IsJsonMimeType(contentType)
						? serializer.SerializeToBytes(response, TestClient.Default.ConnectionSettings.MemoryStreamFactory)
						: Encoding.UTF8.GetBytes(response.ToString());
					break;
				}
			}

			var productCheckResponse =
				productCheckSucceeds ? InMemoryConnection.ValidProductCheckResponse() : new InMemoryHttpResponse { StatusCode = 500 };

			var connection = new InMemoryConnection(responseBytes, productCheckResponse, null, statusCode, exception, contentType);
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var defaultSettings = new ConnectionSettings(connectionPool, connection)
				.DefaultIndex("default-index");
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}
	}
}
