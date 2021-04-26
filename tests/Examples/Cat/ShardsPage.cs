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
	public class ShardsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/shards.asciidoc:294")]
		public void Line294()
		{
			// tag::7e126e2751311db60cfcbb22c9c41caa[]
			var response0 = new SearchResponse<object>();
			// end::7e126e2751311db60cfcbb22c9c41caa[]

			response0.MatchesExample(@"GET _cat/shards");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/shards.asciidoc:320")]
		public void Line320()
		{
			// tag::e42e92050dd1c20262ce9e38f4b42ba0[]
			var response0 = new SearchResponse<object>();
			// end::e42e92050dd1c20262ce9e38f4b42ba0[]

			response0.MatchesExample(@"GET _cat/shards/twitt*");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/shards.asciidoc:385")]
		public void Line385()
		{
			// tag::25c0e66a433a0cd596e0641b752ff6d7[]
			var response0 = new SearchResponse<object>();
			// end::25c0e66a433a0cd596e0641b752ff6d7[]

			response0.MatchesExample(@"GET _cat/shards?h=index,shard,prirep,state,unassigned.reason");
		}
	}
}
