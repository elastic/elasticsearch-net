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
	public class ClearCachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/clear-cache.asciidoc:50")]
		public void Line50()
		{
			// tag::a5e2b3588258430f2e595abda98e3943[]
			var response0 = new SearchResponse<object>();
			// end::a5e2b3588258430f2e595abda98e3943[]

			response0.MatchesExample(@"POST /_security/realm/default_file/_clear_cache");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/clear-cache.asciidoc:57")]
		public void Line57()
		{
			// tag::c1409f591a01589638d9b00436ce42c0[]
			var response0 = new SearchResponse<object>();
			// end::c1409f591a01589638d9b00436ce42c0[]

			response0.MatchesExample(@"POST /_security/realm/default_file/_clear_cache?usernames=rdeniro,alpacino");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/clear-cache.asciidoc:65")]
		public void Line65()
		{
			// tag::00272f75a6afea91f8554ef7cda0c1f2[]
			var response0 = new SearchResponse<object>();
			// end::00272f75a6afea91f8554ef7cda0c1f2[]

			response0.MatchesExample(@"POST /_security/realm/default_file,ldap1/_clear_cache");
		}
	}
}
