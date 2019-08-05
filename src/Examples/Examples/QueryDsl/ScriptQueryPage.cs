using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class ScriptQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line15()
		{
			// tag::b3aa46565d98f8a6750c571bb1c1bb8c[]
			var response0 = new SearchResponse<object>();
			// end::b3aa46565d98f8a6750c571bb1c1bb8c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""filter"" : {
			                ""script"" : {
			                    ""script"" : {
			                        ""source"": ""doc['num1'].value > 1"",
			                        ""lang"": ""painless""
			                     }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line53()
		{
			// tag::c4459f98de5decb37b8c403885f4b226[]
			var response0 = new SearchResponse<object>();
			// end::c4459f98de5decb37b8c403885f4b226[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""filter"" : {
			                ""script"" : {
			                    ""script"" : {
			                        ""source"" : ""doc['num1'].value > params.param1"",
			                        ""lang""   : ""painless"",
			                        ""params"" : {
			                            ""param1"" : 5
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}