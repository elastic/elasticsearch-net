// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Examples.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Examples.Root
{
	public class SearchPage : ExampleBase
	{
		[U]
		[Description("search.asciidoc:18")]
		public void Line18()
		{
			// tag::5d32279dcd52b22d9e1178a02a3ad957[]
			var indexResponse = client.Index(new Tweet
			{
				User = "kimchy",
				PostDate = new DateTime(2009, 11, 15, 14, 12, 12),
				Message = "trying out Elasticsearch"
			}, i => i
				.Index("twitter")
				.Routing("kimchy")
			);
			// end::5d32279dcd52b22d9e1178a02a3ad957[]

			indexResponse.MatchesExample(@"POST /twitter/_doc?routing=kimchy
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[Description("search.asciidoc:32")]
		public void Line32()
		{
			// tag::8acc1d67b152e7027e0f0e1a8b4b2431[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Routing("kimchy")
				.Query(q => q
					.Bool(b => b
						.Must(mu => mu
							.QueryString(qs => qs
								.Query("some query string here")
							)
						)
						.Filter(f => f
							.Term(t => t.User, "kimchy")
						)
					)
				)
			);
			// end::8acc1d67b152e7027e0f0e1a8b4b2431[]

			searchResponse.MatchesExample(@"POST /twitter/_search?routing=kimchy
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""query_string"" : {
			                    ""query"" : ""some query string here""
			                }
			            },
			            ""filter"" : {
			                ""term"" : { ""user"" : ""kimchy"" }
			            }
			        }
			    }
			}", (e, body) =>
			{
				body["query"]["bool"].ToLongFormBoolQuery(b =>
				{
					var filter = b["filter"] as JArray;
					var value = filter[0]["term"]["user"];
					filter[0]["term"]["user"] = new JObject { { "value", value } };
				});
			});
		}

		[U]
		[Description("search.asciidoc:72")]
		public void Line72()
		{
			// tag::014b788c879e4aaa1020672e45e25473[]
			var putSettingsResponse = client.Cluster.PutSettings(c => c
				.Transient(t => t
					.Add("cluster.routing.use_adaptive_replica_selection", false)
				)
			);
			// end::014b788c879e4aaa1020672e45e25473[]

			putSettingsResponse.MatchesExample(@"PUT /_cluster/settings
			{
			    ""transient"": {
			        ""cluster.routing.use_adaptive_replica_selection"": false
			    }
			}");
		}

		[U]
		[Description("search.asciidoc:96")]
		public void Line96()
		{
			// tag::189a921df2f5b1fe580937210ce9c1c2[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q.MatchAll())
				.Stats("group1", "group2")
			);
			// end::189a921df2f5b1fe580937210ce9c1c2[]

			searchResponse.MatchesExample(@"POST /_search
			{
			    ""query"" : {
			        ""match_all"" : {}
			    },
			    ""stats"" : [""group1"", ""group2""]
			}", e =>
			{
				// client sends stats in the query string
				e.Uri.Query = "stats=group1,group2";
				e.ApplyBodyChanges(body =>
				{
					body.Remove("stats");
				});
			});
		}
	}
}
