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

namespace Examples.Cat
{
	public class CountPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/count.asciidoc:57")]
		public void Line57()
		{
			// tag::e7553d4bb4fd82d8f80a4d7af2624afb[]
			var response0 = new SearchResponse<object>();
			// end::e7553d4bb4fd82d8f80a4d7af2624afb[]

			response0.MatchesExample(@"GET /_cat/count/twitter?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/count.asciidoc:79")]
		public void Line79()
		{
			// tag::0a1f8ad54b1d8c9feeaceaeed16c8490[]
			var response0 = new SearchResponse<object>();
			// end::0a1f8ad54b1d8c9feeaceaeed16c8490[]

			response0.MatchesExample(@"GET /_cat/count?v");
		}
	}
}
