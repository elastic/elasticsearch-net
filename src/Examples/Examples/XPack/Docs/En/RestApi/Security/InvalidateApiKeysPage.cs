using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class InvalidateApiKeysPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::0aff04881be21eea45375ec4f4f50e66[]
			var response0 = new SearchResponse<object>();
			// end::0aff04881be21eea45375ec4f4f50e66[]

			response0.MatchesExample(@"POST /_security/api_key
			{
			  ""name"": ""my-api-key""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line97()
		{
			// tag::01cc9dac719f2612a48cc1b23db7cd54[]
			var response0 = new SearchResponse<object>();
			// end::01cc9dac719f2612a48cc1b23db7cd54[]

			response0.MatchesExample(@"DELETE /_security/api_key
			{
			  ""id"" : ""VuaCfGcBCdbkQm-e5aOx""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line110()
		{
			// tag::f388e571224dd6850f8c9f9f08fca3da[]
			var response0 = new SearchResponse<object>();
			// end::f388e571224dd6850f8c9f9f08fca3da[]

			response0.MatchesExample(@"DELETE /_security/api_key
			{
			  ""name"" : ""my-api-key""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line121()
		{
			// tag::dde283eab92608e7bfbfa09c6482a12e[]
			var response0 = new SearchResponse<object>();
			// end::dde283eab92608e7bfbfa09c6482a12e[]

			response0.MatchesExample(@"DELETE /_security/api_key
			{
			  ""realm_name"" : ""native1""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line132()
		{
			// tag::e7d819634d765cde269e2669e2dc677f[]
			var response0 = new SearchResponse<object>();
			// end::e7d819634d765cde269e2669e2dc677f[]

			response0.MatchesExample(@"DELETE /_security/api_key
			{
			  ""username"" : ""myuser""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line143()
		{
			// tag::0ad8aa0d684b09500aa30b4c4d6f29c8[]
			var response0 = new SearchResponse<object>();
			// end::0ad8aa0d684b09500aa30b4c4d6f29c8[]

			response0.MatchesExample(@"DELETE /_security/api_key
			{
			  ""id"" : ""VuaCfGcBCdbkQm-e5aOx"",
			  ""owner"" : ""true""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line155()
		{
			// tag::cfad3631be0634ee49c424f9ccec62d9[]
			var response0 = new SearchResponse<object>();
			// end::cfad3631be0634ee49c424f9ccec62d9[]

			response0.MatchesExample(@"DELETE /_security/api_key
			{
			  ""owner"" : ""true""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line166()
		{
			// tag::6c927313867647e0ef3cd3a37cb410cc[]
			var response0 = new SearchResponse<object>();
			// end::6c927313867647e0ef3cd3a37cb410cc[]

			response0.MatchesExample(@"DELETE /_security/api_key
			{
			  ""username"" : ""myuser"",
			  ""realm_name"" : ""native1""
			}");
		}
	}
}