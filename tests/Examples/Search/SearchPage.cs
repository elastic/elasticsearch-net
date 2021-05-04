// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Examples.Models;
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class SearchPage : ExampleBase
	{
		[U]
		[Description("search/search.asciidoc:10")]
		public void Line10()
		{
			// tag::d2153f3100bf12c2de98f14eb86ab061[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
			);
			// end::d2153f3100bf12c2de98f14eb86ab061[]

			searchResponse.MatchesExample(@"GET /twitter/_search");
		}

		[U]
		[Description("search/search.asciidoc:596")]
		public void Line596()
		{
			// tag::be49260e1b3496c4feac38c56ebb0669[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.QueryOnQueryString("user:kimchy")
			);
			// end::be49260e1b3496c4feac38c56ebb0669[]

			searchResponse.MatchesExample(@"GET /twitter/_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:642")]
		public void Line642()
		{
			// tag::f5569945024b9d664828693705c27c1a[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index(new[] { "kimchy", "elasticsearch" })
				.QueryOnQueryString("user:kimchy")
			);
			// end::f5569945024b9d664828693705c27c1a[]

			searchResponse.MatchesExample(@"GET /kimchy,elasticsearch/_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:654")]
		public void Line654()
		{
			// tag::168bfdde773570cfc6dd3ab3574e413b[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.QueryOnQueryString("user:kimchy")
			);
			// end::168bfdde773570cfc6dd3ab3574e413b[]

			searchResponse.MatchesExample(@"GET /_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:663")]
		public void Line663()
		{
			// tag::8022e6a690344035b6472a43a9d122e0[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.QueryOnQueryString("user:kimchy")
			);
			// end::8022e6a690344035b6472a43a9d122e0[]

			searchResponse.MatchesExample(@"GET /_all/_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:669")]
		public void Line669()
		{
			// tag::43682666e1abcb14770c99f02eb26a0d[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.QueryOnQueryString("user:kimchy")
			);
			// end::43682666e1abcb14770c99f02eb26a0d[]

			searchResponse.MatchesExample(@"GET /*/_search?q=user:kimchy", e =>
			{
				e.Uri.Path = "/_all/_search";
			});
		}

		[U(Skip = "Example not implemented")]
		[Description("search/search.asciidoc:678")]
		public void Line678()
		{
			// tag::84d6a777a51963629272b1be5698b091[]
			var response0 = new SearchResponse<object>();
			// end::84d6a777a51963629272b1be5698b091[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			  ""query"": {
			    ""term"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}
	}
}
