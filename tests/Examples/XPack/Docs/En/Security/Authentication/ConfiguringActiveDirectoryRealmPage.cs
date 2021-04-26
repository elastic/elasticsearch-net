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

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class ConfiguringActiveDirectoryRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-active-directory-realm.asciidoc:178")]
		public void Line178()
		{
			// tag::21e95d29bc37deb5689a654aa323b4ba[]
			var response0 = new SearchResponse<object>();
			// end::21e95d29bc37deb5689a654aa323b4ba[]

			response0.MatchesExample(@"PUT /_security/role_mapping/admins
			{
			  ""roles"" : [ ""monitoring"" , ""user"" ],
			  ""rules"" : { ""field"" : {
			    ""groups"" : ""cn=admins,dc=example,dc=com"" \<1>
			  } },
			  ""enabled"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-active-directory-realm.asciidoc:192")]
		public void Line192()
		{
			// tag::bd0d30a7683037e1ebadd163514765d4[]
			var response0 = new SearchResponse<object>();
			// end::bd0d30a7683037e1ebadd163514765d4[]

			response0.MatchesExample(@"PUT /_security/role_mapping/basic_users
			{
			  ""roles"" : [ ""user"" ],
			  ""rules"" : { ""any"": [
			    { ""field"" : {
			      ""groups"" : ""cn=users,dc=example,dc=com"" \<1>
			    } },
			    { ""field"" : {
			      ""dn"" : ""cn=John Doe,cn=contractors,dc=example,dc=com"" \<2>
			    } }
			  ] },
			  ""enabled"": true
			}");
		}
	}
}
