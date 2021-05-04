// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Security.Authorization
{
	public class DocumentLevelSecurityPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/document-level-security.asciidoc:18")]
		public void Line18()
		{
			// tag::6365312d470426cab1b77e9ffde49170[]
			var response0 = new SearchResponse<object>();
			// end::6365312d470426cab1b77e9ffde49170[]

			response0.MatchesExample(@"POST /_security/role/click_role
			{
			  ""indices"": [
			    {
			      ""names"": [ ""events-*"" ],
			      ""privileges"": [ ""read"" ],
			      ""query"": ""{\""match\"": {\""category\"": \""click\""}}""
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/document-level-security.asciidoc:41")]
		public void Line41()
		{
			// tag::c79e8ee86b332302b25c5c1f5f4f89d7[]
			var response0 = new SearchResponse<object>();
			// end::c79e8ee86b332302b25c5c1f5f4f89d7[]

			response0.MatchesExample(@"POST /_security/role/dept_role
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""query"" : {
			        ""term"" : { ""department_id"" : 12 }
			      }
			    }
			  ]
			}");
		}
	}
}
