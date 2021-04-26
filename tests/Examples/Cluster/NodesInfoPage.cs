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

namespace Examples.Cluster
{
	public class NodesInfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-info.asciidoc:165")]
		public void Line165()
		{
			// tag::3c4d7ef8422d2db423a8f23effcddaa1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();
			// end::3c4d7ef8422d2db423a8f23effcddaa1[]

			response0.MatchesExample(@"GET /_nodes/process");

			response1.MatchesExample(@"GET /_nodes/_all/process");

			response2.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/jvm,process");

			response3.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/info/jvm,process");

			response4.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/_all");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-info.asciidoc:192")]
		public void Line192()
		{
			// tag::68b64313bf89ec3f2c645da61999dbb4[]
			var response0 = new SearchResponse<object>();
			// end::68b64313bf89ec3f2c645da61999dbb4[]

			response0.MatchesExample(@"GET /_nodes/plugins");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-info.asciidoc:263")]
		public void Line263()
		{
			// tag::0c464965126cc09e6812716a145991d4[]
			var response0 = new SearchResponse<object>();
			// end::0c464965126cc09e6812716a145991d4[]

			response0.MatchesExample(@"GET /_nodes/ingest");
		}
	}
}
