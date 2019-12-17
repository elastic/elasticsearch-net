using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class BoostPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::dcef5a46104e2602a0b9f5d968f66f4d[]
			var response0 = new SearchResponse<object>();
			// end::dcef5a46104e2602a0b9f5d968f66f4d[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"": ""text"",
			        ""boost"": 2 \<1>
			      },
			      ""content"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::df827f97ecf543a1722003edbf277c01[]
			var response0 = new SearchResponse<object>();
			// end::df827f97ecf543a1722003edbf277c01[]

			response0.MatchesExample(@"POST _search
			{
			    ""query"": {
			        ""match"" : {
			            ""title"": {
			                ""query"": ""quick brown fox""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::36a07d7014cdd3d6cd9d97651e66e7ef[]
			var response0 = new SearchResponse<object>();
			// end::36a07d7014cdd3d6cd9d97651e66e7ef[]

			response0.MatchesExample(@"POST _search
			{
			    ""query"": {
			        ""match"" : {
			            ""title"": {
			                ""query"": ""quick brown fox"",
			                ""boost"": 2
			            }
			        }
			    }
			}");
		}
	}
}