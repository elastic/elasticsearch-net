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

namespace Examples.IndexModules.Allocation
{
	public class PrioritizationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/prioritization.asciidoc:17")]
		public void Line17()
		{
			// tag::8703f3b1b3895543abc36e2a7a0013d3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::8703f3b1b3895543abc36e2a7a0013d3[]

			response0.MatchesExample(@"PUT index_1");

			response1.MatchesExample(@"PUT index_2");

			response2.MatchesExample(@"PUT index_3
			{
			  ""settings"": {
			    ""index.priority"": 10
			  }
			}");

			response3.MatchesExample(@"PUT index_4
			{
			  ""settings"": {
			    ""index.priority"": 5
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/prioritization.asciidoc:48")]
		public void Line48()
		{
			// tag::a425fcab60f603504becee7d001f0a4b[]
			var response0 = new SearchResponse<object>();
			// end::a425fcab60f603504becee7d001f0a4b[]

			response0.MatchesExample(@"PUT index_4/_settings
			{
			  ""index.priority"": 1
			}");
		}
	}
}
