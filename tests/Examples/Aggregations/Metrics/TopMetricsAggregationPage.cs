using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class TopMetricsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:12")]
		public void Line12()
		{
			// tag::d0321ef6bc9aae91b7fcfb76af085404[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::d0321ef6bc9aae91b7fcfb76af085404[]

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
			        ""metrics"": {""field"": ""v""},
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
			// tag::ba2af8de92d3d197d809e2b9a9580ea5[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ba2af8de92d3d197d809e2b9a9580ea5[]

			response0.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {}}
			{""s"": 1, ""v"": 3.1415, ""m"": 1.9}
			{""index"": {}}
			{""s"": 2, ""v"": 1.0, ""m"": 6.7}
			{""index"": {}}
			{""s"": 3, ""v"": 2.71828, ""m"": -12.2}");

			response1.MatchesExample(@"POST /test/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metrics"": [
			          {""field"": ""v""},
			          {""field"": ""m""}
			        ],
			        ""sort"": {""s"": ""desc""}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:132")]
		public void Line132()
		{
			// tag::c4f657319298f90db11c954bb09403da[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::c4f657319298f90db11c954bb09403da[]

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
			        ""metrics"": {""field"": ""v""},
			        ""sort"": {""s"": ""desc""},
			        ""size"": 2
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:130")]
		public void Line180()
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
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:200")]
		public void Line200()
		{
			// tag::ed8c8e5006923f771aa4db0936184ac7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::ed8c8e5006923f771aa4db0936184ac7[]

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
			            ""metrics"": {""field"": ""v""},
			            ""sort"": {""date"": ""desc""}
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:271")]
		public void Line271()
		{
			// tag::8b1cc4f69975d3fcdaf8ef6d7d71cc81[]
			var response0 = new SearchResponse<object>();
			// end::8b1cc4f69975d3fcdaf8ef6d7d71cc81[]

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
			            ""metrics"": {""field"": ""v""},
			            ""sort"": {""date"": ""desc""}
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:332")]
		public void Line332()
		{
			// tag::315cae9fbbb552bcdf84ae3af6689489[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::315cae9fbbb552bcdf84ae3af6689489[]

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
			        ""metrics"": {""field"": ""v""},
			        ""sort"": {""s"": ""asc""}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:372")]
		public void Line372()
		{
			// tag::035805d6f35a2ef517b9cd9ae037da05[]
			var response0 = new SearchResponse<object>();
			// end::035805d6f35a2ef517b9cd9ae037da05[]

			response0.MatchesExample(@"POST /test*/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metrics"": {""field"": ""v""},
			        ""sort"": {""s"": {""order"": ""asc"", ""numeric_type"": ""double""}}
			      }
			    }
			  }
			}");
		}
	}
}