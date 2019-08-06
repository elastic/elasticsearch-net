using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class PutAppPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line79()
		{
			// tag::4ee31fd4ea6d18f32ec28b7fa433441d[]
			var response0 = new SearchResponse<object>();
			// end::4ee31fd4ea6d18f32ec28b7fa433441d[]

			response0.MatchesExample(@"PUT /_security/privilege
			{
			  ""myapp"": {
			    ""read"": {
			      ""actions"": [ \<1>
			        ""data:read/*"" , \<2>
			        ""action:login"" ],
			        ""metadata"": { \<3>
			          ""description"": ""Read access to myapp""
			        }
			      }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line125()
		{
			// tag::ee90d1fb22b59d30da339d825303b912[]
			var response0 = new SearchResponse<object>();
			// end::ee90d1fb22b59d30da339d825303b912[]

			response0.MatchesExample(@"PUT /_security/privilege
			{
			  ""app01"": {
			    ""read"": {
			      ""actions"": [ ""action:login"", ""data:read/*"" ]
			    },
			    ""write"": {
			      ""actions"": [ ""action:login"", ""data:write/*"" ]
			    }
			  },
			  ""app02"": {
			    ""all"": {
			      ""actions"": [ ""*"" ]
			    }
			  }
			}");
		}
	}
}