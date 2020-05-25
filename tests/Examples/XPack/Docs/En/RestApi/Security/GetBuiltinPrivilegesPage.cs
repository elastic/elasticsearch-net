// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetBuiltinPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-builtin-privileges.asciidoc:54")]
		public void Line54()
		{
			// tag::2623eb122cc0299b42fc9eca6e7f5e56[]
			var response0 = new SearchResponse<object>();
			// end::2623eb122cc0299b42fc9eca6e7f5e56[]

			response0.MatchesExample(@"GET /_security/privilege/_builtin");
		}
	}
}
