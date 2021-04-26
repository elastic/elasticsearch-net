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
	public class FilteringPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/filtering.asciidoc:54")]
		public void Line54()
		{
			// tag::dad2d4add751fde5c39475ca709cc14b[]
			var response0 = new SearchResponse<object>();
			// end::dad2d4add751fde5c39475ca709cc14b[]

			response0.MatchesExample(@"PUT test/_settings
			{
			  ""index.routing.allocation.include.size"": ""big,medium""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/filtering.asciidoc:67")]
		public void Line67()
		{
			// tag::b8b198ede3d08f315348e2a857e47773[]
			var response0 = new SearchResponse<object>();
			// end::b8b198ede3d08f315348e2a857e47773[]

			response0.MatchesExample(@"PUT test/_settings
			{
			  ""index.routing.allocation.include.size"": ""big"",
			  ""index.routing.allocation.include.rack"": ""rack1""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/filtering.asciidoc:109")]
		public void Line109()
		{
			// tag::28ac880057135e46b3b00c7f3976538c[]
			var response0 = new SearchResponse<object>();
			// end::28ac880057135e46b3b00c7f3976538c[]

			response0.MatchesExample(@"PUT test/_settings
			{
			  ""index.routing.allocation.include._ip"": ""192.168.2.*""
			}");
		}
	}
}
