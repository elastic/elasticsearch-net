// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class SamplerAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/sampler-aggregation.asciidoc:19")]
		public void Line19()
		{
			// tag::28035a0e2a874f1b6739badf82a0ecc6[]
			var response0 = new SearchResponse<object>();
			// end::28035a0e2a874f1b6739badf82a0ecc6[]

			response0.MatchesExample(@"POST /stackoverflow/_search?size=0
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""tags:kibana OR tags:javascript""
			        }
			    },
			    ""aggs"": {
			        ""sample"": {
			            ""sampler"": {
			                ""shard_size"": 200
			            },
			            ""aggs"": {
			                ""keywords"": {
			                    ""significant_terms"": {
			                        ""field"": ""tags"",
			                        ""exclude"": [""kibana"", ""javascript""]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/sampler-aggregation.asciidoc:88")]
		public void Line88()
		{
			// tag::279f7af39b62c7d278f9f10b1f107dc0[]
			var response0 = new SearchResponse<object>();
			// end::279f7af39b62c7d278f9f10b1f107dc0[]

			response0.MatchesExample(@"POST /stackoverflow/_search?size=0
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""tags:kibana OR tags:javascript""
			        }
			    },
			    ""aggs"": {
			             ""low_quality_keywords"": {
			                ""significant_terms"": {
			                    ""field"": ""tags"",
			                    ""size"": 3,
			                    ""exclude"":[""kibana"", ""javascript""]
			                }
			        }
			    }
			}");
		}
	}
}
