// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class CreateUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-users.asciidoc:108")]
		public void Line108()
		{
			// tag::4c514b787945952a223cde8a4a09e826[]
			var response0 = new SearchResponse<object>();
			// end::4c514b787945952a223cde8a4a09e826[]

			response0.MatchesExample(@"POST /_security/user/jacknich
			{
			  ""password"" : ""j@rV1s"",
			  ""roles"" : [ ""admin"", ""other_role1"" ],
			  ""full_name"" : ""Jack Nicholson"",
			  ""email"" : ""jacknich@example.com"",
			  ""metadata"" : {
			    ""intelligence"" : 7
			  }
			}");
		}
	}
}
