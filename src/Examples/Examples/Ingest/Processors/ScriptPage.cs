using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Processors
{
	public class ScriptPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line50()
		{
			// tag::c0c7926f235e6ccc7e9a827dcc85e602[]
			var response0 = new SearchResponse<object>();
			// end::c0c7926f235e6ccc7e9a827dcc85e602[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my_index
			{
			    ""description"": ""use index:my_index and type:_doc"",
			    ""processors"": [
			      {
			        ""script"": {
			          ""source"": """"""
			            ctx._index = 'my_index';
			            ctx._type = '_doc';
			          """"""
			        }
			      }
			    ]
			}");
		}

		[U]
		[SkipExample]
		public void Line71()
		{
			// tag::cdc55ad88de55999fe2d79fd4781918b[]
			var response0 = new SearchResponse<object>();
			// end::cdc55ad88de55999fe2d79fd4781918b[]

			response0.MatchesExample(@"PUT any_index/_doc/1?pipeline=my_index
			{
			  ""message"": ""text""
			}");
		}
	}
}