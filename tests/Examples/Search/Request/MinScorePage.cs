// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class MinScorePage : ExampleBase
	{
		[U]
		[Description("search/request/min-score.asciidoc:8")]
		public void Line8()
		{
			// tag::8e8ceac8fc99348f885f85ff714557fd[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.MinScore(0.5)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::8e8ceac8fc99348f885f85ff714557fd[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""min_score"": 0.5,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery()));
		}
	}
}
