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
	public class GetAppPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-app-privileges.asciidoc:58")]
		public void Line58()
		{
			// tag::cd8006165ac64f1ef99af48e5a35a25b[]
			var response0 = new SearchResponse<object>();
			// end::cd8006165ac64f1ef99af48e5a35a25b[]

			response0.MatchesExample(@"GET /_security/privilege/myapp/read");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-app-privileges.asciidoc:88")]
		public void Line88()
		{
			// tag::3b18e9de638ff0b1c7a1f1f6bf1c24f3[]
			var response0 = new SearchResponse<object>();
			// end::3b18e9de638ff0b1c7a1f1f6bf1c24f3[]

			response0.MatchesExample(@"GET /_security/privilege/myapp/");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-app-privileges.asciidoc:95")]
		public void Line95()
		{
			// tag::0ddf705317d9c5095b4a1419a2e3bace[]
			var response0 = new SearchResponse<object>();
			// end::0ddf705317d9c5095b4a1419a2e3bace[]

			response0.MatchesExample(@"GET /_security/privilege/");
		}
	}
}
