// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Processors
{
	public class UserAgentPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/user-agent.asciidoc:27")]
		public void Line27()
		{
			// tag::9c504b5c486d9df689a22b11412e61a3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::9c504b5c486d9df689a22b11412e61a3[]

			response0.MatchesExample(@"PUT _ingest/pipeline/user_agent
			{
			  ""description"" : ""Add user agent information"",
			  ""processors"" : [
			    {
			      ""user_agent"" : {
			        ""field"" : ""agent""
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"PUT my_index/_doc/my_id?pipeline=user_agent
			{
			  ""agent"": ""Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36""
			}");

			response2.MatchesExample(@"GET my_index/_doc/my_id");
		}
	}
}
