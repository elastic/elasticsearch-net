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
	public class IndexPrefixesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/index-prefixes.asciidoc:21")]
		public void Line21()
		{
			// tag::ff5d15a265855b1c11cb20ceef6a1b58[]
			var response0 = new SearchResponse<object>();
			// end::ff5d15a265855b1c11cb20ceef6a1b58[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""body_text"": {
			        ""type"": ""text"",
			        ""index_prefixes"": { }    \<1>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/index-prefixes.asciidoc:41")]
		public void Line41()
		{
			// tag::b19ec4a20c19082e5c40e3b1f28bfbcb[]
			var response0 = new SearchResponse<object>();
			// end::b19ec4a20c19082e5c40e3b1f28bfbcb[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""full_name"": {
			        ""type"": ""text"",
			        ""index_prefixes"": {
			          ""min_chars"" : 1,
			          ""max_chars"" : 10
			        }
			      }
			    }
			  }
			}");
		}
	}
}
