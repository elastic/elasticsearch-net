// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Threading;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.Extensions;
using Tests.Domain.Extensions;

// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Tests.ClientConcepts.LowLevel
{
	public class LowLevelResponseTypes
	{
		/**[[low-level-response-types]]
		 * === Low Level Client Response Types
		 *
		 */

		public static string Response()
		{
			return @"{
				""boolean"" : true,
				""string"" : ""v"",
				""number"" : 29,
				""array"" : [1, 2, 3, 4],
				""object"" : {
					""first"" : ""value1"",
					""second"" : ""value2"",
					""nested"" : { ""x"" : ""value3"" }
				},
				""array_of_objects"" : [
					{
						""first"" : ""value11"",
						""second"" : ""value12"",
						""nested"" : { ""x"" : ""value4"", ""z"" : [{""id"": 1}] }
					},
					{
						""first"" : ""value21"",
						""second"" : ""value22"",
						""nested"" : { ""x"" : ""value5"", ""z"" : [{""id"": 3}, {""id"": 2}] },
						""complex.nested"" : { ""x"" : ""value6"" }
					}
				]
			}";
		}

		public LowLevelResponseTypes()
		{
			var connection = new InMemoryConnection(Response().Utf8Bytes());
			this.Client = new ElasticClient(new ConnectionSettings(connection).ApplyDomainSettings());

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			this.LowLevelClient = new ElasticLowLevelClient(new ConnectionConfiguration(pool, connection, new LowLevelRequestResponseSerializer()));
		}

		private ElasticClient Client { get;  }
		private IElasticLowLevelClient LowLevelClient { get;  }


		[U] public void DynamicResponse()
		{
			/**[float]
			* === DynamicResponse
			*
			*/

			// this still uses Utf8Json as NEST uses it and makes it the default for low level client
			// However DynamicResponseBuilder asks the System.Text.Json serializer to deserialize DynmicResponse
			var response = Client.LowLevel.Search<DynamicResponse>(PostData.Empty);
			AssertOnResponse(response);

			// this uses System.Text.Json
			var responseLowLevel = LowLevelClient.Search<DynamicResponse>(PostData.Empty);
			AssertOnResponse(responseLowLevel);
		}

		private static void AssertOnResponse(DynamicResponse response)
		{
			response.Get<string>("object.first")
				.Should()
				.NotBeEmpty()
				.And.Be("value1");

			response.Get<string>("object._arbitrary_key_")
				.Should()
				.NotBeEmpty()
				.And.Be("first");

			response.Get<int>("array.1").Should().Be(2);
			response.Get<long>("array.1").Should().Be(2);
			response.Get<long>("number").Should().Be(29);
			response.Get<long?>("number").Should().Be(29);
			response.Get<long?>("number_does_not_exist").Should().Be(null);
			response.Get<long?>("number").Should().Be(29);

			response.Get<string>("array_of_objects.1.second")
				.Should()
				.NotBeEmpty()
				.And.Be("value22");

			response.Get<string>("array_of_objects.1.complex\\.nested.x")
				.Should()
				.NotBeEmpty()
				.And.Be("value6");

			/**
			 * You can project into arrays using the dot notation
			 */
			response.Get<string[]>("array_of_objects.first")
				.Should()
				.NotBeEmpty()
				.And.HaveCount(2)
				.And.BeEquivalentTo(new[] { "value11", "value21" });

			/**
			 * You can even peek into array of arrays
			 */
			var nestedZs = response.Get<int[][]>("array_of_objects.nested.z.id")
				//.SelectMany(d=>d.Get<int[]>("id"))
				.Should()
				.NotBeEmpty()
				.And.HaveCount(2)
				.And.BeEquivalentTo(new[] { new[] { 1 }, new[] { 3, 2 } });
		}
	}
}
