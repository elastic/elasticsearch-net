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
	public class ManagingRolesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/managing-roles.asciidoc:158")]
		public void Line158()
		{
			// tag::d3e5edac5b461020017fd9d8ec7a91fa[]
			var response0 = new SearchResponse<object>();
			// end::d3e5edac5b461020017fd9d8ec7a91fa[]

			response0.MatchesExample(@"POST /_security/role/clicks_admin
			{
			  ""run_as"": [ ""clicks_watcher_1"" ],
			  ""cluster"": [ ""monitor"" ],
			  ""indices"": [
			    {
			      ""names"": [ ""events-*"" ],
			      ""privileges"": [ ""read"" ],
			      ""field_security"" : {
			        ""grant"" : [ ""category"", ""@timestamp"", ""message"" ]
			      },
			      ""query"": ""{\""match\"": {\""category\"": \""click\""}}""
			    }
			  ]
			}");
		}
	}
}
