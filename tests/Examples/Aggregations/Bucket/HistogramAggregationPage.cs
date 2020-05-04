// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class HistogramAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/histogram-aggregation.asciidoc:28")]
		public void Line28()
		{
			// tag::322e1a8842fc5924b972a9a32c29c17a[]
			var response0 = new SearchResponse<object>();
			// end::322e1a8842fc5924b972a9a32c29c17a[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""prices"" : {
			            ""histogram"" : {
			                ""field"" : ""price"",
			                ""interval"" : 50
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/histogram-aggregation.asciidoc:86")]
		public void Line86()
		{
			// tag::0003e4064d004a341c193ddd5d82a07f[]
			var response0 = new SearchResponse<object>();
			// end::0003e4064d004a341c193ddd5d82a07f[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""prices"" : {
			            ""histogram"" : {
			                ""field"" : ""price"",
			                ""interval"" : 50,
			                ""min_doc_count"" : 1
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/histogram-aggregation.asciidoc:161")]
		public void Line161()
		{
			// tag::c72bd866a7e21907fa71f1067371db55[]
			var response0 = new SearchResponse<object>();
			// end::c72bd866a7e21907fa71f1067371db55[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""constant_score"" : { ""filter"": { ""range"" : { ""price"" : { ""to"" : ""500"" } } } }
			    },
			    ""aggs"" : {
			        ""prices"" : {
			            ""histogram"" : {
			                ""field"" : ""price"",
			                ""interval"" : 50,
			                ""extended_bounds"" : {
			                    ""min"" : 0,
			                    ""max"" : 500
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/histogram-aggregation.asciidoc:213")]
		public void Line213()
		{
			// tag::e0bba0f00a589933499493390a9a0517[]
			var response0 = new SearchResponse<object>();
			// end::e0bba0f00a589933499493390a9a0517[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""prices"" : {
			            ""histogram"" : {
			                ""field"" : ""price"",
			                ""interval"" : 50,
			                ""keyed"" : true
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/histogram-aggregation.asciidoc:272")]
		public void Line272()
		{
			// tag::271c55d9a421dbc794caa0ebaead95e3[]
			var response0 = new SearchResponse<object>();
			// end::271c55d9a421dbc794caa0ebaead95e3[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""quantity"" : {
			             ""histogram"" : {
			                 ""field"" : ""quantity"",
			                 ""interval"": 10,
			                 ""missing"": 0 \<1>
			             }
			         }
			    }
			}");
		}
	}
}