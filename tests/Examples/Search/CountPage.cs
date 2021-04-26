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
using Elasticsearch.Net;
using Examples.Models;

namespace Examples.Search
{
	public class CountPage : ExampleBase
	{
		[U]
		[Description("search/count.asciidoc:7")]
		public void Line7()
		{
			// tag::1b542e3ea87a742f95641d64dcfb1bdb[]
			var countResponse = client.Count<Tweet>(c => c
				.Index("twitter")
				.QueryOnQueryString("user:kimchy")
			);
			// end::1b542e3ea87a742f95641d64dcfb1bdb[]

			countResponse.MatchesExample(@"GET /twitter/_count?q=user:kimchy");
		}

		[U]
		[Description("search/count.asciidoc:99")]
		public void Line99()
		{
			// tag::8f0511f8a5cb176ff2afdd4311799a33[]
			var indexResponse = client.Index(new Tweet
			{
				User = "kimchy"
			}, i => i.Id(1).Index("twitter").Refresh(Refresh.True));

			var countResponse1 = client.Count<Tweet>(c => c
				.Index("twitter")
				.QueryOnQueryString("user:kimchy")
			);

			var countResponse2 = client.Count<Tweet>(c => c
				.Index("twitter")
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::8f0511f8a5cb176ff2afdd4311799a33[]

			indexResponse.MatchesExample(@"PUT /twitter/_doc/1?refresh
			{
			    ""user"": ""kimchy""
			}", e =>
			{
				e.Uri.Query = "refresh=true";
			});

			countResponse1.MatchesExample(@"GET /twitter/_count?q=user:kimchy");

			countResponse2.MatchesExample(@"GET /twitter/_count
			{
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery()));
		}
	}
}
