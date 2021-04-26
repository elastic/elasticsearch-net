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

namespace Examples.Indices
{
	public class SimulateTemplatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/simulate-template.asciidoc:58")]
		public void Line58()
		{
			// tag::0f7aa40ad26d59a9268630b980a3d594[]
			var response0 = new SearchResponse<object>();
			// end::0f7aa40ad26d59a9268630b980a3d594[]

			response0.MatchesExample(@"POST /_index_template/_simulate/template_1");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/simulate-template.asciidoc:179")]
		public void Line179()
		{
			// tag::b38a702277d7aaabf31971c0ade265ae[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::b38a702277d7aaabf31971c0ade265ae[]

			response0.MatchesExample(@"PUT /_component_template/ct1                   <1>
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_shards"": 2
			    }
			  }
			}");

			response1.MatchesExample(@"PUT /_component_template/ct2                    <2>
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_replicas"": 0
			    },
			    ""mappings"": {
			      ""properties"": {
			        ""@timestamp"": {
			          ""type"": ""date""
			        }
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"PUT /_index_template/final-template            <3>
			{
			  ""index_patterns"": [""myindex*""],
			  ""composed_of"": [""ct1"", ""ct2""],
			  ""priority"": 5
			}");

			response3.MatchesExample(@"POST /_index_template/_simulate/final-template <4>");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/simulate-template.asciidoc:256")]
		public void Line256()
		{
			// tag::3628c268a9e764edb99b89616f58686f[]
			var response0 = new SearchResponse<object>();
			// end::3628c268a9e764edb99b89616f58686f[]

			response0.MatchesExample(@"POST /_index_template/_simulate
			{
			  ""index_patterns"": [""myindex*""],
			  ""composed_of"": [""ct2""],
			  ""priority"": 10,
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_replicas"": 1
			    }
			  }
			}");
		}
	}
}