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

namespace Examples.Scripting
{
	public class EnginePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("scripting/engine.asciidoc:28")]
		public void Line28()
		{
			// tag::d9de409a4a197ce7cbe3714e07155d34[]
			var response0 = new SearchResponse<object>();
			// end::d9de409a4a197ce7cbe3714e07155d34[]

			response0.MatchesExample(@"POST /_search
			{
			  ""query"": {
			    ""function_score"": {
			      ""query"": {
			        ""match"": {
			          ""body"": ""foo""
			        }
			      },
			      ""functions"": [
			        {
			          ""script_score"": {
			            ""script"": {
			                ""source"": ""pure_df"",
			                ""lang"" : ""expert_scripts"",
			                ""params"": {
			                    ""field"": ""body"",
			                    ""term"": ""foo""
			                }
			            }
			          }
			        }
			      ]
			    }
			  }
			}");
		}
	}
}
