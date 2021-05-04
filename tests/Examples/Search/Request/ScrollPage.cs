// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using System.Linq;
using Elasticsearch.Net;
using Examples.Models;
using Newtonsoft.Json.Linq;

namespace Examples.Search.Request
{
	public class ScrollPage : ExampleBase
	{
		[U]
		[Description("search/request/scroll.asciidoc:45")]
		public void Line45()
		{
			// tag::7e52bec09624cf6c0de5d13f2bfad5a5[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Size(100)
				.Scroll("1m")
				.Query(q => q
					.Match(m => m
						.Field(f => f.Title)
						.Query("elasticsearch")
					)
				)
			);
			// end::7e52bec09624cf6c0de5d13f2bfad5a5[]

			searchResponse.MatchesExample(@"POST /twitter/_search?scroll=1m
			{
			    ""size"": 100,
			    ""query"": {
			        ""match"" : {
			            ""title"" : ""elasticsearch""
			        }
			    }
			}", (e, b) =>
			{
				b["query"]["match"]["title"].ToLongFormQuery();
			});
		}

		[U]
		[Description("search/request/scroll.asciidoc:63")]
		public void Line63()
		{
			// tag::b41dce56b0e640d32b1cf452f87cec17[]
			var searchResponse = client.Scroll<Tweet>(
				"1m",
				"DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==");
			// end::b41dce56b0e640d32b1cf452f87cec17[]

			searchResponse.MatchesExample(@"POST /_search/scroll \<1>
			{
			    ""scroll"" : ""1m"", \<2>
			    ""scroll_id"" : ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ=="" \<3>
			}");
		}

		[U]
		[Description("search/request/scroll.asciidoc:95")]
		public void Line95()
		{
			// tag::d5dcddc6398b473b6ad9bce5c6adf986[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.Scroll("1m")
				.Sort(so => so
					.Descending(SortSpecialField.DocumentIndexOrder)
				)
			);
			// end::d5dcddc6398b473b6ad9bce5c6adf986[]

			searchResponse.MatchesExample(@"GET /_search?scroll=1m
			{
			  ""sort"": [
			    ""_doc""
			  ]
			}", (e, b) =>
			{
				b["sort"][0] = new JObject
				{
					["_doc"] = new JObject
					{
						["order"] = "desc"
					}
				};
			});
		}

		[U]
		[Description("search/request/scroll.asciidoc:148")]
		public void Line148()
		{
			// tag::72beebe779a258c225dee7b023e60c52[]
			var nodesStatsResponse = client.Nodes.Stats(s => s
				.Metric(NodesStatsMetric.Indices)
				.IndexMetric(NodesStatsIndexMetric.Search)
			);
			// end::72beebe779a258c225dee7b023e60c52[]

			nodesStatsResponse.MatchesExample(@"GET /_nodes/stats/indices/search");
		}

		[U]
		[Description("search/request/scroll.asciidoc:161")]
		public void Line161()
		{
			// tag::b0d64d0a554549e5b2808002a0725493[]
			var clearScrollResponse = client.ClearScroll(c => c
				.ScrollId("DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==")
			);
			// end::b0d64d0a554549e5b2808002a0725493[]

			clearScrollResponse.MatchesExample(@"DELETE /_search/scroll
			{
			    ""scroll_id"" : ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==""
			}", (e, b) =>
			{
				var scrollId = b["scroll_id"];
				b["scroll_id"] = new JArray(scrollId);
			});
		}

