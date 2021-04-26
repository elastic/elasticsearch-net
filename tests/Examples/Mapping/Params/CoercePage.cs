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
	public class CoercePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/coerce.asciidoc:19")]
		public void Line19()
		{
			// tag::5c734d4a7252cc155f8dc90c4785f491[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::5c734d4a7252cc155f8dc90c4785f491[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": {
			        ""type"": ""integer""
			      },
			      ""number_two"": {
			        ""type"": ""integer"",
			        ""coerce"": false
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""number_one"": ""10"" \<1>
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""number_two"": ""10"" \<2>
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/coerce.asciidoc:60")]
		public void Line60()
		{
			// tag::dad2db81c728827a782a3fefd3399849[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::dad2db81c728827a782a3fefd3399849[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""index.mapping.coerce"": false
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": {
			        ""type"": ""integer"",
			        ""coerce"": true
			      },
			      ""number_two"": {
			        ""type"": ""integer""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""number_one"": ""10"" } \<1>");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""number_two"": ""10"" } \<2>");
		}
	}
}
