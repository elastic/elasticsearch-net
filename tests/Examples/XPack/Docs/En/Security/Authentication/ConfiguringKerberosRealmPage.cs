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
	public class ConfiguringKerberosRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-kerberos-realm.asciidoc:179")]
		public void Line179()
		{
			// tag::9584b042223982e0bfde8d12d42c9705[]
			var response0 = new SearchResponse<object>();
			// end::9584b042223982e0bfde8d12d42c9705[]

			response0.MatchesExample(@"POST /_security/role_mapping/kerbrolemapping
			{
			  ""roles"" : [ ""monitoring_user"" ],
			  ""enabled"": true,
			  ""rules"" : {
			    ""field"" : { ""username"" : ""user@REALM"" }
			  }
			}");
		}
	}
}
