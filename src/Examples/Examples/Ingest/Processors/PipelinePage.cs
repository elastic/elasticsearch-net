using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Processors
{
	public class PipelinePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line29()
		{
			// tag::8494d09c39e109a012094eb9d6ec52ac[]
			var response0 = new SearchResponse<object>();
			// end::8494d09c39e109a012094eb9d6ec52ac[]

			response0.MatchesExample(@"PUT _ingest/pipeline/pipelineA
			{
			  ""description"" : ""inner pipeline"",
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""inner_pipeline_set"",
			        ""value"": ""inner""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line48()
		{
			// tag::02c48d461536709c3fc8a0e8147c3787[]
			var response0 = new SearchResponse<object>();
			// end::02c48d461536709c3fc8a0e8147c3787[]

			response0.MatchesExample(@"PUT _ingest/pipeline/pipelineB
			{
			  ""description"" : ""outer pipeline"",
			  ""processors"" : [
			    {
			      ""pipeline"" : {
			        ""name"": ""pipelineA""
			      }
			    },
			    {
			      ""set"" : {
			        ""field"": ""outer_pipeline_set"",
			        ""value"": ""outer""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line74()
		{
			// tag::88647e818ffcbe39e5cf627f5b9a676c[]
			var response0 = new SearchResponse<object>();
			// end::88647e818ffcbe39e5cf627f5b9a676c[]

			response0.MatchesExample(@"PUT /myindex/_doc/1?pipeline=pipelineB
			{
			  ""field"": ""value""
			}");
		}
	}
}