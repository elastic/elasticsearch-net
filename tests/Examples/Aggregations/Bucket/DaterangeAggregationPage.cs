// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class DaterangeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/daterange-aggregation.asciidoc:16")]
		public void Line16()
		{
			// tag::a27c42ae4897ee6d2f6be3ddf80a8b3e[]
			var response0 = new SearchResponse<object>();
			// end::a27c42ae4897ee6d2f6be3ddf80a8b3e[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"": {
			        ""range"": {
			            ""date_range"": {
			                ""field"": ""date"",
			                ""format"": ""MM-yyyy"",
			                ""ranges"": [
			                    { ""to"": ""now-10M/M"" }, \<1>
			                    { ""from"": ""now-10M/M"" } \<2>
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/daterange-aggregation.asciidoc:78")]
		public void Line78()
		{
			// tag::a6ef8cd8c8218d547727ffc5485bfbd7[]
			var response0 = new SearchResponse<object>();
			// end::a6ef8cd8c8218d547727ffc5485bfbd7[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			   ""aggs"": {
			       ""range"": {
			           ""date_range"": {
			               ""field"": ""date"",
			               ""missing"": ""1976/11/30"",
			               ""ranges"": [
			                  {
			                    ""key"": ""Older"",
			                    ""to"": ""2016/02/01""
			                  }, \<1>
			                  {
			                    ""key"": ""Newer"",
			                    ""from"": ""2016/02/01"",
			                    ""to"" : ""now/d""
			                  }
			              ]
			          }
			      }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/daterange-aggregation.asciidoc:269")]
		public void Line269()
		{
			// tag::901d66919e584515717bf78ab5ca2cbb[]
			var response0 = new SearchResponse<object>();
			// end::901d66919e584515717bf78ab5ca2cbb[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			   ""aggs"": {
			       ""range"": {
			           ""date_range"": {
			               ""field"": ""date"",
			               ""time_zone"": ""CET"",
			               ""ranges"": [
			                  { ""to"": ""2016/02/01"" }, \<1>
			                  { ""from"": ""2016/02/01"", ""to"" : ""now/d"" }, \<2>
			                  { ""from"": ""now/d"" }
			              ]
			          }
			      }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/daterange-aggregation.asciidoc:298")]
		public void Line298()
		{
			// tag::83721157085b4e5a8a5ed3ede88b3690[]
			var response0 = new SearchResponse<object>();
			// end::83721157085b4e5a8a5ed3ede88b3690[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"": {
			        ""range"": {
			            ""date_range"": {
			                ""field"": ""date"",
			                ""format"": ""MM-yyy"",
			                ""ranges"": [
			                    { ""to"": ""now-10M/M"" },
			                    { ""from"": ""now-10M/M"" }
			                ],
			                ""keyed"": true
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/daterange-aggregation.asciidoc:347")]
		public void Line347()
		{
			// tag::2d1c675b3cb93119219a13db93262c1e[]
			var response0 = new SearchResponse<object>();
			// end::2d1c675b3cb93119219a13db93262c1e[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"": {
			        ""range"": {
			            ""date_range"": {
			                ""field"": ""date"",
			                ""format"": ""MM-yyy"",
			                ""ranges"": [
			                    { ""from"": ""01-2015"",  ""to"": ""03-2015"", ""key"": ""quarter_01"" },
			                    { ""from"": ""03-2015"", ""to"": ""06-2015"", ""key"": ""quarter_02"" }
			                ],
			                ""keyed"": true
			            }
			        }
			    }
			}");
		}
	}
}
