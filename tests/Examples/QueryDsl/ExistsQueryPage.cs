// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class ExistsQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/exists-query.asciidoc:20")]
		public void Line20()
		{
			// tag::3342c69b2c2303247217532956fcce85[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Exists(e => e
						.Field("user")
					)
				)
			);
			// end::3342c69b2c2303247217532956fcce85[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""exists"": {
			            ""field"": ""user""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/exists-query.asciidoc:56")]
		public void Line56()
		{
			// tag::43af86de5e49aa06070092fffc138208[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => !q
					.Exists(e => e
						.Field("user")
					)
				)
			);
			// end::43af86de5e49aa06070092fffc138208[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"": {
			            ""must_not"": {
			                ""exists"": {
			                    ""field"": ""user""
			                }
			            }
			        }
			    }
			}", (e, body) =>
			{
				body["query"]["bool"]["must_not"].ToJArray();
			});
		}
	}
}
