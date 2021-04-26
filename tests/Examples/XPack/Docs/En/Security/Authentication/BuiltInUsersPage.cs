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
	public class BuiltInUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/built-in-users.asciidoc:142")]
		public void Line142()
		{
			// tag::6f0389ac52808df23bb6081a1acd4eed[]
			var response0 = new SearchResponse<object>();
			// end::6f0389ac52808df23bb6081a1acd4eed[]

			response0.MatchesExample(@"PUT _security/user/logstash_system/_enable");
		}
	}
}
