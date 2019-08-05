using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class ValuecountAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line10()
		{
			// tag::5dd695679b5141d9142d3d30ba8d300a[]
			var response0 = new SearchResponse<object>();
			// end::5dd695679b5141d9142d3d30ba8d300a[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""types_count"" : { ""value_count"" : { ""field"" : ""type"" } }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line44()
		{
			// tag::3722cb3705b6bc7f486969deace3dd83[]
			var response0 = new SearchResponse<object>();
			// end::3722cb3705b6bc7f486969deace3dd83[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""type_count"" : {
			            ""value_count"" : {
			                ""script"" : {
			                    ""source"" : ""doc['type'].value""
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line64()
		{
			// tag::213ab768f1b6a895e09403a0880e259a[]
			var response0 = new SearchResponse<object>();
			// end::213ab768f1b6a895e09403a0880e259a[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""types_count"" : {
			            ""value_count"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"" : {
			                        ""field"" : ""type""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}