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
	public class SamlLogoutApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/saml-logout-api.asciidoc:62")]
		public void Line62()
		{
			// tag::8d4dda5d988d568f4f4210a6387e026f[]
			var response0 = new SearchResponse<object>();
			// end::8d4dda5d988d568f4f4210a6387e026f[]

			response0.MatchesExample(@"POST /_security/saml/logout
			{
			  ""token"" : ""46ToAxZVaXVVZTVKOVF5YU04ZFJVUDVSZlV3"",
			  ""refresh_token"" : ""mJdXLtmvTUSpoLwMvdBt_w""
			}");
		}
	}
}
