// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class RefreshPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/refresh.asciidoc:10")]
		public void Line10()
		{
			// tag::c2ac42934e4b76197032b2fc429e317d[]
			var response0 = new SearchResponse<object>();
			// end::c2ac42934e4b76197032b2fc429e317d[]

			response0.MatchesExample(@"POST /twitter/_refresh");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/refresh.asciidoc:93")]
		public void Line93()
		{
			// tag::0e98949e80e665795bc6cfc997165241[]
			var response0 = new SearchResponse<object>();
			// end::0e98949e80e665795bc6cfc997165241[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_refresh");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/refresh.asciidoc:103")]
		public void Line103()
		{
			// tag::d7898526d239d2aea83727fb982f8f77[]
			var response0 = new SearchResponse<object>();
			// end::d7898526d239d2aea83727fb982f8f77[]

			response0.MatchesExample(@"POST /_refresh");
		}
	}
}