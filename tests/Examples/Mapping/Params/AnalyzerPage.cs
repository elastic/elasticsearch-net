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
	public class AnalyzerPage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/analyzer.asciidoc:35")]
		public void Line35()
		{
			// tag::5bf1e4194dce1e15eb7f48fd72b1fc6b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::5bf1e4194dce1e15eb7f48fd72b1fc6b[]

			response0.MatchesExample(@"PUT my_index
			{
			   ""settings"":{
			      ""analysis"":{
			         ""analyzer"":{
			            ""my_analyzer"":{ \<1>
			               ""type"":""custom"",
			               ""tokenizer"":""standard"",
			               ""filter"":[
			                  ""lowercase""
			               ]
			            },
			            ""my_stop_analyzer"":{ \<2>
			               ""type"":""custom"",
			               ""tokenizer"":""standard"",
			               ""filter"":[
			                  ""lowercase"",
			                  ""english_stop""
			               ]
			            }
			         },
			         ""filter"":{
			            ""english_stop"":{
			               ""type"":""stop"",
			               ""stopwords"":""_english_""
			            }
			         }
			      }
			   },
			   ""mappings"":{
			       ""properties"":{
			          ""title"": {
			             ""type"":""text"",
			             ""analyzer"":""my_analyzer"", \<3>
			             ""search_analyzer"":""my_stop_analyzer"", \<4>
			             ""search_quote_analyzer"":""my_analyzer"" \<5>
			         }
			      }
			   }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			   ""title"":""The Quick Brown Fox""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			   ""title"":""A Quick Brown Fox""
			}");

			response3.MatchesExample(@"GET my_index/_search
			{
			   ""query"":{
			      ""query_string"":{
			         ""query"":""\""the quick brown fox\"""" \<6>
			      }
			   }
			}");
		}
	}
}
