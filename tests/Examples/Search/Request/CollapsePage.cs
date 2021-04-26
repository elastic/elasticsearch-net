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
using Examples.Models;
using Newtonsoft.Json.Linq;

namespace Examples.Search.Request
{
	public class CollapsePage : ExampleBase
	{
		[U]
		[Description("search/request/collapse.asciidoc:9")]
		public void Line9()
		{
			// tag::032f67ced3e7d106f8722432ebbd94d3[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Query(q => q
					.Match(m => m
						.Field(f => f.Message)
						.Query("elasticsearch")
					)
				)
				.Collapse(c => c
					.Field(f => f.User)
				)
				.Sort(so => so
					.Field(f => f.Likes, SortOrder.Descending)
				)
				.From(10)
			);
			// end::032f67ced3e7d106f8722432ebbd94d3[]

			searchResponse.MatchesExample(@"GET /twitter/_search
			{
			    ""query"": {
			        ""match"": {
			            ""message"": ""elasticsearch""
			        }
			    },
			    ""collapse"" : {
			        ""field"" : ""user"" \<1>
			    },
			    ""sort"": [""likes""], \<2>
			    ""from"": 10 \<3>
			}", e => e.ApplyBodyChanges(json =>
			{
				json["query"]["match"]["message"].ToLongFormQuery();
				json["sort"] = new JArray(new JObject
				{
					["likes"] = new JObject { ["order"] = "desc" }
				});
			}));
		}

		[U]
		[Description("search/request/collapse.asciidoc:43")]
		public void Line43()
		{
			// tag::63d36a10d9475be2e2fa73d2415e20e6[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Query(q => q
					.Match(m => m
						.Field(f => f.Message)
						.Query("elasticsearch")
					)
				)
				.Collapse(c => c
					.Field(f => f.User)
					.InnerHits(ih => ih
						.Name("last_tweets")
						.Size(5)
						.Sort(so => so
							.Ascending("date")
						)
					)
					.MaxConcurrentGroupSearches(4)
				)
				.Sort(so => so
					.Field(f => f.Likes, SortOrder.Descending)
				)
			);
			// end::63d36a10d9475be2e2fa73d2415e20e6[]

			searchResponse.MatchesExample(@"GET /twitter/_search
			{
			    ""query"": {
			        ""match"": {
			            ""message"": ""elasticsearch""
			        }
			    },
			    ""collapse"" : {
			        ""field"" : ""user"", \<1>
			        ""inner_hits"": {
			            ""name"": ""last_tweets"", \<2>
			            ""size"": 5, \<3>
			            ""sort"": [{ ""date"": ""asc"" }] \<4>
			        },
			        ""max_concurrent_group_searches"": 4 \<5>
			    },
			    ""sort"": [""likes""]
			}", e => e.ApplyBodyChanges(json =>
			{
				json["query"]["match"]["message"].ToLongFormQuery();
				json["collapse"]["inner_hits"]["sort"][0]["date"] = new JObject { ["order"] = "asc" };
				json["sort"] = new JArray(new JObject
				{
					["likes"] = new JObject { ["order"] = "desc" }
				});
			}));
		}

		[U(Skip = "Waiting on PR to support multiple inner hits: https://github.com/elastic/elasticsearch-net/issues/4723")]
		[Description("search/request/collapse.asciidoc:77")]
		public void Line77()
		{
			// tag::4f20ca49fbaac83620d4cb23fd355f3b[]
			var searchResponse = client.Search<Tweet>(s => s
				.Query(q => q
					.Match(m => m
						.Field(f => f.Message)
						.Query("elasticsearch")
					)
				)
				.Collapse(c => c
					.Field(f => f.User)
					.InnerHits(ih => ih
						.Name("most_liked")
						.Size(3)
						.Sort(so => so
							.Field(f => f.Field("likes"))
						)
					)
				)
				.Sort(so => so
					.Field(f => f.Likes, SortOrder.Descending)
				)
			);
			// end::4f20ca49fbaac83620d4cb23fd355f3b[]

			searchResponse.MatchesExample(@"GET /twitter/_search
			{
			    ""query"": {
			        ""match"": {
			            ""message"": ""elasticsearch""
			        }
			    },
			    ""collapse"" : {
			        ""field"" : ""user"", \<1>
			        ""inner_hits"": [
			            {
			                ""name"": ""most_liked"",  \<2>
			                ""size"": 3,
			                ""sort"": [""likes""]
			            },
			            {
			                ""name"": ""most_recent"", \<3>
			                ""size"": 3,
			                ""sort"": [{ ""date"": ""asc"" }]
			            }
			        ]
			    },
			    ""sort"": [""likes""]
			}");
		}
	}
}
