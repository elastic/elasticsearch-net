// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class TopMetricsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:10")]
		public void Line10()
		{
			// tag::9d1fb129ac783355a20097effded1845[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9d1fb129ac783355a20097effded1845[]

			response0.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {}}
			{""s"": 1, ""m"": 3.1415}
			{""index"": {}}
			{""s"": 2, ""m"": 1.0}
			{""index"": {}}
			{""s"": 3, ""m"": 2.71828}");

			response1.MatchesExample(@"POST /test/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metrics"": {""field"": ""m""},
			        ""sort"": {""s"": ""desc""}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:74")]
		public void Line74()
		{
			// tag::73882fb729426f186221b5ebbd8f571f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::73882fb729426f186221b5ebbd8f571f[]

			response0.MatchesExample(@"PUT /test
			{
			  ""mappings"": {
			    ""properties"": {
			      ""d"": {""type"": ""date""}
			    }
			  }
			}");

			response1.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {}}
			{""s"": 1, ""m"": 3.1415, ""i"": 1, ""d"": ""2020-01-01T00:12:12Z""}
			{""index"": {}}
			{""s"": 2, ""m"": 1.0, ""i"": 6, ""d"": ""2020-01-02T00:12:12Z""}
			{""index"": {}}
			{""s"": 3, ""m"": 2.71828, ""i"": -12, ""d"": ""2019-12-31T00:12:12Z""}");

			response2.MatchesExample(@"POST /test/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metrics"": [
			          {""field"": ""m""},
			          {""field"": ""i""},
			          {""field"": ""d""}
			        ],
			        ""sort"": {""s"": ""desc""}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:133")]
		public void Line133()
		{
			// tag::6013ed65d2058da5ce704b47a504b60a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::6013ed65d2058da5ce704b47a504b60a[]

			response0.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {}}
			{""s"": 1, ""m"": 3.1415}
			{""index"": {}}
			{""s"": 2, ""m"": 1.0}
			{""index"": {}}
			{""s"": 3, ""m"": 2.71828}");

			response1.MatchesExample(@"POST /test/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metrics"": {""field"": ""m""},
			        ""sort"": {""s"": ""desc""},
			        ""size"": 3
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:183")]
		public void Line183()
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
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:202")]
		public void Line202()
		{
			// tag::5b3384992c398ea8a3064d2e08725e2b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::5b3384992c398ea8a3064d2e08725e2b[]

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
			{""ip"": ""192.168.0.1"", ""date"": ""2020-01-01T01:01:01"", ""m"": 1}
			{""index"": {}}
			{""ip"": ""192.168.0.1"", ""date"": ""2020-01-01T02:01:01"", ""m"": 2}
			{""index"": {}}
			{""ip"": ""192.168.0.2"", ""date"": ""2020-01-01T02:01:01"", ""m"": 3}");

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
			            ""metrics"": {""field"": ""m""},
			            ""sort"": {""date"": ""desc""}
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:273")]
		public void Line273()
		{
			// tag::4af15c4f26ddefb9c350e7a246a66a15[]
			var response0 = new SearchResponse<object>();
			// end::4af15c4f26ddefb9c350e7a246a66a15[]

			response0.MatchesExample(@"POST /node/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""ip"": {
			      ""terms"": {
			        ""field"": ""ip"",
			        ""order"": {""tm.m"": ""desc""}
			      },
			      ""aggs"": {
			        ""tm"": {
			          ""top_metrics"": {
			            ""metrics"": {""field"": ""m""},
			            ""sort"": {""date"": ""desc""}
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:334")]
		public void Line334()
		{
			// tag::2d2f5ec97aa34ff7822a6a1ed08ef335[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2d2f5ec97aa34ff7822a6a1ed08ef335[]

			response0.MatchesExample(@"POST /test/_bulk?refresh
			{""index"": {""_index"": ""test1""}}
			{""s"": 1, ""m"": 3.1415}
			{""index"": {""_index"": ""test1""}}
			{""s"": 2, ""m"": 1}
			{""index"": {""_index"": ""test2""}}
			{""s"": 3.1, ""m"": 2.71828}");

			response1.MatchesExample(@"POST /test*/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metrics"": {""field"": ""m""},
			        ""sort"": {""s"": ""asc""}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/top-metrics-aggregation.asciidoc:374")]
		public void Line374()
		{
			// tag::56da9c55774f4c2e8eadde0579bdc60c[]
			var response0 = new SearchResponse<object>();
			// end::56da9c55774f4c2e8eadde0579bdc60c[]

			response0.MatchesExample(@"POST /test*/_search?filter_path=aggregations
			{
			  ""aggs"": {
			    ""tm"": {
			      ""top_metrics"": {
			        ""metrics"": {""field"": ""m""},
			        ""sort"": {""s"": {""order"": ""asc"", ""numeric_type"": ""double""}}
			      }
			    }
			  }
			}");
		}
	}
}
