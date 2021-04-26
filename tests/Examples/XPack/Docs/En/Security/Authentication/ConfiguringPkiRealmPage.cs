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
	public class ConfiguringPkiRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-pki-realm.asciidoc:163")]
		public void Line163()
		{
			// tag::70bbe14bc4d5a5d58e81ab2b02408817[]
			var response0 = new SearchResponse<object>();
			// end::70bbe14bc4d5a5d58e81ab2b02408817[]

			response0.MatchesExample(@"PUT /_security/role_mapping/users
			{
			  ""roles"" : [ ""user"" ],
			  ""rules"" : { ""field"" : {
			    ""dn"" : ""cn=John Doe,ou=example,o=com"" \<1>
			  } },
			  ""enabled"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-pki-realm.asciidoc:271")]
		public void Line271()
		{
			// tag::1f8a6d2cc57ed8997a52354aca371aac[]
			var response0 = new SearchResponse<object>();
			// end::1f8a6d2cc57ed8997a52354aca371aac[]

			response0.MatchesExample(@"PUT /_security/role_mapping/direct_pki_only
			{
			  ""roles"" : [ ""role_for_pki1_direct"" ],
			  ""rules"" : {
			    ""all"": [
			      {
			        ""field"": {""realm.name"": ""pki1""}
			      },
			      {
			        ""field"": {
			          ""metadata.pki_delegated_by_user"": null <1>
			        }
			      }
			    ]
			  },
			  ""enabled"": true
			}");
		}
	}
}
