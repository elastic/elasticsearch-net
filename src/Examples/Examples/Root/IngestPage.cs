using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class IngestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line32()
		{
			// tag::55704b69b03239fe13293fc7622d27da[]
			var response0 = new SearchResponse<object>();
			// end::55704b69b03239fe13293fc7622d27da[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my_pipeline_id
			{
			  ""description"" : ""describe pipeline"",
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""new""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line52()
		{
			// tag::6f3a4b4a01b6fae193897f00cb4855d0[]
			var response0 = new SearchResponse<object>();
			// end::6f3a4b4a01b6fae193897f00cb4855d0[]

			response0.MatchesExample(@"PUT my-index/_doc/my-id?pipeline=my_pipeline_id
			{
			  ""foo"": ""bar""
			}");
		}
	}
}