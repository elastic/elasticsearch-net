// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class ExplainPage : ExampleBase
	{
		[U]
		[Description("search/request/explain.asciidoc:7")]
		public void Line7()
		{
			// tag::e405e90fe3207157d3c0f9c76c6778e8[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Explain()
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::e405e90fe3207157d3c0f9c76c6778e8[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""explain"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery()));
		}
	}
}
