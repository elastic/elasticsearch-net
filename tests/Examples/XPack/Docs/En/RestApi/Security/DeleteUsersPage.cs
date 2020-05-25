// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DeleteUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/delete-users.asciidoc:39")]
		public void Line39()
		{
			// tag::ffd63dd186ab81b893faec3b3358fa09[]
			var response0 = new SearchResponse<object>();
			// end::ffd63dd186ab81b893faec3b3358fa09[]

			response0.MatchesExample(@"DELETE /_security/user/jacknich");
		}
	}
}
