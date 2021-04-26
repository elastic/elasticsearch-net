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
	public class VersionPage : ExampleBase
	{
		[U]
		[Description("search/request/version.asciidoc:7")]
		public void Line7()
		{
			// tag::9535be36eac8a589bd6bf7b7228eefd7[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Version()
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::9535be36eac8a589bd6bf7b7228eefd7[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""version"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery()));
		}
	}
}
