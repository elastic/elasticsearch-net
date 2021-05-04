// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class MatchAllQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/match-all-query.asciidoc:11")]
		public void Line11()
		{
			// tag::09d617863a103c82fb4101e6165ea7fe[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.MatchAll(m => m));
			// end::09d617863a103c82fb4101e6165ea7fe[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_all"": {}
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-all-query.asciidoc:23")]
		public void Line23()
		{
			// tag::75330ec1305d2beb0e2f34d2195464e2[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.MatchAll(m => m.Boost(1.2)));
			// end::75330ec1305d2beb0e2f34d2195464e2[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_all"": { ""boost"" : 1.2 }
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-all-query.asciidoc:39")]
		public void Line39()
		{
			// tag::81c9aa2678d6166a9662ddf2c011a6a5[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q.MatchNone())
			);
			// end::81c9aa2678d6166a9662ddf2c011a6a5[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_none"": {}
			    }
			}");
		}
	}
}
