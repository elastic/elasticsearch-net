/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
			Exception exception = null
		)
		{
			var settings = CreateConnectionSettings(response, statusCode, modifySettings, contentType, exception);
			return new ElasticClient(settings);
		}

		public static ConnectionSettings CreateConnectionSettings(
			object response,
			int statusCode = 200,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			string contentType = null,
			Exception exception = null
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

			var connection = new InMemoryConnection(responseBytes, statusCode, exception, contentType);
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var defaultSettings = new ConnectionSettings(connectionPool, connection)
				.DefaultIndex("default-index");
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}
	}
}
