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

namespace Examples.Mapping.Types
{
	public class SparseVectorPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line27()
		{
			// tag::9e9bd85e9135533e7fb8b079a6d4ae21[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::9e9bd85e9135533e7fb8b079a6d4ae21[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_vector"": {
			        ""type"": ""sparse_vector""
			      },
			      ""my_text"" : {
			        ""type"" : ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_text"" : ""text1"",
			  ""my_vector"" : {""1"": 0.5, ""5"": -0.5,  ""100"": 1}
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""my_text"" : ""text2"",
			  ""my_vector"" : {""103"": 0.5, ""4"": -0.5,  ""5"": 1, ""11"" : 1.2}
			}");

			response3.MatchesExample(@"");
		}
	}
}
