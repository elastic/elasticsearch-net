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

namespace Examples.XPack.Docs.En.Security.Authorization
{
	public class AliasPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/alias-privileges.asciidoc:61")]
		public void Line61()
		{
			// tag::474700a60cce2a786b7fd5bedf7df687[]
			var response0 = new SearchResponse<object>();
			// end::474700a60cce2a786b7fd5bedf7df687[]

			response0.MatchesExample(@"GET /.ds-logs-000002/_doc/2");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/alias-privileges.asciidoc:85")]
		public void Line85()
		{
			// tag::65c86eb274d4ef4fb0a1649ac35c1305[]
			var response0 = new SearchResponse<object>();
			// end::65c86eb274d4ef4fb0a1649ac35c1305[]

			response0.MatchesExample(@"GET /.ds-logs-000003/_doc/2");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/alias-privileges.asciidoc:127")]
		public void Line127()
		{
			// tag::195fe9fadf952ddd7a69c8aaf98d47a1[]
			var response0 = new SearchResponse<object>();
			// end::195fe9fadf952ddd7a69c8aaf98d47a1[]

			response0.MatchesExample(@"GET /current_year/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/alias-privileges.asciidoc:153")]
		public void Line153()
		{
			// tag::c4d60fd70ef1be46616a0d4e7578d8b9[]
			var response0 = new SearchResponse<object>();
			// end::c4d60fd70ef1be46616a0d4e7578d8b9[]

			response0.MatchesExample(@"PUT /2015
			{
			    ""aliases"" : {
			        ""current_year"" : {}
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/alias-privileges.asciidoc:165")]
		public void Line165()
		{
			// tag::06f6cb6e20d2faf6599cfe1f39c6c56b[]
			var response0 = new SearchResponse<object>();
			// end::06f6cb6e20d2faf6599cfe1f39c6c56b[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""2015"", ""alias"" : ""current_year"" } }
			    ]
			}");
		}
	}
}
