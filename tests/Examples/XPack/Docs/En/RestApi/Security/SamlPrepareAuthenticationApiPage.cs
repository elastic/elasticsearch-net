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
	public class SamlPrepareAuthenticationApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/saml-prepare-authentication-api.asciidoc:70")]
		public void Line70()
		{
			// tag::a5dfcfd1cfb3558e7912456669c92eee[]
			var response0 = new SearchResponse<object>();
			// end::a5dfcfd1cfb3558e7912456669c92eee[]

			response0.MatchesExample(@"POST /_security/saml/prepare
			{
			  ""realm"" : ""saml1""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/saml-prepare-authentication-api.asciidoc:81")]
		public void Line81()
		{
			// tag::da3f280bc65b581fb3097be768061bee[]
			var response0 = new SearchResponse<object>();
			// end::da3f280bc65b581fb3097be768061bee[]

			response0.MatchesExample(@"POST /_security/saml/prepare
			{
			  ""acs"" : ""https://kibana.org/api/security/saml/callback""
			}");
		}
	}
}
