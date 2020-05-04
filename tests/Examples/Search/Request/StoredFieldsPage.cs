// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class StoredFieldsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/stored-fields.asciidoc:13")]
		public void Line13()
		{
			// tag::2eeb3e55a7d3955e084bb369f1539009[]
			var response0 = new SearchResponse<object>();
			// end::2eeb3e55a7d3955e084bb369f1539009[]

			response0.MatchesExample(@"GET /_search
			{
			    ""stored_fields"" : [""user"", ""postDate""],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/stored-fields.asciidoc:29")]
		public void Line29()
		{
			// tag::2af86a6ebbb834fbcf6fa7268f87a3a5[]
			var response0 = new SearchResponse<object>();
			// end::2af86a6ebbb834fbcf6fa7268f87a3a5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""stored_fields"" : [],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/stored-fields.asciidoc:55")]
		public void Line55()
		{
			// tag::ccec437aed7a10d9111724ffd929fe00[]
			var response0 = new SearchResponse<object>();
			// end::ccec437aed7a10d9111724ffd929fe00[]

			response0.MatchesExample(@"GET /_search
			{
			    ""stored_fields"": ""_none_"",
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}