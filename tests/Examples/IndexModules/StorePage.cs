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

namespace Examples.IndexModules
{
	public class StorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/store.asciidoc:30")]
		public void Line30()
		{
			// tag::fba99da14d4323c91794703438979912[]
			var response0 = new SearchResponse<object>();
			// end::fba99da14d4323c91794703438979912[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.store.type"": ""hybridfs""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/store.asciidoc:120")]
		public void Line120()
		{
			// tag::9ba2e779fe3e9d12ed5fca1ba3f8be97[]
			var response0 = new SearchResponse<object>();
			// end::9ba2e779fe3e9d12ed5fca1ba3f8be97[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.store.preload"": [""nvd"", ""dvd""]
			  }
			}");
		}
	}
}
