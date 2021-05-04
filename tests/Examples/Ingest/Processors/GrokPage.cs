// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Processors
{
	public class GrokPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/grok.asciidoc:72")]
		public void Line72()
		{
			// tag::5024c524a7db0d6bb44c1820007cc5f4[]
			var response0 = new SearchResponse<object>();
			// end::5024c524a7db0d6bb44c1820007cc5f4[]

			response0.MatchesExample(@"POST _ingest/pipeline/_simulate
			{
			  ""pipeline"": {
			    ""description"" : ""..."",
			    ""processors"": [
			      {
			        ""grok"": {
			          ""field"": ""message"",
			          ""patterns"": [""%{IP:client} %{WORD:method} %{URIPATHPARAM:request} %{NUMBER:bytes:int} %{NUMBER:duration:double}""]
			        }
			      }
			    ]
			  },
			  ""docs"":[
			    {
			      ""_source"": {
			        ""message"": ""55.3.244.1 GET /index.html 15824 0.043""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/grok.asciidoc:164")]
		public void Line164()
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

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/grok.asciidoc:288")]
		public void Line288()
		{
			// tag::98574a419b6be603a0af8f7f22a92d23[]
			var response0 = new SearchResponse<object>();
			// end::98574a419b6be603a0af8f7f22a92d23[]

			response0.MatchesExample(@"GET _ingest/processor/grok");
		}
	}
}
