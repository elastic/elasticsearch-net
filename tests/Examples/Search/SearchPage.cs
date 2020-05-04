// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Examples.Models;
using System.ComponentModel;

namespace Examples.Search
{
	public class SearchPage : ExampleBase
	{
		[U]
		[Description("search/search.asciidoc:7")]
		public void Line7()
		{
			// tag::9bdd3c0d47e60c8cfafc8109f9369922[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.QueryOnQueryString("tag:wow")
			);
			// end::9bdd3c0d47e60c8cfafc8109f9369922[]

			searchResponse.MatchesExample(@"GET /twitter/_search?q=tag:wow");
		}

		[U]
		[Description("search/search.asciidoc:346")]
		public void Line346()
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
		[Description("search/search.asciidoc:392")]
		public void Line392()
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
		[Description("search/search.asciidoc:404")]
		public void Line404()
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
		[Description("search/search.asciidoc:413")]
		public void Line413()
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
		[Description("search/search.asciidoc:419")]
		public void Line419()
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
	}
}
