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

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetTokensPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-tokens.asciidoc:111")]
		public void Line111()
		{
			// tag::cee591c1fc70d4f180c623a3a6d07755[]
			var response0 = new SearchResponse<object>();
			// end::cee591c1fc70d4f180c623a3a6d07755[]

			response0.MatchesExample(@"POST /_security/oauth2/token
			{
			  ""grant_type"" : ""client_credentials""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-tokens.asciidoc:146")]
		public void Line146()
		{
			// tag::e1337c6b76defd5a46d05220f9d9c9fc[]
			var response0 = new SearchResponse<object>();
			// end::e1337c6b76defd5a46d05220f9d9c9fc[]

			response0.MatchesExample(@"POST /_security/oauth2/token
			{
			  ""grant_type"" : ""password"",
			  ""username"" : ""test_admin"",
			  ""password"" : ""x-pack-test-password""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-tokens.asciidoc:176")]
		public void Line176()
		{
			// tag::1873f8a8a291e6fcd6c1c83ea6928759[]
			var response0 = new SearchResponse<object>();
			// end::1873f8a8a291e6fcd6c1c83ea6928759[]

			response0.MatchesExample(@"POST /_security/oauth2/token
			{
			    ""grant_type"": ""refresh_token"",
			    ""refresh_token"": ""vLBPvmAB6KvwvJZr27cS""
			}");
		}
	}
}
