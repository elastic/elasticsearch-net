using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Licensing
{
	public class UpdateLicensePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line59()
		{
			// tag::4fb399ee372ae8837cbb9aa66be30f62[]
			var response0 = new SearchResponse<object>();
			// end::4fb399ee372ae8837cbb9aa66be30f62[]

			response0.MatchesExample(@"POST /_license
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
		public void Line135()
		{
			// tag::efe30c2a1611afdc85ae522e4f5a457b[]
			var response0 = new SearchResponse<object>();
			// end::efe30c2a1611afdc85ae522e4f5a457b[]

			response0.MatchesExample(@"POST /_license?acknowledge=true
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