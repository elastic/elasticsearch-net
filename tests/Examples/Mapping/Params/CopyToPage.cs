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
	public class CopyToPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/copy-to.asciidoc:10")]
		public void Line10()
		{
			// tag::363d200a378f8c3acc6d8a77df42eba7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::363d200a378f8c3acc6d8a77df42eba7[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""first_name"": {
			        ""type"": ""text"",
			        ""copy_to"": ""full_name"" \<1>
			      },
			      ""last_name"": {
			        ""type"": ""text"",
			        ""copy_to"": ""full_name"" \<1>
			      },
			      ""full_name"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""first_name"": ""John"",
			  ""last_name"": ""Smith""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""full_name"": { \<2>
			        ""query"": ""John Smith"",
			        ""operator"": ""and""
			      }
			    }
			  }
			}");

			response3.MatchesExample(@"");
		}
	}
}
