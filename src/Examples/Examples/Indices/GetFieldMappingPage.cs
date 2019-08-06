using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class GetFieldMappingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::ba3a852ba26b650bc23be38ecebda5e4[]
			var response0 = new SearchResponse<object>();
			// end::ba3a852ba26b650bc23be38ecebda5e4[]

			response0.MatchesExample(@"PUT publications
			{
			    ""mappings"": {
			        ""properties"": {
			            ""id"": { ""type"": ""text"" },
			            ""title"":  { ""type"": ""text""},
			            ""abstract"": { ""type"": ""text""},
			            ""author"": {
			                ""properties"": {
			                    ""id"": { ""type"": ""text"" },
			                    ""name"": { ""type"": ""text"" }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::299900fb08da80fe455cf3f1bb7d62ee[]
			var response0 = new SearchResponse<object>();
			// end::299900fb08da80fe455cf3f1bb7d62ee[]

			response0.MatchesExample(@"GET publications/_mapping/field/title");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::9af393bb38bf098d65d00e7637824f44[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::9af393bb38bf098d65d00e7637824f44[]

			response0.MatchesExample(@"GET /twitter,kimchy/_mapping/field/message");

			response1.MatchesExample(@"GET /_all/_mapping/field/message,user.id");

			response2.MatchesExample(@"GET /_all/_mapping/field/*.id");
		}

		[U(Skip = "Example not implemented")]
		public void Line91()
		{
			// tag::ed3bdf4d6799b43526851e92b6a60c55[]
			var response0 = new SearchResponse<object>();
			// end::ed3bdf4d6799b43526851e92b6a60c55[]

			response0.MatchesExample(@"GET publications/_mapping/field/author.id,abstract,name");
		}

		[U(Skip = "Example not implemented")]
		public void Line128()
		{
			// tag::b61afb7ca29a11243232ffcc8b5a43cf[]
			var response0 = new SearchResponse<object>();
			// end::b61afb7ca29a11243232ffcc8b5a43cf[]

			response0.MatchesExample(@"GET publications/_mapping/field/a*");
		}
	}
}