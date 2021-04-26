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

namespace Examples.Mapping.Params
{
	public class DynamicPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/dynamic.asciidoc:9")]
		public void Line9()
		{
			// tag::e65e9805b8b17f72616f099e11a5c337[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::e65e9805b8b17f72616f099e11a5c337[]

			response0.MatchesExample(@"PUT my_index/_doc/1 \<1>
			{
			  ""username"": ""johnsmith"",
			  ""name"": {
			    ""first"": ""John"",
			    ""last"": ""Smith""
			  }
			}");

			response1.MatchesExample(@"GET my_index/_mapping \<2>");

			response2.MatchesExample(@"PUT my_index/_doc/2 \<3>
			{
			  ""username"": ""marywhite"",
			  ""email"": ""mary@white.com"",
			  ""name"": {
			    ""first"": ""Mary"",
			    ""middle"": ""Alice"",
			    ""last"": ""White""
			  }
			}");

			response3.MatchesExample(@"GET my_index/_mapping \<4>");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/dynamic.asciidoc:60")]
		public void Line60()
		{
			// tag::4b478d9b1231513362d2fa8c766cd0a5[]
			var response0 = new SearchResponse<object>();
			// end::4b478d9b1231513362d2fa8c766cd0a5[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic"": false, \<1>
			    ""properties"": {
			      ""user"": { \<2>
			        ""properties"": {
			          ""name"": {
			            ""type"": ""text""
			          },
			          ""social_networks"": { \<3>
			            ""dynamic"": true,
			            ""properties"": {}
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
