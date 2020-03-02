using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Licensing
{
	public class UpdateLicensePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line61()
		{
			// tag::85f2839beeb71edb66988e5c82188be0[]
			var response0 = new SearchResponse<object>();
			// end::85f2839beeb71edb66988e5c82188be0[]

			response0.MatchesExample(@"PUT _license
			{
			  ""licenses"": [
			    {
			      ""uid"":""893361dc-9749-4997-93cb-802e3d7fa4xx"",
			      ""type"":""basic"",
			      ""issue_date_in_millis"":1411948800000,
			      ""expiry_date_in_millis"":1914278399999,
			      ""max_nodes"":1,
			      ""issued_to"":""issuedTo"",
			      ""issuer"":""issuer"",
			      ""signature"":""xx""
			    }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line137()
		{
			// tag::46b1c1f6e0c86528be84c373eeb8d425[]
			var response0 = new SearchResponse<object>();
			// end::46b1c1f6e0c86528be84c373eeb8d425[]

			response0.MatchesExample(@"PUT _license?acknowledge=true
			{
			  ""licenses"": [
			    {
			      ""uid"":""893361dc-9749-4997-93cb-802e3d7fa4xx"",
			      ""type"":""basic"",
			      ""issue_date_in_millis"":1411948800000,
			      ""expiry_date_in_millis"":1914278399999,
			      ""max_nodes"":1,
			      ""issued_to"":""issuedTo"",
			      ""issuer"":""issuer"",
			      ""signature"":""xx""
			    }
			    ]
			}");
		}
	}
}