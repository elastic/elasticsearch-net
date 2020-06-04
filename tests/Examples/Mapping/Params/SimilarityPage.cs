// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class SimilarityPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/similarity.asciidoc:32")]
		public void Line32()
		{
			// tag::e6e31dcdd1ca214c17e375c54069d513[]
			var response0 = new SearchResponse<object>();
			// end::e6e31dcdd1ca214c17e375c54069d513[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""default_field"": { \<1>
			        ""type"": ""text""
			      },
			      ""boolean_sim_field"": {
			        ""type"": ""text"",
			        ""similarity"": ""boolean"" \<2>
			      }
			    }
			  }
			}");
		}
	}
}
