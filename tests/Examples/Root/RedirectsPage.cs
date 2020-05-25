// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class RedirectsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line408()
		{
			// tag::4ab6d3ed4f4422cee8a590040a579be5[]
			var response0 = new SearchResponse<object>();
			// end::4ab6d3ed4f4422cee8a590040a579be5[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": {
			        ""match"": {
			          ""text"": ""quick brown fox""
			        }
			      },
			      ""filter"": {
			        ""term"": {
			          ""status"": ""published""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
