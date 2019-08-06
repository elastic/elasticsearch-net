using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class DiversifiedSamplerAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line30()
		{
			// tag::3344c3478f1e8bbbef683757638a34f4[]
			var response0 = new SearchResponse<object>();
			// end::3344c3478f1e8bbbef683757638a34f4[]

			response0.MatchesExample(@"POST /stackoverflow/_search?size=0
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""tags:elasticsearch""
			        }
			    },
			    ""aggs"": {
			        ""my_unbiased_sample"": {
			            ""diversified_sampler"": {
			                ""shard_size"": 200,
			                ""field"" : ""author""
			            },
			            ""aggs"": {
			                ""keywords"": {
			                    ""significant_terms"": {
			                        ""field"": ""tags"",
			                        ""exclude"": [""elasticsearch""]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line96()
		{
			// tag::07afce825c09de17a3d73a02b17a0a97[]
			var response0 = new SearchResponse<object>();
			// end::07afce825c09de17a3d73a02b17a0a97[]

			response0.MatchesExample(@"POST /stackoverflow/_search?size=0
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""tags:kibana""
			        }
			    },
			    ""aggs"": {
			        ""my_unbiased_sample"": {
			            ""diversified_sampler"": {
			                ""shard_size"": 200,
			                ""max_docs_per_value"" : 3,
			                ""script"" : {
			                    ""lang"": ""painless"",
			                    ""source"": ""doc['tags'].hashCode()""
			                }
			            },
			            ""aggs"": {
			                ""keywords"": {
			                    ""significant_terms"": {
			                        ""field"": ""tags"",
			                        ""exclude"": [""kibana""]
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}