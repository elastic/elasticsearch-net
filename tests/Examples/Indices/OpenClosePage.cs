// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class OpenClosePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/open-close.asciidoc:11")]
		public void Line11()
		{
			// tag::7f36828a03e8cb5a028d9a6efb056b88[]
			var response0 = new SearchResponse<object>();
			// end::7f36828a03e8cb5a028d9a6efb056b88[]

			response0.MatchesExample(@"POST /twitter/_open");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/open-close.asciidoc:122")]
		public void Line122()
		{
			// tag::37e6177bf8803971d30a4252498c07a4[]
			var response0 = new SearchResponse<object>();
			// end::37e6177bf8803971d30a4252498c07a4[]

			response0.MatchesExample(@"POST /my_index/_open");
		}
	}
}
