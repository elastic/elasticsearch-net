// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class ScrollPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:40")]
		public void Line40()
		{
			// tag::7e52bec09624cf6c0de5d13f2bfad5a5[]
			var response0 = new SearchResponse<object>();
			// end::7e52bec09624cf6c0de5d13f2bfad5a5[]

			response0.MatchesExample(@"POST /twitter/_search?scroll=1m
			{
			    ""size"": 100,
			    ""query"": {
			        ""match"" : {
			            ""title"" : ""elasticsearch""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:58")]
		public void Line58()
		{
			// tag::b41dce56b0e640d32b1cf452f87cec17[]
			var response0 = new SearchResponse<object>();
			// end::b41dce56b0e640d32b1cf452f87cec17[]

			response0.MatchesExample(@"POST /_search/scroll \<1>
			{
			    ""scroll"" : ""1m"", \<2>
			    ""scroll_id"" : ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ=="" \<3>
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:90")]
		public void Line90()
		{
			// tag::d5dcddc6398b473b6ad9bce5c6adf986[]
			var response0 = new SearchResponse<object>();
			// end::d5dcddc6398b473b6ad9bce5c6adf986[]

			response0.MatchesExample(@"GET /_search?scroll=1m
			{
			  ""sort"": [
			    ""_doc""
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:143")]
		public void Line143()
		{
			// tag::72beebe779a258c225dee7b023e60c52[]
			var response0 = new SearchResponse<object>();
			// end::72beebe779a258c225dee7b023e60c52[]

			response0.MatchesExample(@"GET /_nodes/stats/indices/search");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:156")]
		public void Line156()
		{
			// tag::b0d64d0a554549e5b2808002a0725493[]
			var response0 = new SearchResponse<object>();
			// end::b0d64d0a554549e5b2808002a0725493[]

			response0.MatchesExample(@"DELETE /_search/scroll
			{
			    ""scroll_id"" : ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:167")]
		public void Line167()
		{
			// tag::3a700f836d8d5da1b656a876554028aa[]
			var response0 = new SearchResponse<object>();
			// end::3a700f836d8d5da1b656a876554028aa[]

			response0.MatchesExample(@"DELETE /_search/scroll
			{
			    ""scroll_id"" : [
			      ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ=="",
			      ""DnF1ZXJ5VGhlbkZldGNoBQAAAAAAAAABFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAAAxZrUllkUVlCa1NqNmRMaUhiQlZkMWFBAAAAAAAAAAIWa1JZZFFZQmtTajZkTGlIYkJWZDFhQQAAAAAAAAAFFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAABBZrUllkUVlCa1NqNmRMaUhiQlZkMWFB""
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:181")]
		public void Line181()
		{
			// tag::c2c21e2824fbf6b7198ede30419da82b[]
			var response0 = new SearchResponse<object>();
			// end::c2c21e2824fbf6b7198ede30419da82b[]

			response0.MatchesExample(@"DELETE /_search/scroll/_all");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:189")]
		public void Line189()
		{
			// tag::b94cee0f74f57742b3948f9b784dfdd4[]
			var response0 = new SearchResponse<object>();
			// end::b94cee0f74f57742b3948f9b784dfdd4[]

			response0.MatchesExample(@"DELETE /_search/scroll/DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==,DnF1ZXJ5VGhlbkZldGNoBQAAAAAAAAABFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAAAxZrUllkUVlCa1NqNmRMaUhiQlZkMWFBAAAAAAAAAAIWa1JZZFFZQmtTajZkTGlIYkJWZDFhQQAAAAAAAAAFFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAABBZrUllkUVlCa1NqNmRMaUhiQlZkMWFB");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:201")]
		public void Line201()
		{
			// tag::1027ab1ca767ac1428176ef4f84bfbcf[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1027ab1ca767ac1428176ef4f84bfbcf[]

			response0.MatchesExample(@"GET /twitter/_search?scroll=1m
			{
			    ""slice"": {
			        ""id"": 0, \<1>
			        ""max"": 2 \<2>
			    },
			    ""query"": {
			        ""match"" : {
			            ""title"" : ""elasticsearch""
			        }
			    }
			}");

			response1.MatchesExample(@"GET /twitter/_search?scroll=1m
			{
			    ""slice"": {
			        ""id"": 1,
			        ""max"": 2
			    },
			    ""query"": {
			        ""match"" : {
			            ""title"" : ""elasticsearch""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/scroll.asciidoc:263")]
		public void Line263()
		{
			// tag::fdcaba9547180439ff4b6275034a5170[]
			var response0 = new SearchResponse<object>();
			// end::fdcaba9547180439ff4b6275034a5170[]

			response0.MatchesExample(@"GET /twitter/_search?scroll=1m
			{
			    ""slice"": {
			        ""field"": ""date"",
			        ""id"": 0,
			        ""max"": 10
			    },
			    ""query"": {
			        ""match"" : {
			            ""title"" : ""elasticsearch""
			        }
			    }
			}");
		}
	}
}
