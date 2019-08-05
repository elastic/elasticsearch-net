using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis
{
	public class GetPipelinePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line28()
		{
			// tag::375bd91eeb64a865c49352ef0c745a0a[]
			var response0 = new SearchResponse<object>();
			// end::375bd91eeb64a865c49352ef0c745a0a[]

			response0.MatchesExample(@"GET _ingest/pipeline/my-pipeline-id");
		}

		[U]
		[SkipExample]
		public void Line69()
		{
			// tag::8fc926f8c03c4a03afee543370d92f66[]
			var response0 = new SearchResponse<object>();
			// end::8fc926f8c03c4a03afee543370d92f66[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my-pipeline-id
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

		[U]
		[SkipExample]
		public void Line91()
		{
			// tag::9f549bb400b6cc1523b00d60bc8fd8e1[]
			var response0 = new SearchResponse<object>();
			// end::9f549bb400b6cc1523b00d60bc8fd8e1[]

			response0.MatchesExample(@"GET /_ingest/pipeline/my-pipeline-id?filter_path=*.version");
		}
	}
}