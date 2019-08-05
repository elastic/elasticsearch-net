using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis
{
	public class PutPipelinePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line7()
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
	}
}