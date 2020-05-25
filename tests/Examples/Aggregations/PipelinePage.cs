// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations
{
	public class PipelinePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline.asciidoc:54")]
		public void Line54()
		{
			// tag::ec20b1c236955a545476eeeea747d9de[]
			var response0 = new SearchResponse<object>();
			// end::ec20b1c236955a545476eeeea747d9de[]

			response0.MatchesExample(@"POST /_search
			{
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""timestamp"",
			                ""calendar_interval"":""day""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""lemmings"" } \<1>
			                },
			                ""the_deriv"":{
			                    ""derivative"":{ ""buckets_path"": ""the_sum"" } \<2>
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline.asciidoc:84")]
		public void Line84()
		{
			// tag::11be7655fdafcf4c1454a0e9ad8ddf63[]
			var response0 = new SearchResponse<object>();
			// end::11be7655fdafcf4c1454a0e9ad8ddf63[]

			response0.MatchesExample(@"POST /_search
			{
			    ""aggs"" : {
			        ""sales_per_month"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""month""
			            },
			            ""aggs"": {
			                ""sales"": {
			                    ""sum"": {
			                        ""field"": ""price""
			                    }
			                }
			            }
			        },
			        ""max_monthly_sales"": {
			            ""max_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"" \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline.asciidoc:119")]
		public void Line119()
		{
			// tag::88a6b6f721b91f0919127a34ee2fbe0e[]
			var response0 = new SearchResponse<object>();
			// end::88a6b6f721b91f0919127a34ee2fbe0e[]

			response0.MatchesExample(@"POST /_search
			{
			    ""aggs"" : {
			        ""sales_per_month"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""month""
			            },
			            ""aggs"": {
			                ""sale_type"": {
			                    ""terms"": {
			                        ""field"": ""type""
			                    },
			                    ""aggs"": {
			                        ""sales"": {
			                            ""sum"": {
			                                ""field"": ""price""
			                            }
			                        }
			                    }
			                },
			                ""hat_vs_bag_ratio"": {
			                    ""bucket_script"": {
			                        ""buckets_path"": {
			                            ""hats"": ""sale_type['hat']>sales"", \<1>
			                            ""bags"": ""sale_type['bag']>sales""  \<1>
			                        },
			                        ""script"": ""params.hats / params.bags""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline.asciidoc:168")]
		public void Line168()
		{
			// tag::f3dd309ab027e86048b476b54f0d4ca1[]
			var response0 = new SearchResponse<object>();
			// end::f3dd309ab027e86048b476b54f0d4ca1[]

			response0.MatchesExample(@"POST /_search
			{
			    ""aggs"": {
			        ""my_date_histo"": {
			            ""date_histogram"": {
			                ""field"":""timestamp"",
			                ""calendar_interval"":""day""
			            },
			            ""aggs"": {
			                ""the_deriv"": {
			                    ""derivative"": { ""buckets_path"": ""_count"" } \<1>
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline.asciidoc:194")]
		public void Line194()
		{
			// tag::2afc1231679898bd864d06679d9e951b[]
			var response0 = new SearchResponse<object>();
			// end::2afc1231679898bd864d06679d9e951b[]

			response0.MatchesExample(@"POST /sales/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""histo"": {
			      ""date_histogram"": {
			        ""field"": ""date"",
			        ""calendar_interval"": ""day""
			      },
			      ""aggs"": {
			        ""categories"": {
			          ""terms"": {
			            ""field"": ""category""
			          }
			        },
			        ""min_bucket_selector"": {
			          ""bucket_selector"": {
			            ""buckets_path"": {
			              ""count"": ""categories._bucket_count"" \<1>
			            },
			            ""script"": {
			              ""source"": ""params.count != 0""
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
