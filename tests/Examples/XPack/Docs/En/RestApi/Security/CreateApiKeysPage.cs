// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
