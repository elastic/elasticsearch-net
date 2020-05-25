// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DisableUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/disable-users.asciidoc:45")]
		public void Line45()
		{
			// tag::bb293e1bdf0c6f6d9069eeb7edc9d399[]
			var response0 = new SearchResponse<object>();
			// end::bb293e1bdf0c6f6d9069eeb7edc9d399[]

			response0.MatchesExample(@"PUT /_security/user/jacknich/_disable");
		}
	}
}
