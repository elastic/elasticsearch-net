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

namespace Examples.Search.Suggesters
{
	public class MiscPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/misc.asciidoc:10")]
		public void Line10()
		{
			// tag::e194e9cbe3eb2305f4f7cdda0cf529bd[]
			var response0 = new SearchResponse<object>();
			// end::e194e9cbe3eb2305f4f7cdda0cf529bd[]

			response0.MatchesExample(@"POST _search?typed_keys
			{
			  ""suggest"": {
			    ""text"" : ""some test mssage"",
			    ""my-first-suggester"" : {
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    },
			    ""my-second-suggester"" : {
			      ""phrase"" : {
			        ""field"" : ""message""
			      }
			    }
			  }
			}");
		}
	}
}
