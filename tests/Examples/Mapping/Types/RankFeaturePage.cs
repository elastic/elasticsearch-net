// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class RankFeaturePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/rank-feature.asciidoc:11")]
		public void Line11()
		{
			// tag::1e088f892b20697fd6e537a3ecf624ee[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::1e088f892b20697fd6e537a3ecf624ee[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""pagerank"": {
			        ""type"": ""rank_feature"" \<1>
			      },
			      ""url_length"": {
			        ""type"": ""rank_feature"",
			        ""positive_score_impact"": false \<2>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""pagerank"": 8,
			  ""url_length"": 22
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""rank_feature"": {
			      ""field"": ""pagerank""
			    }
			  }
			}");
		}
	}
}
