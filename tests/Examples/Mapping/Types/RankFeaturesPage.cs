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
	public class RankFeaturesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/rank-features.asciidoc:16")]
		public void Line16()
		{
			// tag::17867d05695ddeaee5d5aea2263ac589[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::17867d05695ddeaee5d5aea2263ac589[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""topics"": {
			        ""type"": ""rank_features"" \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""topics"": { \<2>
			    ""politics"": 20,
			    ""economics"": 50.8
			  }
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""topics"": {
			    ""politics"": 5.2,
			    ""sports"": 80.1
			  }
			}");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""rank_feature"": {
			      ""field"": ""topics.politics""
			    }
			  }
			}");
		}
	}
}
