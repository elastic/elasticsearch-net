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

namespace Examples.Indices
{
	public class AliasExistsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/alias-exists.asciidoc:12")]
		public void Line12()
		{
			// tag::83e388644f60178c8de0d0e4247ee4c6[]
			var response0 = new SearchResponse<object>();
			// end::83e388644f60178c8de0d0e4247ee4c6[]

			response0.MatchesExample(@"HEAD /_alias/alias1");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/alias-exists.asciidoc:62")]
		public void Line62()
		{
			// tag::666785827827be4b5252ec859c354d30[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::666785827827be4b5252ec859c354d30[]

			response0.MatchesExample(@"HEAD /_alias/2030");

			response1.MatchesExample(@"HEAD /_alias/20*");

			response2.MatchesExample(@"HEAD /logs_20302801/_alias/*");
		}
	}
}
