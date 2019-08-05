using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class WeightedAvgAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line55()
		{
			// tag::c15dead46d351f62cfc066f1ca1a24eb[]
			var response0 = new SearchResponse<object>();
			// end::c15dead46d351f62cfc066f1ca1a24eb[]

			response0.MatchesExample(@"POST /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""weighted_grade"": {
			            ""weighted_avg"": {
			                ""value"": {
			                    ""field"": ""grade""
			                },
			                ""weight"": {
			                    ""field"": ""weight""
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line102()
		{
			// tag::4c15a4b054c7d0aaaa17deaff853bb28[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4c15a4b054c7d0aaaa17deaff853bb28[]

			response0.MatchesExample(@"POST /exams/_doc?refresh
			{
			    ""grade"": [1, 2, 3],
			    ""weight"": 2
			}");

			response1.MatchesExample(@"POST /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""weighted_grade"": {
			            ""weighted_avg"": {
			                ""value"": {
			                    ""field"": ""grade""
			                },
			                ""weight"": {
			                    ""field"": ""weight""
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line153()
		{
			// tag::e88e8c78ed50936c8b7436c90b988ddf[]
			var response0 = new SearchResponse<object>();
			// end::e88e8c78ed50936c8b7436c90b988ddf[]

			response0.MatchesExample(@"POST /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""weighted_grade"": {
			            ""weighted_avg"": {
			                ""value"": {
			                    ""script"": ""doc.grade.value + 1""
			                },
			                ""weight"": {
			                    ""script"": ""doc.weight.value + 1""
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line186()
		{
			// tag::cebfe0fed62091eb38b6348c89643f89[]
			var response0 = new SearchResponse<object>();
			// end::cebfe0fed62091eb38b6348c89643f89[]

			response0.MatchesExample(@"POST /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""weighted_grade"": {
			            ""weighted_avg"": {
			                ""value"": {
			                    ""field"": ""grade"",
			                    ""missing"": 2
			                },
			                ""weight"": {
			                    ""field"": ""weight"",
			                    ""missing"": 3
			                }
			            }
			        }
			    }
			}");
		}
	}
}