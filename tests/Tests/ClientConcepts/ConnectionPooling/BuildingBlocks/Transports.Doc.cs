// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Elastic.Transport;
using Nest;
using System.Threading;
using Elastic.Transport.Products;
using Elastic.Transport.Products.Elasticsearch;
using Elasticsearch.Net;
using Tests.Domain;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	public class Transports
	{
		/**=== Transports
		*
		* The `ITransport` interface can be seen as the motor block of the client. Its interface is
		* deceitfully simple, yet it's ultimately responsible for translating a client call to a response.
		*
		* If for some reason you do not agree with the way we wrote the internals of the client,
		* by implementing a custom `ITransport`, you can circumvent all of it and introduce your own.
		*/
		public async Task InterfaceExplained()
		{
			/**
			* Transport is generically typed to a type that implements `IConnectionConfigurationValues`
			* This is the minimum `ITransport` needs to report back for the client to function.
			*
			* In the low level client, `ElasticLowLevelClient`, a `Transport` is instantiated like this:
			*/
			var lowLevelTransport = new Transport<ConnectionConfiguration>(new ConnectionConfiguration());

			/** and in the high level client, `ElasticClient`, like this */
			var highlevelTransport = new Transport<ConnectionSettings>(new ConnectionSettings());

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var inMemoryTransport = new Transport<ConnectionSettings>(new ConnectionSettings(connectionPool, new InMemoryConnection()));

			/**
			* The only two methods on `ITransport` are `Request()` and `RequestAsync()`; the default `ITransport` implementation is responsible for introducing
			* many of the building blocks in the client. If you feel that the defaults do not work for you then you can swap them out for your own
			* custom `ITransport` implementation and if you do, {github}/issues[please let us know] as we'd love to learn why you've go down this route!
			*/
			var response = inMemoryTransport.Request<SearchResponse<Project>>(
				HttpMethod.GET,
				"/_search",
				PostData.Serializable(new { query = new { match_all = new { } } }));

			response = await inMemoryTransport.RequestAsync<SearchResponse<Project>>(
				HttpMethod.GET,
				"/_search",
				default(CancellationToken),
				PostData.Serializable(new { query = new { match_all = new { } } }));
		}
	}
}
