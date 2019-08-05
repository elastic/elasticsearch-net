using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis
{
	public class SimulatePipelinePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line50()
		{
			// tag::68168bb8190037f0c1ea1254f5f5e5a0[]
			var response0 = new SearchResponse<object>();
			// end::68168bb8190037f0c1ea1254f5f5e5a0[]

			response0.MatchesExample(@"POST _ingest/pipeline/_simulate
			{
			  ""pipeline"" :
			  {
			    ""description"": ""_description"",
			    ""processors"": [
			      {
			        ""set"" : {
			          ""field"" : ""field2"",
			          ""value"" : ""_value""
			        }
			      }
			    ]
			  },
			  ""docs"": [
			    {
			      ""_index"": ""index"",
			      ""_id"": ""id"",
			      ""_source"": {
			        ""foo"": ""bar""
			      }
			    },
			    {
			      ""_index"": ""index"",
			      ""_id"": ""id"",
			      ""_source"": {
			        ""foo"": ""rab""
			      }
			    }
			  ]
			}");
		}

		[U]
		[SkipExample]
		public void Line135()
		{
			// tag::6ee061e58bf07bd6a678d210811e2000[]
			var response0 = new SearchResponse<object>();
			// end::6ee061e58bf07bd6a678d210811e2000[]

			response0.MatchesExample(@"POST _ingest/pipeline/_simulate?verbose
			{
			  ""pipeline"" :
			  {
			    ""description"": ""_description"",
			    ""processors"": [
			      {
			        ""set"" : {
			          ""field"" : ""field2"",
			          ""value"" : ""_value2""
			        }
			      },
			      {
			        ""set"" : {
			          ""field"" : ""field3"",
			          ""value"" : ""_value3""
			        }
			      }
			    ]
			  },
			  ""docs"": [
			    {
			      ""_index"": ""index"",
			      ""_id"": ""id"",
			      ""_source"": {
			        ""foo"": ""bar""
			      }
			    },
			    {
			      ""_index"": ""index"",
			      ""_id"": ""id"",
			      ""_source"": {
			        ""foo"": ""rab""
			      }
			    }
			  ]
			}");
		}
	}
}