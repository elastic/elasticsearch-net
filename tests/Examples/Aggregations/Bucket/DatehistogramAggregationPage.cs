// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Elasticsearch.Net;

namespace Examples.Aggregations.Bucket
{
	public class DatehistogramAggregationPage : ExampleBase
	{
		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:119")]
		public void Line119()
		{
			// tag::b789292f9cf63ce912e058c46d90ce20[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("sales_over_time", d => d
						.Field("date")
						.CalendarInterval(DateInterval.Month)
					)
				)
			);
			// end::b789292f9cf63ce912e058c46d90ce20[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""month""
			            }
			        }
			    }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:138")]
		public void Line138()
		{
			// tag::73e5c88ad1488b213fb278ee1cb42289[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("sales_over_time", d => d
						.Field("date")
						.CalendarInterval("2d")
					)
				)
			);
			// end::73e5c88ad1488b213fb278ee1cb42289[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""2d""
			            }
			        }
			    }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:214")]
		public void Line214()
		{
			// tag::09ecba5814d71e4c44468575eada9878[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("sales_over_time", d => d
						.Field("date")
						.FixedInterval("30d")
					)
				)
			);
			// end::09ecba5814d71e4c44468575eada9878[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""fixed_interval"" : ""30d""
			            }
			        }
			    }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U(Skip="w units are not valid for fixed interval")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:232")]
		public void Line232()
		{
			// tag::2bb2339ac055337abf753bddb7771659[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("sales_over_time", d => d
						.Field("date")
						.FixedInterval("2w")
					)
				)
			);
			// end::2bb2339ac055337abf753bddb7771659[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
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

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:303")]
		public void Line303()
		{
			// tag::8a355eb25d2a01ba62dc1a22dd46f46f[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("sales_over_time", d => d
						.Field("date")
						.CalendarInterval("1M")
						.Format("yyyy-MM-dd")
					)
				)
			);
			// end::8a355eb25d2a01ba62dc1a22dd46f46f[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
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
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:380")]
		public void Line380()
		{
			// tag::70f0aa5853697e265ef3b1df72940951[]
			var indexResponse1 = client.Index(new
			{
				date = "2015-10-01T00:30:00Z"
			}, i => i.Index("my_index").Id(1).Refresh(Refresh.True));

			var indexResponse2 = client.Index(new
			{
				date = "2015-10-01T01:30:00Z"
			}, i => i.Index("my_index").Id(2).Refresh(Refresh.True));

			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("by_day", d => d
						.Field("date")
						.CalendarInterval(DateInterval.Day)
					)
				)
			);
			// end::70f0aa5853697e265ef3b1df72940951[]

			indexResponse1.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""date"": ""2015-10-01T00:30:00Z""
			}", e => e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true"));

			indexResponse2.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""date"": ""2015-10-01T01:30:00Z""
			}", e => e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true"));

			searchResponse.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""date_histogram"": {
			        ""field"":     ""date"",
			        ""calendar_interval"":  ""day""
			      }
			    }
			  }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:431")]
		public void Line431()
		{
			// tag::8de3206f80e18185a5ad6481f4c2ee07[]
			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("by_day", d => d
						.Field("date")
						.CalendarInterval(DateInterval.Day)
						.TimeZone("-01:00")
					)
				)
			);
			// end::8de3206f80e18185a5ad6481f4c2ee07[]

			searchResponse.MatchesExample(@"GET my_index/_search?size=0
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
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:502")]
		public void Line502()
		{
			// tag::aa6bfe54e2436eb668091fe31c2fbf4d[]
			var indexResponse1 = client.Index(new
			{
				date = "2015-10-01T05:30:00Z"
			}, i => i.Index("my_index").Id(1).Refresh(Refresh.True));

			var indexResponse2 = client.Index(new
			{
				date = "2015-10-01T06:30:00Z"
			}, i => i.Index("my_index").Id(2).Refresh(Refresh.True));

			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("by_day", d => d
						.Field("date")
						.CalendarInterval(DateInterval.Day)
						.Offset("+6h")
					)
				)
			);
			// end::aa6bfe54e2436eb668091fe31c2fbf4d[]

			indexResponse1.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""date"": ""2015-10-01T05:30:00Z""
			}", e => e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true"));

			indexResponse2.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""date"": ""2015-10-01T06:30:00Z""
			}", e => e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true"));

			searchResponse.MatchesExample(@"GET my_index/_search?size=0
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
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U(Skip = "Keyed is not supported by the client as deserialization of buckets does not support the keyed format")]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:567")]
		public void Line567()
		{
			// tag::9524a9b7373fa4eb2905183b0e806962[]
			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("by_day", d => d
						.Field("date")
						.CalendarInterval(new DateMathTime(1, DateMathTimeUnit.Month))
						.TimeZone("yyyy-MM-dd")
						//.Keyed()
					)
				)
			);
			// end::9524a9b7373fa4eb2905183b0e806962[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
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
			}", (e, b) =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:636")]
		public void Line636()
		{
			// tag::39a6a038c4b551022afe83de0523634e[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("sale_date", d => d
						.Field("date")
						.CalendarInterval(DateInterval.Year)
						.Missing(new DateTime(2000, 1, 1))
					)
				)
			);
			// end::39a6a038c4b551022afe83de0523634e[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
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
			}", (e, b) =>
			{
				e.Uri.Query = e.Uri.Query.Replace("size=0", string.Empty);
				b["size"] = 0;
				b["aggs"]["sale_date"]["date_histogram"]["missing"] = "2000-01-01T00:00:00";
			});
		}

		[U]
		[Description("aggregations/bucket/datehistogram-aggregation.asciidoc:669")]
		public void Line669()
		{
			// tag::6faf10a73f7d5fffbcb037bdb2cbaff8[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.Terms("dayOfWeek", d => d
						.Script(sc => sc
							.Source("doc['date'].value.dayOfWeekEnum.value")
							.Lang("painless")
						)
					)
				)
			);
			// end::6faf10a73f7d5fffbcb037bdb2cbaff8[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
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
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}
	}
}
