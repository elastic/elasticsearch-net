// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-users.asciidoc:56")]
		public void Line56()
		{
			// tag::3924ee252581ebb96ac0e60046125ae8[]
			var response0 = new SearchResponse<object>();
			// end::3924ee252581ebb96ac0e60046125ae8[]

			response0.MatchesExample(@"GET /_security/user/jacknich");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-users.asciidoc:80")]
		public void Line80()
		{
			// tag::abdbc81e799e28c833556b1c29f03ba6[]
			var response0 = new SearchResponse<object>();
			// end::abdbc81e799e28c833556b1c29f03ba6[]

			response0.MatchesExample(@"GET /_security/user");
		}
	}
}
