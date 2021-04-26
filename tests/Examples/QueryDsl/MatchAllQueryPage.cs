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
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class MatchAllQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/match-all-query.asciidoc:11")]
		public void Line11()
		{
			// tag::09d617863a103c82fb4101e6165ea7fe[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.MatchAll(m => m));
			// end::09d617863a103c82fb4101e6165ea7fe[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_all"": {}
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-all-query.asciidoc:23")]
		public void Line23()
		{
			// tag::75330ec1305d2beb0e2f34d2195464e2[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.MatchAll(m => m.Boost(1.2)));
			// end::75330ec1305d2beb0e2f34d2195464e2[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_all"": { ""boost"" : 1.2 }
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-all-query.asciidoc:39")]
		public void Line39()
		{
			// tag::81c9aa2678d6166a9662ddf2c011a6a5[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q.MatchNone())
			);
			// end::81c9aa2678d6166a9662ddf2c011a6a5[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_none"": {}
			    }
			}");
		}
	}
}
