using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class RangeQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line16()
		{
			// tag::97bcd92ef148312d41e69f0d18284327[]
			var response0 = new SearchResponse<object>();
			// end::97bcd92ef148312d41e69f0d18284327[]

			response0.MatchesExample(@"GET _search
			{
			    ""query"": {
			        ""range"" : {
			            ""age"" : {
			                ""gte"" : 10,
			                ""lte"" : 20,
			                ""boost"" : 2.0
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line152()
		{
			// tag::4466d410e06712c63328de4db249e6da[]
			var response0 = new SearchResponse<object>();
			// end::4466d410e06712c63328de4db249e6da[]

			response0.MatchesExample(@"GET _search
			{
			    ""query"": {
			        ""range"" : {
			            ""timestamp"" : {
			                ""gte"" : ""now-1d/d"",
			                ""lt"" :  ""now/d""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line214()
		{
			// tag::5d13a71fa7fda73b15111803b1c7cfd3[]
			var response0 = new SearchResponse<object>();
			// end::5d13a71fa7fda73b15111803b1c7cfd3[]

			response0.MatchesExample(@"GET _search
			{
			    ""query"": {
			        ""range"" : {
			            ""timestamp"" : {
			                ""time_zone"": ""+01:00"", \<1>
			                ""gte"": ""2015-01-01 00:00:00"", \<2>
			                ""lte"": ""now"" \<3>
			            }
			        }
			    }
			}");
		}
	}
}