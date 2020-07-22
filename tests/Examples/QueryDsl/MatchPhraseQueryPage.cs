// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class MatchPhraseQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/match-phrase-query.asciidoc:11")]
		public void Line11()
		{
			// tag::83f95657beca9bf5d8264c80c7fb463f[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.MatchPhrase(mp => mp
						.Field("message")
						.Query("this is a test")
					)
				)
			);
			// end::83f95657beca9bf5d8264c80c7fb463f[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_phrase"" : {
			            ""message"" : ""this is a test""
			        }
			    }
			}", (e, b) => b["query"]["match_phrase"]["message"].ToLongFormQuery());
		}

		[U]
		[Description("query-dsl/match-phrase-query.asciidoc:30")]
		public void Line30()
		{
			// tag::72231b7debac60c95b9869a97dafda3a[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.MatchPhrase(mp => mp
						.Field("message")
						.Query("this is a test")
						.Analyzer("my_analyzer")
					)
				)
			);
			// end::72231b7debac60c95b9869a97dafda3a[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_phrase"" : {
			            ""message"" : {
			                ""query"" : ""this is a test"",
			                ""analyzer"" : ""my_analyzer""
			            }
			        }
			    }
			}");
		}
	}
}
