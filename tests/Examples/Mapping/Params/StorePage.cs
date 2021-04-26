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
	public class StorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/store.asciidoc:20")]
		public void Line20()
		{
			// tag::ff26214b3981f7418688e4c8905d5068[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::ff26214b3981f7418688e4c8905d5068[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"": ""text"",
			        ""store"": true \<1>
			      },
			      ""date"": {
			        ""type"": ""date"",
			        ""store"": true \<1>
			      },
			      ""content"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""title"":   ""Some short title"",
			  ""date"":    ""2015-01-01"",
			  ""content"": ""A very long content field...""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""stored_fields"": [ ""title"", ""date"" ] \<2>
			}");
		}
	}
}
