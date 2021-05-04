// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DeleteAppPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/delete-app-privileges.asciidoc:41")]
		public void Line41()
		{
			// tag::ebd76a45e153c4656c5871e23b7b5508[]
			var response0 = new SearchResponse<object>();
			// end::ebd76a45e153c4656c5871e23b7b5508[]

			response0.MatchesExample(@"DELETE /_security/privilege/myapp/read");
		}
	}
}
