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

namespace Examples.Docs
{
	public class MultiTermvectorsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("docs/multi-termvectors.asciidoc:10")]
		public void Line10()
		{
			// tag::c6d18f08822463356b297f238c6650d9[]
			var response0 = new SearchResponse<object>();
			// end::c6d18f08822463356b297f238c6650d9[]

			response0.MatchesExample(@"POST /_mtermvectors
			{
			   ""docs"": [
			      {
			         ""_index"": ""twitter"",
			         ""_id"": ""2"",
			         ""term_statistics"": true
			      },
			      {
			         ""_index"": ""twitter"",
			         ""_id"": ""1"",
			         ""fields"": [
			            ""message""
			         ]
			      }
			   ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-termvectors.asciidoc:91")]
		public void Line91()
		{
			// tag::2c8638acc208bd0a47403c1f054fde21[]
			var response0 = new SearchResponse<object>();
			// end::2c8638acc208bd0a47403c1f054fde21[]

			response0.MatchesExample(@"POST /twitter/_mtermvectors
			{
			   ""docs"": [
			      {
			         ""_id"": ""2"",
			         ""fields"": [
			            ""message""
			         ],
			         ""term_statistics"": true
			      },
			      {
			         ""_id"": ""1""
			      }
			   ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-termvectors.asciidoc:114")]
		public void Line114()
		{
			// tag::f31eea58baf0dbd39823ff9100c9ce28[]
			var response0 = new SearchResponse<object>();
			// end::f31eea58baf0dbd39823ff9100c9ce28[]

			response0.MatchesExample(@"POST /twitter/_mtermvectors
			{
			    ""ids"" : [""1"", ""2""],
			    ""parameters"": {
			    	""fields"": [
			         	""message""
			      	],
			      	""term_statistics"": true
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-termvectors.asciidoc:135")]
		public void Line135()
		{
			// tag::29840a67fdc13cd329ca2c69a2303e83[]
			var response0 = new SearchResponse<object>();
			// end::29840a67fdc13cd329ca2c69a2303e83[]

			response0.MatchesExample(@"POST /_mtermvectors
			{
			   ""docs"": [
			      {
			         ""_index"": ""twitter"",
			         ""doc"" : {
			            ""user"" : ""John Doe"",
			            ""message"" : ""twitter test test test""
			         }
			      },
			      {
			         ""_index"": ""twitter"",
			         ""doc"" : {
			           ""user"" : ""Jane Doe"",
			           ""message"" : ""Another twitter test ...""
			         }
			      }
			   ]
			}");
		}
	}
}
