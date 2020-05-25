// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DeleteRoleMappingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/delete-role-mappings.asciidoc:46")]
		public void Line46()
		{
			// tag::261480571394632db40e88fbb6c59c2f[]
			var response0 = new SearchResponse<object>();
			// end::261480571394632db40e88fbb6c59c2f[]

			response0.MatchesExample(@"DELETE /_security/role_mapping/mapping1");
		}
	}
}
