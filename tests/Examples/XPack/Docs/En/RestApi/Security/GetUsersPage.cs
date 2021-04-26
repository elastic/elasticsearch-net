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
	public class GetUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-users.asciidoc:56")]
		public void Line56()
		{
			// tag::3924ee252581ebb96ac0e60046125ae8[]
			var response0 = new SearchResponse<object>();
			// end::3924ee252581ebb96ac0e60046125ae8[]

			response0.MatchesExample(@"GET /_security/user/jacknich");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-users.asciidoc:80")]
		public void Line80()
		{
			// tag::abdbc81e799e28c833556b1c29f03ba6[]
			var response0 = new SearchResponse<object>();
			// end::abdbc81e799e28c833556b1c29f03ba6[]

			response0.MatchesExample(@"GET /_security/user");
		}
	}
}
