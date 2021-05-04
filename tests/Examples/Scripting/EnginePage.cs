// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
