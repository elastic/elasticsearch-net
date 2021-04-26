/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
