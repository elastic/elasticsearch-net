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
	public class NodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/nodes.asciidoc:304")]
		public void Line304()
		{
			// tag::db20adb70a8e8d0709d15ba0daf18d23[]
			var response0 = new SearchResponse<object>();
			// end::db20adb70a8e8d0709d15ba0daf18d23[]

			response0.MatchesExample(@"GET /_cat/nodes?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/nodes.asciidoc:332")]
		public void Line332()
		{
			// tag::21d3e98d911642ab3bda2657f7a06f80[]
			var response0 = new SearchResponse<object>();
			// end::21d3e98d911642ab3bda2657f7a06f80[]

			response0.MatchesExample(@"GET /_cat/nodes?v&h=id,ip,port,v,m");
		}
	}
}
