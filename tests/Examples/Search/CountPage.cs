// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
