// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class DatehistogramAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:119")]
		public void Line119()
		{
			// tag::b789292f9cf63ce912e058c46d90ce20[]
			var response0 = new SearchResponse<object>();
			// end::b789292f9cf63ce912e058c46d90ce20[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""month""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:138")]
		public void Line138()
		{
			// tag::73e5c88ad1488b213fb278ee1cb42289[]
			var response0 = new SearchResponse<object>();
			// end::73e5c88ad1488b213fb278ee1cb42289[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""2d""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:214")]
		public void Line214()
		{
			// tag::09ecba5814d71e4c44468575eada9878[]
			var response0 = new SearchResponse<object>();
			// end::09ecba5814d71e4c44468575eada9878[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""fixed_interval"" : ""30d""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:232")]
		public void Line232()
		{
			// tag::2bb2339ac055337abf753bddb7771659[]
			var response0 = new SearchResponse<object>();
			// end::2bb2339ac055337abf753bddb7771659[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""fixed_interval"" : ""2w""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:303")]
		public void Line303()
		{
			// tag::8a355eb25d2a01ba62dc1a22dd46f46f[]
			var response0 = new SearchResponse<object>();
			// end::8a355eb25d2a01ba62dc1a22dd46f46f[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""1M"",
			                ""format"" : ""yyyy-MM-dd"" \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:380")]
		public void Line380()
		{
			// tag::70f0aa5853697e265ef3b1df72940951[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::70f0aa5853697e265ef3b1df72940951[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""date"": ""2015-10-01T00:30:00Z""
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""date"": ""2015-10-01T01:30:00Z""
			}");

			response2.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""date_histogram"": {
			        ""field"":     ""date"",
			        ""calendar_interval"":  ""day""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:431")]
		public void Line431()
		{
			// tag::8de3206f80e18185a5ad6481f4c2ee07[]
			var response0 = new SearchResponse<object>();
			// end::8de3206f80e18185a5ad6481f4c2ee07[]

			response0.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""date_histogram"": {
			        ""field"":     ""date"",
			        ""calendar_interval"":  ""day"",
			        ""time_zone"": ""-01:00""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:502")]
		public void Line502()
		{
			// tag::aa6bfe54e2436eb668091fe31c2fbf4d[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::aa6bfe54e2436eb668091fe31c2fbf4d[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""date"": ""2015-10-01T05:30:00Z""
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""date"": ""2015-10-01T06:30:00Z""
			}");

			response2.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""date_histogram"": {
			        ""field"":     ""date"",
			        ""calendar_interval"":  ""day"",
			        ""offset"":    ""+6h""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:567")]
		public void Line567()
		{
			// tag::9524a9b7373fa4eb2905183b0e806962[]
			var response0 = new SearchResponse<object>();
			// end::9524a9b7373fa4eb2905183b0e806962[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""1M"",
			                ""format"" : ""yyyy-MM-dd"",
			                ""keyed"": true
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:636")]
		public void Line636()
		{
			// tag::39a6a038c4b551022afe83de0523634e[]
			var response0 = new SearchResponse<object>();
			// end::39a6a038c4b551022afe83de0523634e[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sale_date"" : {
			             ""date_histogram"" : {
			                 ""field"" : ""date"",
			                 ""calendar_interval"": ""year"",
			                 ""missing"": ""2000/01/01"" \<1>
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:669")]
		public void Line669()
		{
			// tag::6faf10a73f7d5fffbcb037bdb2cbaff8[]
			var response0 = new SearchResponse<object>();
			// end::6faf10a73f7d5fffbcb037bdb2cbaff8[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"": {
			        ""dayOfWeek"": {
			            ""terms"": {
			                ""script"": {
			                    ""lang"": ""painless"",
			                    ""source"": ""doc['date'].value.dayOfWeekEnum.value""
			                }
			            }
			        }
			    }
			}");
		}
	}
}
