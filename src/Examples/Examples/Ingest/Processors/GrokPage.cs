using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Processors
{
	public class GrokPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line159()
		{
			// tag::77828fcaecc3f058c48b955928198ff6[]
			var response0 = new SearchResponse<object>();
			// end::77828fcaecc3f058c48b955928198ff6[]

			response0.MatchesExample(@"POST _ingest/pipeline/_simulate
			{
			  ""pipeline"": {
			  ""description"" : ""parse multiple patterns"",
			  ""processors"": [
			    {
			      ""grok"": {
			        ""field"": ""message"",
			        ""patterns"": [""%{FAVORITE_DOG:pet}"", ""%{FAVORITE_CAT:pet}""],
			        ""pattern_definitions"" : {
			          ""FAVORITE_DOG"" : ""beagle"",
			          ""FAVORITE_CAT"" : ""burmese""
			        }
			      }
			    }
			  ]
			},
			""docs"":[
			  {
			    ""_source"": {
			      ""message"": ""I love burmese cats!""
			    }
			  }
			  ]
			}");
		}

		[U]
		[SkipExample]
		public void Line287()
		{
			// tag::98574a419b6be603a0af8f7f22a92d23[]
			var response0 = new SearchResponse<object>();
			// end::98574a419b6be603a0af8f7f22a92d23[]

			response0.MatchesExample(@"GET _ingest/processor/grok");
		}
	}
}