using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class IndexBoostPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::393c6b7a2e8c3381530c41ff2f7c4991[]
			var response0 = new SearchResponse<object>();
			// end::393c6b7a2e8c3381530c41ff2f7c4991[]

			response0.MatchesExample(@"GET /_search
			{
			    ""indices_boost"" : {
			        ""index1"" : 1.4,
			        ""index2"" : 1.3
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line25()
		{
			// tag::fb8a4322825d26c4e7b41bd763b3d392[]
			var response0 = new SearchResponse<object>();
			// end::fb8a4322825d26c4e7b41bd763b3d392[]

			response0.MatchesExample(@"GET /_search
			{
			    ""indices_boost"" : [
			        { ""alias1"" : 1.4 },
			        { ""index*"" : 1.3 }
			    ]
			}");
		}
	}
}