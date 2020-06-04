// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class FromSizePage : ExampleBase
	{
		[U]
		[Description("search/request/from-size.asciidoc:22")]
		public void Line22()
		{
			// tag::e7d74af44b92196d7d55351d0a40eb81[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.From(5)
				.Size(20)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::e7d74af44b92196d7d55351d0a40eb81[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""from"": 5,
			  ""size"": 20,
			  ""query"": {
			    ""term"": { ""user"": ""kimchy"" }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery());
			});
		}
	}
}
