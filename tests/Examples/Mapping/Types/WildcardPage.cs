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
using System.ComponentModel;
using Nest;

namespace Examples.Mapping.Types
{
	public class WildcardPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/wildcard.asciidoc:23")]
		public void Line23()
		{
			// tag::4f272d2d69153037c3eba5864745b23e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::4f272d2d69153037c3eba5864745b23e[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_wildcard"": {
			        ""type"": ""wildcard""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_wildcard"" : ""This string can be quite lengthy""
			}");

			response2.MatchesExample(@"POST my_index/_doc/_search
			{
			  ""query"": {
			      ""wildcard"" : ""*quite*lengthy""
			  }
			}");

			response3.MatchesExample(@"");
		}
	}
}
