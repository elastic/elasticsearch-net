using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class HasPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line68()
		{
			// tag::9684e5fa8c22a07a372feb6fc1f5f7c0[]
			var response0 = new SearchResponse<object>();
			// end::9684e5fa8c22a07a372feb6fc1f5f7c0[]

			response0.MatchesExample(@"GET /_security/user/_has_privileges
			{
			  ""cluster"": [ ""monitor"", ""manage"" ],
			  ""index"" : [
			    {
			      ""names"": [ ""suppliers"", ""products"" ],
			      ""privileges"": [ ""read"" ]
			    },
			    {
			      ""names"": [ ""inventory"" ],
			      ""privileges"" : [ ""read"", ""write"" ]
			    }
			  ],
			  ""application"": [
			    {
			      ""application"": ""inventory_manager"",
			      ""privileges"" : [ ""read"", ""data:write/inventory"" ],
			      ""resources"" : [ ""product/1852563"" ]
			    }
			  ]
			}");
		}
	}
}