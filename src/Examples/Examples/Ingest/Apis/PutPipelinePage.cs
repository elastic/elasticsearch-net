using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis
{
	public class PutPipelinePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::e7e28812b86c5257bf48931d131409f0[]
			var response0 = new SearchResponse<object>();
			// end::e7e28812b86c5257bf48931d131409f0[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my-pipeline-id
			{
			  ""description"" : ""describe pipeline"",
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line93()
		{
			// tag::3467d11d772321353951461b756e3ffb[]
			var response0 = new SearchResponse<object>();
			// end::3467d11d772321353951461b756e3ffb[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/my-pipeline-id
			{
			  ""description"" : ""describe pipeline"",
			  ""version"" : 123,
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line113()
		{
			// tag::ca42bbe58bdd127eb042389699d4a45c[]
			var response0 = new SearchResponse<object>();
			// end::ca42bbe58bdd127eb042389699d4a45c[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/my-pipeline-id
			{
			  ""description"" : ""describe pipeline"",
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			    }
			  ]
			}");
		}
	}
}