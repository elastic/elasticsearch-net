// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class ThaiTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/thai-tokenizer.asciidoc:20")]
		public void Line20()
		{
			// tag::a1e5f3956f9a697e79478fc9a6e30e1f[]
			var response0 = new SearchResponse<object>();
			// end::a1e5f3956f9a697e79478fc9a6e30e1f[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""thai"",
			  ""text"": ""การที่ได้ต้องแสดงว่างานดี""
			}");
		}
	}
}
