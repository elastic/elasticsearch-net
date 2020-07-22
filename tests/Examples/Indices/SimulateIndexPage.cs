using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class SimulateIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/simulate-index.asciidoc:35")]
		public void Line35()
		{
			// tag::2345ccc35fa4c2df72410fe7c464ba9b[]
			var response0 = new SearchResponse<object>();
			// end::2345ccc35fa4c2df72410fe7c464ba9b[]

			response0.MatchesExample(@"POST /_index_template/_simulate_index/myindex-1");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/simulate-index.asciidoc:106")]
		public void Line106()
		{
			// tag::eebb3260fd311f2e88ed568c477e986f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::eebb3260fd311f2e88ed568c477e986f[]

			response0.MatchesExample(@"PUT /_component_template/ct1                    <1>
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_shards"": 2
			    }
			  }
			}");

			response1.MatchesExample(@"PUT /_component_template/ct2                    <2>
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_replicas"": 0
			    },
			    ""mappings"": {
			      ""properties"": {
			        ""@timestamp"": {
			          ""type"": ""date""
			        }
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"PUT /_index_template/final-template             <3>
			{
			  ""index_patterns"": [""myindex*""],
			  ""composed_of"": [""ct1"", ""ct2""],
			  ""priority"": 5
			}");

			response3.MatchesExample(@"POST /_index_template/_simulate_index/myindex-1 <4>");
		}
	}
}