using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Threading;
using Elastic.Xunit.XunitPlumbing;
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
			return $@"{{
			""boolean"" : true,
			""string"" : ""v"",
			""number"" : 29,
			""array"" : [1, 2, 3, 4],
			""object"" : {{
				""first"" : ""value1"",
				""second"" : ""value2"",
				""nested"" : {{ ""x"" : ""value3"" }}
			}},
			""array_of_objects"" : [
				{{
					""first"" : ""value11"",
					""second"" : ""value12"",
					""nested"" : {{ ""x"" : ""value4"", ""z"" : [{{""id"": 1}}] }}
				}},
				{{
					""first"" : ""value21"",
					""second"" : ""value22"",
					""nested"" : {{ ""x"" : ""value5"", ""z"" : [{{""id"": 3}}, {{""id"": 2}}] }},
					""complex.nested"" : {{ ""x"" : ""value6"" }}
				}}
			]
		}}";
		}

		public LowLevelResponseTypes()
		{
			var connection = new InMemoryConnection(Response().Utf8Bytes());
			this.Client = new ElasticClient(new ConnectionSettings(connection).ApplyDomainSettings());
		}

		private ElasticClient Client { get;  }


		[U] public void DynamicResponse()
		{
			/**[float]
			* === DynamicResponse
			*
			*/

			var response = Client.LowLevel.Search<DynamicResponse>(PostData.Empty);

			response.Get<string>("object.first").Should()
				.NotBeEmpty()
				.And.Be("value1");

			response.Get<string>("object._arbitrary_key_").Should()
				.NotBeEmpty()
				.And.Be("first");

			response.Get<int>("array.1").Should().Be(2);
			response.Get<long>("array.1").Should().Be(2);
			response.Get<long>("number").Should().Be(29);
			response.Get<long?>("number").Should().Be(29);
			response.Get<long?>("number_does_not_exist").Should().Be(null);
			response.Get<long?>("number").Should().Be(29);

			response.Get<string>("array_of_objects.1.second").Should()
				.NotBeEmpty()
				.And.Be("value22");

			response.Get<string>("array_of_objects.1.complex\\.nested.x").Should()
				.NotBeEmpty()
				.And.Be("value6");

			/**
			 * You can project into arrays using the dot notation
			 */
			response.Get<string[]>("array_of_objects.first").Should()
				.NotBeEmpty()
				.And.HaveCount(2)
				.And.BeEquivalentTo(new [] {"value11", "value21"});

			/**
			 * You can even peek into array of arrays
			 */
			var nestedZs = response.Get<int[][]>("array_of_objects.nested.z.id")
				//.SelectMany(d=>d.Get<int[]>("id"))
				.Should().NotBeEmpty()
				.And.HaveCount(2)
				.And.BeEquivalentTo(new [] { new [] {1}, new []{3,2}});


		}

	}
}
