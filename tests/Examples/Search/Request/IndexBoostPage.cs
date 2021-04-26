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

namespace Examples.Search.Request
{
	public class IndexBoostPage : ExampleBase
	{
		[U]
		[Description("search/request/index-boost.asciidoc:11")]
		public void Line11()
		{
			// tag::69dce2801f824f61e4f3ea9ee9371e31[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.IndicesBoost(ib => ib
					.Add("index1", 1.4)
					.Add("index2", 1.3)
				)
			);
			// end::69dce2801f824f61e4f3ea9ee9371e31[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""indices_boost"" : [
			        { ""index1"" : 1.4 },
			        { ""index2"" : 1.3 }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/index-boost.asciidoc:25")]
		public void Line25()
		{
			// tag::fb8a4322825d26c4e7b41bd763b3d392[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.IndicesBoost(ib => ib
					.Add("alias1", 1.4)
					.Add("index*", 1.3)
				)
			);
			// end::fb8a4322825d26c4e7b41bd763b3d392[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""indices_boost"" : [
			        { ""alias1"" : 1.4 },
			        { ""index*"" : 1.3 }
			    ]
			}");
		}
	}
}
