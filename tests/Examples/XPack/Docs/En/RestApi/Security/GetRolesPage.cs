// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetRolesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-roles.asciidoc:56")]
		public void Line56()
		{
			// tag::115529722ba30b0b0d51a7ff87e59198[]
			var response0 = new SearchResponse<object>();
			// end::115529722ba30b0b0d51a7ff87e59198[]

			response0.MatchesExample(@"GET /_security/role/my_admin_role");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-roles.asciidoc:90")]
		public void Line90()
		{
			// tag::128283698535116931dca9d16a16dca2[]
			var response0 = new SearchResponse<object>();
			// end::128283698535116931dca9d16a16dca2[]

			response0.MatchesExample(@"GET /_security/role");
		}
	}
}