		[U]
		[Description("search/request/scroll.asciidoc:172")]
		public void Line172()
		{
			// tag::3a700f836d8d5da1b656a876554028aa[]
			var clearScrollResponse = client.ClearScroll(c => c
				.ScrollId(
					"DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==",
					"DnF1ZXJ5VGhlbkZldGNoBQAAAAAAAAABFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAAAxZrUllkUVlCa1NqNmRMaUhiQlZkMWFBAAAAAAAAAAIWa1JZZFFZQmtTajZkTGlIYkJWZDFhQQAAAAAAAAAFFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAABBZrUllkUVlCa1NqNmRMaUhiQlZkMWFB")
			);
			// end::3a700f836d8d5da1b656a876554028aa[]

			clearScrollResponse.MatchesExample(@"DELETE /_search/scroll
			{
			    ""scroll_id"" : [
			      ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ=="",
			      ""DnF1ZXJ5VGhlbkZldGNoBQAAAAAAAAABFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAAAxZrUllkUVlCa1NqNmRMaUhiQlZkMWFBAAAAAAAAAAIWa1JZZFFZQmtTajZkTGlIYkJWZDFhQQAAAAAAAAAFFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAABBZrUllkUVlCa1NqNmRMaUhiQlZkMWFB""
			    ]
			}");
		}

		[U]
		[Description("search/request/scroll.asciidoc:186")]
		public void Line186()
		{
			// tag::c2c21e2824fbf6b7198ede30419da82b[]
			var clearScrollResponse = client.ClearScroll(c => c
				.ScrollId("_all") // <1> The client always sends `scroll_id` in the request body
			);
			// end::c2c21e2824fbf6b7198ede30419da82b[]

			clearScrollResponse.MatchesExample(@"DELETE /_search/scroll/_all", (e, b) =>
			{
				var index = e.Uri.Path.IndexOf("_all", StringComparison.Ordinal);
				var scrollId = e.Uri.Path.Substring(index);
				e.Uri.Path = e.Uri.Path.Substring(0, index);
				b["scroll_id"] = new JArray(scrollId);
			});
		}

		[U]
		[Description("search/request/scroll.asciidoc:194")]
		public void Line194()
		{
			// tag::b94cee0f74f57742b3948f9b784dfdd4[]
			var clearScrollResponse = client.ClearScroll(c => c
				.ScrollId(
					"DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==", // <1> The client always sends `scroll_id` in the request body
					"DnF1ZXJ5VGhlbkZldGNoBQAAAAAAAAABFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAAAxZrUllkUVlCa1NqNmRMaUhiQlZkMWFBAAAAAAAAAAIWa1JZZFFZQmtTajZkTGlIYkJWZDFhQQAAAAAAAAAFFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAABBZrUllkUVlCa1NqNmRMaUhiQlZkMWFB")
			);
			// end::b94cee0f74f57742b3948f9b784dfdd4[]

			clearScrollResponse.MatchesExample(@"DELETE /_search/scroll/DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==,DnF1ZXJ5VGhlbkZldGNoBQAAAAAAAAABFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAAAxZrUllkUVlCa1NqNmRMaUhiQlZkMWFBAAAAAAAAAAIWa1JZZFFZQmtTajZkTGlIYkJWZDFhQQAAAAAAAAAFFmtSWWRRWUJrU2o2ZExpSGJCVmQxYUEAAAAAAAAABBZrUllkUVlCa1NqNmRMaUhiQlZkMWFB",
				(e, b) =>
				{
					var index = e.Uri.Path.IndexOf("DX", StringComparison.Ordinal);
					var scrollId = e.Uri.Path.Substring(index).Split(",");
					e.Uri.Path = e.Uri.Path.Substring(0, index);
					b["scroll_id"] = new JArray(scrollId.Cast<object>());
				});
		}

		[U]
		[Description("search/request/scroll.asciidoc:206")]
		public void Line206()
		{
			// tag::1027ab1ca767ac1428176ef4f84bfbcf[]
			var searchResponse1 = client.Search<Tweet>(s => s
				.Index("twitter")
				.Scroll("1m")
				.Slice(sl => sl
					.Id(0)
					.Max(2)
				)
				.Query(q => q
					.Match(m => m
						.Field(f => f.Title)
						.Query("elasticsearch")
					)
				)
			);

			var searchResponse2 = client.Search<Tweet>(s => s
				.Index("twitter")
				.Scroll("1m")
				.Slice(sl => sl
					.Id(1)
					.Max(2)
				)
				.Query(q => q
					.Match(m => m
						.Field(f => f.Title)
						.Query("elasticsearch")
					)
				)
			);
			// end::1027ab1ca767ac1428176ef4f84bfbcf[]

			searchResponse1.MatchesExample(@"GET /twitter/_search?scroll=1m
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
			}", (e, b) =>
			{
				b["query"]["match"]["title"].ToLongFormQuery();
			});

			searchResponse2.MatchesExample(@"GET /twitter/_search?scroll=1m
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
			}", (e, b) =>
			{
				b["query"]["match"]["title"].ToLongFormQuery();
			});
		}

		[U]
		[Description("search/request/scroll.asciidoc:268")]
		public void Line268()
		{
			// tag::fdcaba9547180439ff4b6275034a5170[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Scroll("1m")
				.Slice(sl => sl
					.Field(f => f.Date)
					.Id(0)
					.Max(10)
				)
				.Query(q => q
					.Match(m => m
						.Field(f => f.Title)
						.Query("elasticsearch")
					)
				)
			);
			// end::fdcaba9547180439ff4b6275034a5170[]

			searchResponse.MatchesExample(@"GET /twitter/_search?scroll=1m
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
			}", (e, b) =>
			{
				b["query"]["match"]["title"].ToLongFormQuery();
			});
		}
	}
}
