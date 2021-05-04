// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Suggesters
{
	public class MiscPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/misc.asciidoc:10")]
		public void Line10()
		{
			// tag::e194e9cbe3eb2305f4f7cdda0cf529bd[]
			var response0 = new SearchResponse<object>();
			// end::e194e9cbe3eb2305f4f7cdda0cf529bd[]

			response0.MatchesExample(@"POST _search?typed_keys
			{
			  ""suggest"": {
			    ""text"" : ""some test mssage"",
			    ""my-first-suggester"" : {
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    },
			    ""my-second-suggester"" : {
			      ""phrase"" : {
			        ""field"" : ""message""
			      }
			    }
			  }
			}");
		}
	}
}
