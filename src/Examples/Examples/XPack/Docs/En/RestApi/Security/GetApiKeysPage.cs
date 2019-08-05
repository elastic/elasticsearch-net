using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetApiKeysPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line51()
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

		[U]
		[SkipExample]
		public void Line78()
		{
			// tag::701f1fffc65e9e51c96aa60261e2eae3[]
			var response0 = new SearchResponse<object>();
			// end::701f1fffc65e9e51c96aa60261e2eae3[]

			response0.MatchesExample(@"GET /_security/api_key?id=VuaCfGcBCdbkQm-e5aOx");
		}

		[U]
		[SkipExample]
		public void Line88()
		{
			// tag::7b864d61767ab283cfd5f9b9ba784b1f[]
			var response0 = new SearchResponse<object>();
			// end::7b864d61767ab283cfd5f9b9ba784b1f[]

			response0.MatchesExample(@"GET /_security/api_key?name=my-api-key");
		}

		[U]
		[SkipExample]
		public void Line97()
		{
			// tag::10d9da8a3b7061479be908c8c5c76cfb[]
			var response0 = new SearchResponse<object>();
			// end::10d9da8a3b7061479be908c8c5c76cfb[]

			response0.MatchesExample(@"GET /_security/api_key?realm_name=native1");
		}

		[U]
		[SkipExample]
		public void Line106()
		{
			// tag::62eafc5b3ab75cc67314d5a8567d6077[]
			var response0 = new SearchResponse<object>();
			// end::62eafc5b3ab75cc67314d5a8567d6077[]

			response0.MatchesExample(@"GET /_security/api_key?username=myuser");
		}

		[U]
		[SkipExample]
		public void Line116()
		{
			// tag::30abc76a39e551f4b52c65002bb6405d[]
			var response0 = new SearchResponse<object>();
			// end::30abc76a39e551f4b52c65002bb6405d[]

			response0.MatchesExample(@"GET /_security/api_key?username=myuser&realm_name=native1");
		}
	}
}