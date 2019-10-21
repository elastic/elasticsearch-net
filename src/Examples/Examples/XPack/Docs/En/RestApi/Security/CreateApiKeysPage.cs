using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class CreateApiKeysPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line70()
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