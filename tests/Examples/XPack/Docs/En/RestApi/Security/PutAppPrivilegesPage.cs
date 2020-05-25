// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class PutAppPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/put-app-privileges.asciidoc:88")]
		public void Line88()
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
		[Description("../../x-pack/docs/en/rest-api/security/put-app-privileges.asciidoc:130")]
		public void Line130()
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
