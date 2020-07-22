// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Analyzers
{
	public class SimpleAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/simple-analyzer.asciidoc:15")]
		public void Line15()
		{
			// tag::1ea24f67fbbb6293d53caf2fe0c4b984[]
			var response0 = new SearchResponse<object>();
			// end::1ea24f67fbbb6293d53caf2fe0c4b984[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""simple"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/simple-analyzer.asciidoc:134")]
		public void Line134()
		{
			// tag::27bb04d77cbaab09d25fed6dec70835e[]
			var response0 = new SearchResponse<object>();
			// end::27bb04d77cbaab09d25fed6dec70835e[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_simple_analyzer"": {
			          ""tokenizer"": ""lowercase"",
			          ""filter"": [                          <1>
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
