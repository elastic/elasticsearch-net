// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetApiKeysPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:65")]
		public void Line65()
		{
			// tag::8d3be5482270921111754772479f8676[]
			var response0 = new SearchResponse<object>();
			// end::8d3be5482270921111754772479f8676[]

			response0.MatchesExample(@"POST /_security/api_key
			{
			  ""name"": ""my-api-key"",
			  ""role_descriptors"": {}
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:90")]
		public void Line90()
		{
			// tag::701f1fffc65e9e51c96aa60261e2eae3[]
			var response0 = new SearchResponse<object>();
			// end::701f1fffc65e9e51c96aa60261e2eae3[]

			response0.MatchesExample(@"GET /_security/api_key?id=VuaCfGcBCdbkQm-e5aOx");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:99")]
		public void Line99()
		{
			// tag::7b864d61767ab283cfd5f9b9ba784b1f[]
			var response0 = new SearchResponse<object>();
			// end::7b864d61767ab283cfd5f9b9ba784b1f[]

			response0.MatchesExample(@"GET /_security/api_key?name=my-api-key");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:107")]
		public void Line107()
		{
			// tag::d1e0fee64389e7c8d4c092030626b61f[]
			var response0 = new SearchResponse<object>();
			// end::d1e0fee64389e7c8d4c092030626b61f[]

			response0.MatchesExample(@"GET /_security/api_key?name=my-*");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:115")]
		public void Line115()
		{
			// tag::10d9da8a3b7061479be908c8c5c76cfb[]
			var response0 = new SearchResponse<object>();
			// end::10d9da8a3b7061479be908c8c5c76cfb[]

			response0.MatchesExample(@"GET /_security/api_key?realm_name=native1");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:123")]
		public void Line123()
		{
			// tag::62eafc5b3ab75cc67314d5a8567d6077[]
			var response0 = new SearchResponse<object>();
			// end::62eafc5b3ab75cc67314d5a8567d6077[]

			response0.MatchesExample(@"GET /_security/api_key?username=myuser");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:131")]
		public void Line131()
		{
			// tag::9608820dbeac261ba53fb89bb9400560[]
			var response0 = new SearchResponse<object>();
			// end::9608820dbeac261ba53fb89bb9400560[]

			response0.MatchesExample(@"GET /_security/api_key?owner=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:138")]
		public void Line138()
		{
			// tag::ca5ae0eb7709f3807bc6239cd4bd9141[]
			var response0 = new SearchResponse<object>();
			// end::ca5ae0eb7709f3807bc6239cd4bd9141[]

			response0.MatchesExample(@"GET /_security/api_key");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:146")]
		public void Line146()
		{
			// tag::fffaa7ecef94e1404ebec2f9069448e3[]
			var response0 = new SearchResponse<object>();
			// end::fffaa7ecef94e1404ebec2f9069448e3[]

			response0.MatchesExample(@"POST /_security/api_key
			{
			  ""name"": ""my-api-key-1""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:157")]
		public void Line157()
		{
			// tag::dffbbdc4025e5777c647d8818847b960[]
			var response0 = new SearchResponse<object>();
			// end::dffbbdc4025e5777c647d8818847b960[]

			response0.MatchesExample(@"GET /_security/api_key?id=VuaCfGcBCdbkQm-e5aOx&owner=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/get-api-keys.asciidoc:167")]
		public void Line167()
		{
			// tag::30abc76a39e551f4b52c65002bb6405d[]
			var response0 = new SearchResponse<object>();
			// end::30abc76a39e551f4b52c65002bb6405d[]

			response0.MatchesExample(@"GET /_security/api_key?username=myuser&realm_name=native1");
		}
	}
}
