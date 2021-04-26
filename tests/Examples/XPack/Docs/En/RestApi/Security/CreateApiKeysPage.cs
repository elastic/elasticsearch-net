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

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class CreateApiKeysPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-api-keys.asciidoc:71")]
		public void Line71()
		{
			// tag::0c8f24166d0ce7b8792781b268b544a9[]
			var response0 = new SearchResponse<object>();
			// end::0c8f24166d0ce7b8792781b268b544a9[]

			response0.MatchesExample(@"POST /_security/api_key
			{
			  ""name"": ""my-api-key"",
			  ""expiration"": ""1d"", \<1>
			  ""role_descriptors"": { \<2>
			    ""role-a"": {
			      ""cluster"": [""all""],
			      ""index"": [
			        {
			          ""names"": [""index-a*""],
			          ""privileges"": [""read""]
			        }
			      ]
			    },
			    ""role-b"": {
			      ""cluster"": [""all""],
			      ""index"": [
			        {
			          ""names"": [""index-b*""],
			          ""privileges"": [""all""]
			        }
			      ]
			    }
			  }
			}");
		}
	}
}
