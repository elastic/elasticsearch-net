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
	public class OidcAuthenticateApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/oidc-authenticate-api.asciidoc:65")]
		public void Line65()
		{
			// tag::9c01db07c9ac395b6370e3b33965c21f[]
			var response0 = new SearchResponse<object>();
			// end::9c01db07c9ac395b6370e3b33965c21f[]

			response0.MatchesExample(@"POST /_security/oidc/authenticate
			{
			  ""redirect_uri"" : ""https://oidc-kibana.elastic.co:5603/api/security/oidc/callback?code=jtI3Ntt8v3_XvcLzCFGq&state=4dbrihtIAt3wBTwo6DxK-vdk-sSyDBV8Yf0AjdkdT5I"",
			  ""state"" : ""4dbrihtIAt3wBTwo6DxK-vdk-sSyDBV8Yf0AjdkdT5I"",
			  ""nonce"" : ""WaBPH0KqPVdG5HHdSxPRjfoZbXMCicm5v1OiAj0DUFM"",
			  ""realm"" : ""oidc1""
			}");
		}
	}
}
