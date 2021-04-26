/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
