using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class TopMetricsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:12")]
		public void Line12()
		{
			// tag::8fa80d369028e9ac0432f5c2d64ac574[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::8fa80d369028e9ac0432f5c2d64ac574[]

			response0.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {}}
			{""s"": 1, ""v"": 3.1415}
			{""index"": {}}
			{""s"": 2, ""v"": 1.0}
			{""index"": {}}
			{""s"": 3, ""v"": 2.71828}");

			response1.MatchesExample(@"POST /test/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metric"": {""field"": ""v""},
			        ""sort"": {""s"": ""desc""}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:82")]
		public void Line82()
		{
			// tag::2c45781caccfc50c0656802fb613a6ea[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2c45781caccfc50c0656802fb613a6ea[]

			response0.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {}}
			{""s"": 1, ""v"": 3.1415}
			{""index"": {}}
			{""s"": 2, ""v"": 1.0}
			{""index"": {}}
			{""s"": 3, ""v"": 2.71828}");

			response1.MatchesExample(@"POST /test/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metric"": {""field"": ""v""},
			        ""sort"": {""s"": ""desc""},
			        ""size"": 2
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:130")]
		public void Line130()
		{
			// tag::b63ce79ce4fa1bb9b99a789f4dcfef4e[]
			var response0 = new SearchResponse<object>();
			// end::b63ce79ce4fa1bb9b99a789f4dcfef4e[]

			response0.MatchesExample(@"PUT /test/_settings
			{
			  ""top_metrics_max_size"": 100
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:150")]
		public void Line150()
		{
			// tag::a4f89e46f108ddab069bd4b3f798f2c6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::a4f89e46f108ddab069bd4b3f798f2c6[]

			response0.MatchesExample(@"PUT /node
			{
			  ""mappings"": {
			    ""properties"": {
			      ""ip"": {""type"": ""ip""},
			      ""date"": {""type"": ""date""}
			    }
			  }
			}");

			response1.MatchesExample(@"POST /node/_bulk?refresh
			{""index"": {}}
			{""ip"": ""192.168.0.1"", ""date"": ""2020-01-01T01:01:01"", ""v"": 1}
			{""index"": {}}
			{""ip"": ""192.168.0.1"", ""date"": ""2020-01-01T02:01:01"", ""v"": 2}
			{""index"": {}}
			{""ip"": ""192.168.0.2"", ""date"": ""2020-01-01T02:01:01"", ""v"": 3}");

			response2.MatchesExample(@"POST /node/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""ip"": {
			      ""terms"": {
			        ""field"": ""ip""
			      },
			      ""aggs"": {
			        ""tm"": {
			          ""top_metrics"": {
			            ""metric"": {""field"": ""v""},
			            ""sort"": {""date"": ""desc""}
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:221")]
		public void Line221()
		{
			// tag::1ba79a9bfab9275c2095e720f5664fab[]
			var response0 = new SearchResponse<object>();
			// end::1ba79a9bfab9275c2095e720f5664fab[]

			response0.MatchesExample(@"POST /node/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""ip"": {
			      ""terms"": {
			        ""field"": ""ip"",
			        ""order"": {""tm.v"": ""desc""}
			      },
			      ""aggs"": {
			        ""tm"": {
			          ""top_metrics"": {
			            ""metric"": {""field"": ""v""},
			            ""sort"": {""date"": ""desc""}
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:282")]
		public void Line282()
		{
			// tag::b160996a6ab06abeed6899e63c2d192b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b160996a6ab06abeed6899e63c2d192b[]

			response0.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {""_index"": ""test1""}}
			{""s"": 1, ""v"": 3.1415}
			{""index"": {""_index"": ""test1""}}
			{""s"": 2, ""v"": 1}
			{""index"": {""_index"": ""test2""}}
			{""s"": 3.1, ""v"": 2.71828}");

			response1.MatchesExample(@"POST /test*/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metric"": {""field"": ""v""},
			        ""sort"": {""s"": ""asc""}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:322")]
		public void Line322()
		{
			// tag::efc492b00b90206ae795f9afda4a1307[]
			var response0 = new SearchResponse<object>();
			// end::efc492b00b90206ae795f9afda4a1307[]

			response0.MatchesExample(@"POST /test*/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metric"": {""field"": ""v""},
			        ""sort"": {""s"": {""order"": ""asc"", ""numeric_type"": ""double""}}
			      }
			    }
			  }
			}");
		}
	}
}