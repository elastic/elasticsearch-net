// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class SegmentsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/segments.asciidoc:12")]
		public void Line12()
		{
			// tag::3f176d802b3e2964686ecd91748fab89[]
			var response0 = new SearchResponse<object>();
			// end::3f176d802b3e2964686ecd91748fab89[]

			response0.MatchesExample(@"GET /twitter/_segments");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/segments.asciidoc:115")]
		public void Line115()
		{
			// tag::940e8c2c7ff92d71f489bdb7183c1ce6[]
			var response0 = new SearchResponse<object>();
			// end::940e8c2c7ff92d71f489bdb7183c1ce6[]

			response0.MatchesExample(@"GET /test/_segments");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/segments.asciidoc:124")]
		public void Line124()
		{
			// tag::975b4b92464d52068516aa2f0f955cc1[]
			var response0 = new SearchResponse<object>();
			// end::975b4b92464d52068516aa2f0f955cc1[]

			response0.MatchesExample(@"GET /test1,test2/_segments");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/segments.asciidoc:133")]
		public void Line133()
		{
			// tag::6414b9276ba1c63898c3ff5cbe03c54e[]
			var response0 = new SearchResponse<object>();
			// end::6414b9276ba1c63898c3ff5cbe03c54e[]

			response0.MatchesExample(@"GET /_segments");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/segments.asciidoc:193")]
		public void Line193()
		{
			// tag::1b21d886f6e9619c73079d14581ccbe4[]
			var response0 = new SearchResponse<object>();
			// end::1b21d886f6e9619c73079d14581ccbe4[]

			response0.MatchesExample(@"GET /test/_segments?verbose=true");
		}
	}
}
