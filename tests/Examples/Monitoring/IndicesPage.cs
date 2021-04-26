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

namespace Examples.Monitoring
{
	public class IndicesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("monitoring/indices.asciidoc:12")]
		public void Line12()
		{
			// tag::83dfd0852101eca3ba8174c9c38b4e73[]
			var response0 = new SearchResponse<object>();
			// end::83dfd0852101eca3ba8174c9c38b4e73[]

			response0.MatchesExample(@"GET /_template/.monitoring-*");
		}

		[U(Skip = "Example not implemented")]
		[Description("monitoring/indices.asciidoc:29")]
		public void Line29()
		{
			// tag::a63906c63a8681c72d53ee0fcf2ffd35[]
			var response0 = new SearchResponse<object>();
			// end::a63906c63a8681c72d53ee0fcf2ffd35[]

			response0.MatchesExample(@"PUT /_template/custom_monitoring
			{
			    ""index_patterns"": "".monitoring-*"",
			    ""order"": 1,
			    ""settings"": {
			        ""number_of_shards"": 5,
			        ""number_of_replicas"": 2
			    }
			}");
		}
	}
}
