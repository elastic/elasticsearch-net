// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
		public static ElasticsearchClient Create(
			object response,
			int statusCode = 200,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> modifySettings = null,
			string contentType = RequestData.DefaultMimeType,
			Exception exception = null
		)
		{
			var settings = CreateConnectionSettings(response, statusCode, modifySettings, contentType, exception);
			return new ElasticsearchClient(settings);
		}

		public static ElasticsearchClientSettings CreateConnectionSettings(
			object response,
			int statusCode = 200,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> modifySettings = null,
			string contentType = RequestData.DefaultMimeType,
			Exception exception = null,
			Serializer serializer = null
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
						responseBytes = contentType == RequestData.DefaultMimeType
							? serializer.SerializeToBytes(response,
								TestClient.Default.ElasticsearchClientSettings.MemoryStreamFactory)
							: Encoding.UTF8.GetBytes(response.ToString());
						break;
					}
			}

			var headers = new Dictionary<string, IEnumerable<string>> { { "x-elastic-product", new[] { "Elasticsearch" } } };

			var connection = new InMemoryTransportClient(responseBytes, statusCode, exception, contentType, headers);
			var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
			var defaultSettings = new ElasticsearchClientSettings(nodePool, connection)
				.DefaultIndex("default-index");
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}
	}
}
