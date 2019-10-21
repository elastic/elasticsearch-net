using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetApiKeysPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
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
		public void Line90()
		{
			// tag::701f1fffc65e9e51c96aa60261e2eae3[]
			var response0 = new SearchResponse<object>();
			// end::701f1fffc65e9e51c96aa60261e2eae3[]

			response0.MatchesExample(@"GET /_security/api_key?id=VuaCfGcBCdbkQm-e5aOx");
		}

		[U(Skip = "Example not implemented")]
		public void Line99()
		{
			// tag::7b864d61767ab283cfd5f9b9ba784b1f[]
			var response0 = new SearchResponse<object>();
			// end::7b864d61767ab283cfd5f9b9ba784b1f[]

			response0.MatchesExample(@"GET /_security/api_key?name=my-api-key");
		}

		[U(Skip = "Example not implemented")]
		public void Line107()
		{
			// tag::10d9da8a3b7061479be908c8c5c76cfb[]
			var response0 = new SearchResponse<object>();
			// end::10d9da8a3b7061479be908c8c5c76cfb[]

			response0.MatchesExample(@"GET /_security/api_key?realm_name=native1");
		}

		[U(Skip = "Example not implemented")]
		public void Line115()
		{
			// tag::62eafc5b3ab75cc67314d5a8567d6077[]
			var response0 = new SearchResponse<object>();
			// end::62eafc5b3ab75cc67314d5a8567d6077[]

			response0.MatchesExample(@"GET /_security/api_key?username=myuser");
		}

		[U(Skip = "Example not implemented")]
		public void Line123()
		{
			// tag::9608820dbeac261ba53fb89bb9400560[]
			var response0 = new SearchResponse<object>();
			// end::9608820dbeac261ba53fb89bb9400560[]

			response0.MatchesExample(@"GET /_security/api_key?owner=true");
		}

		[U(Skip = "Example not implemented")]
		public void Line130()
		{
			// tag::ca5ae0eb7709f3807bc6239cd4bd9141[]
			var response0 = new SearchResponse<object>();
			// end::ca5ae0eb7709f3807bc6239cd4bd9141[]

			response0.MatchesExample(@"GET /_security/api_key");
		}

		[U(Skip = "Example not implemented")]
		public void Line138()
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
		public void Line149()
		{
			// tag::dffbbdc4025e5777c647d8818847b960[]
			var response0 = new SearchResponse<object>();
			// end::dffbbdc4025e5777c647d8818847b960[]

			response0.MatchesExample(@"GET /_security/api_key?id=VuaCfGcBCdbkQm-e5aOx&owner=true");
		}

		[U(Skip = "Example not implemented")]
		public void Line159()
		{
			// tag::30abc76a39e551f4b52c65002bb6405d[]
			var response0 = new SearchResponse<object>();
			// end::30abc76a39e551f4b52c65002bb6405d[]

			response0.MatchesExample(@"GET /_security/api_key?username=myuser&realm_name=native1");
		}
	}
}