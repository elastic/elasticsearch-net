// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class VersionPage : ExampleBase
	{
		[U]
		[Description("search/request/version.asciidoc:7")]
		public void Line7()
		{
			// tag::9535be36eac8a589bd6bf7b7228eefd7[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Version()
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::9535be36eac8a589bd6bf7b7228eefd7[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""version"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery()));
		}
	}
}
