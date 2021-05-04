// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class SeqNoPage : ExampleBase
	{
		[U]
		[Description("search/request/seq-no.asciidoc:8")]
		public void Line8()
		{
			// tag::63965d439716ed6d18d30baef09001a5[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.SequenceNumberPrimaryTerm()
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::63965d439716ed6d18d30baef09001a5[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""seq_no_primary_term"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(json =>
				{
					json.Remove("seq_no_primary_term");
					json["query"]["term"]["user"].ToLongFormTermQuery();
				});

				e.Uri.Query = "seq_no_primary_term=true";
			});
		}
	}
}
