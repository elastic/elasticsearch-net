// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class RangeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:10")]
		public void Line10()
		{
			// tag::e84a496049274a0fed24e319da7a864c[]
			var response0 = new SearchResponse<object>();
			// end::e84a496049274a0fed24e319da7a864c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""price_ranges"" : {
			            ""range"" : {
			                ""field"" : ""price"",
			                ""ranges"" : [
			                    { ""to"" : 100.0 },
			                    { ""from"" : 100.0, ""to"" : 200.0 },
			                    { ""from"" : 200.0 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:67")]
		public void Line67()
		{
			// tag::d637c754aec195a1df39cafca49cbe7e[]
			var response0 = new SearchResponse<object>();
			// end::d637c754aec195a1df39cafca49cbe7e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""price_ranges"" : {
			            ""range"" : {
			                ""field"" : ""price"",
			                ""keyed"" : true,
			                ""ranges"" : [
			                    { ""to"" : 100 },
			                    { ""from"" : 100, ""to"" : 200 },
			                    { ""from"" : 200 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:120")]
		public void Line120()
		{
			// tag::4d147b4a4dabef9b0a8a13cbe8174e09[]
			var response0 = new SearchResponse<object>();
			// end::4d147b4a4dabef9b0a8a13cbe8174e09[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""price_ranges"" : {
			            ""range"" : {
			                ""field"" : ""price"",
			                ""keyed"" : true,
			                ""ranges"" : [
			                    { ""key"" : ""cheap"", ""to"" : 100 },
			                    { ""key"" : ""average"", ""from"" : 100, ""to"" : 200 },
			                    { ""key"" : ""expensive"", ""from"" : 200 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:178")]
		public void Line178()
		{
			// tag::bdf31f63d0941a4183ceae1cc2342c39[]
			var response0 = new SearchResponse<object>();
			// end::bdf31f63d0941a4183ceae1cc2342c39[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""price_ranges"" : {
			            ""range"" : {
			                ""script"" : {
			                    ""lang"": ""painless"",
			                    ""source"": ""doc['price'].value""
			                },
			                ""ranges"" : [
			                    { ""to"" : 100 },
			                    { ""from"" : 100, ""to"" : 200 },
			                    { ""from"" : 200 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:202")]
		public void Line202()
		{
			// tag::4c9c453c92431a05b413bfc0163104b4[]
			var response0 = new SearchResponse<object>();
			// end::4c9c453c92431a05b413bfc0163104b4[]

			response0.MatchesExample(@"POST /_scripts/convert_currency
			{
			  ""script"": {
			    ""lang"": ""painless"",
			    ""source"": ""doc[params.field].value * params.conversion_rate""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:216")]
		public void Line216()
		{
			// tag::7a6d758654eecbc3a1a76744b4de0a23[]
			var response0 = new SearchResponse<object>();
			// end::7a6d758654eecbc3a1a76744b4de0a23[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""price_ranges"" : {
			            ""range"" : {
			                ""script"" : {
			                    ""id"": ""convert_currency"", \<1>
			                    ""params"": { \<2>
			                        ""field"": ""price"",
			                        ""conversion_rate"": 0.835526591
			                    }
			                },
			                ""ranges"" : [
			                    { ""from"" : 0, ""to"" : 100 },
			                    { ""from"" : 100 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:275")]
		public void Line275()
		{
			// tag::022956b81fa70e72b56c66be16d0e982[]
			var response0 = new SearchResponse<object>();
			// end::022956b81fa70e72b56c66be16d0e982[]

			response0.MatchesExample(@"GET /sales/_search
			{
			    ""aggs"" : {
			        ""price_ranges"" : {
			            ""range"" : {
			                ""field"" : ""price"",
			                ""script"" : {
			                    ""source"": ""_value * params.conversion_rate"",
			                    ""params"" : {
			                        ""conversion_rate"" : 0.8
			                    }
			                },
			                ""ranges"" : [
			                    { ""to"" : 35 },
			                    { ""from"" : 35, ""to"" : 70 },
			                    { ""from"" : 70 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-aggregation.asciidoc:305")]
		public void Line305()
		{
			// tag::3b52f4f7ea4abfa6db6bf54199b15f53[]
			var response0 = new SearchResponse<object>();
			// end::3b52f4f7ea4abfa6db6bf54199b15f53[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""price_ranges"" : {
			            ""range"" : {
			                ""field"" : ""price"",
			                ""ranges"" : [
			                    { ""to"" : 100 },
			                    { ""from"" : 100, ""to"" : 200 },
			                    { ""from"" : 200 }
			                ]
			            },
			            ""aggs"" : {
			                ""price_stats"" : {
			                    ""stats"" : { ""field"" : ""price"" }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
