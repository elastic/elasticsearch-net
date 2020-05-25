// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetRoleMappingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-role-mappings.asciidoc:64")]
		public void Line64()
		{
			// tag::8b3a94495127efd9d56b2cd7f3eecdca[]
			var response0 = new SearchResponse<object>();
			// end::8b3a94495127efd9d56b2cd7f3eecdca[]

			response0.MatchesExample(@"GET /_security/role_mapping/mapping1");
		}
	}
}
