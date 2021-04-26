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

namespace Examples.Search.Request
{
	public class ScriptFieldsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/script-fields.asciidoc:8")]
		public void Line8()
		{
			// tag::68358f94e77b5dce7eb01679516bae69[]
			var response0 = new SearchResponse<object>();
			// end::68358f94e77b5dce7eb01679516bae69[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match_all"": {}
			    },
			    ""script_fields"" : {
			        ""test1"" : {
			            ""script"" : {
			                ""lang"": ""painless"",
			                ""source"": ""doc['price'].value * 2""
			            }
			        },
			        ""test2"" : {
			            ""script"" : {
			                ""lang"": ""painless"",
			                ""source"": ""doc['price'].value * params.factor"",
			                ""params"" : {
			                    ""factor""  : 2.0
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/script-fields.asciidoc:44")]
		public void Line44()
		{
			// tag::34dd16c077e81b3744963b19a3dc9e49[]
			var response0 = new SearchResponse<object>();
			// end::34dd16c077e81b3744963b19a3dc9e49[]

			response0.MatchesExample(@"GET /_search
			    {
			        ""query"" : {
			            ""match_all"": {}
			        },
			        ""script_fields"" : {
			            ""test1"" : {
			                ""script"" : ""params['_source']['message']""
			            }
			        }
			    }");
		}
	}
}
