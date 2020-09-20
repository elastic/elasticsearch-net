// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class TrackTotalHitsPage : ExampleBase
	{
		[U]
		[Description("search/request/track-total-hits.asciidoc:23")]
		public void Line23()
		{
			// tag::32789ba30a73d8813b61c39619ad7d71[]
			var searchResponse = client.Search<object>(s => s
				.Index("twitter")
				.TrackTotalHits()
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("Elasticsearch")
					)
				)
			);
			// end::32789ba30a73d8813b61c39619ad7d71[]

			searchResponse.MatchesExample(@"GET twitter/_search
			{
			    ""track_total_hits"": true,
			     ""query"": {
			        ""match"" : {
			            ""message"" : ""Elasticsearch""
			        }
			     }
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["message"].ToLongFormQuery()));
		}

		[U(Skip = "Track Total hits needs to support number: https://github.com/elastic/elasticsearch-net/issues/4241")]
		[Description("search/request/track-total-hits.asciidoc:68")]
		public void Line68()
		{
			// tag::e45cb729ed4a694b2d6cabaa55c9b5be[]
			var searchResponse = client.Search<object>(s => s
				//.TrackTotalHits(100)
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("Elasticsearch")
					)
				)
			);
			// end::e45cb729ed4a694b2d6cabaa55c9b5be[]

			searchResponse.MatchesExample(@"GET twitter/_search
			{
			    ""track_total_hits"": 100,
			     ""query"": {
			        ""match"" : {
			            ""message"" : ""Elasticsearch""
			        }
			     }
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["message"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/track-total-hits.asciidoc:142")]
		public void Line142()
		{
			// tag::d9e08bca979c7ba3a9581f69470bf914[]
			var searchResponse = client.Search<object>(s => s
				.Index("twitter")
				.TrackTotalHits(false)
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("Elasticsearch")
					)
				)
			);
			// end::d9e08bca979c7ba3a9581f69470bf914[]

			searchResponse.MatchesExample(@"GET twitter/_search
			{
			    ""track_total_hits"": false,
			     ""query"": {
			        ""match"" : {
			            ""message"" : ""Elasticsearch""
			        }
			     }
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["message"].ToLongFormQuery()));
		}
	}
}
