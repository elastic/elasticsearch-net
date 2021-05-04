// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class BuiltInUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/built-in-users.asciidoc:142")]
		public void Line142()
		{
			// tag::6f0389ac52808df23bb6081a1acd4eed[]
			var response0 = new SearchResponse<object>();
			// end::6f0389ac52808df23bb6081a1acd4eed[]

			response0.MatchesExample(@"PUT _security/user/logstash_system/_enable");
		}
	}
}
