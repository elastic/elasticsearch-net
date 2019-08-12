using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class StatsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::6f04f3c1afe94e03d26ff5966fd4b98d[]
			var response0 = new SearchResponse<object>();
			// end::6f04f3c1afe94e03d26ff5966fd4b98d[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : { ""stats"" : { ""field"" : ""grade"" } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line50()
		{
			// tag::9ed80262680e67c629a08f6754a7c5c9[]
			var response0 = new SearchResponse<object>();
			// end::9ed80262680e67c629a08f6754a7c5c9[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			             ""stats"" : {
			                 ""script"" : {
			                     ""lang"": ""painless"",
			                     ""source"": ""doc['grade'].value""
			                 }
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line71()
		{
			// tag::2ba8575100b37b85d0052d46a00ce4cd[]
			var response0 = new SearchResponse<object>();
			// end::2ba8575100b37b85d0052d46a00ce4cd[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""stats"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"" : {
			                        ""field"" : ""grade""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line96()
		{
			// tag::1341888a2677cf6e1db11e6cab2dd8ce[]
			var response0 = new SearchResponse<object>();
			// end::1341888a2677cf6e1db11e6cab2dd8ce[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""stats"" : {
			                ""field"" : ""grade"",
			                ""script"" : {
			                    ""lang"": ""painless"",
			                    ""source"": ""_value * params.correction"",
			                    ""params"" : {
			                        ""correction"" : 1.2
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line125()
		{
			// tag::7371dcfe4adb43996f4c26684318302b[]
			var response0 = new SearchResponse<object>();
			// end::7371dcfe4adb43996f4c26684318302b[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""stats"" : {
			                ""field"" : ""grade"",
			                ""missing"": 0 \<1>
			            }
			        }
			    }
			}");
		}
	}
}