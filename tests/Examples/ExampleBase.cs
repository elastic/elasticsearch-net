// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net;
using Nest;

namespace Examples
{
	public abstract class ExampleBase
	{
		// ReSharper disable once FieldCanBeMadeReadOnly.Global
		// ReSharper disable once InconsistentNaming
		protected IElasticClient client;

		protected ExampleBase()
		{
			var settings = new ConnectionSettings(new InMemoryConnection())
				.DisableDirectStreaming();

			client = new ElasticClient(settings);
		}
	}
}
