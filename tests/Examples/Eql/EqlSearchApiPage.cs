using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Eql
{
	public class EqlSearchApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("eql/eql-search-api.asciidoc:35")]
		public void Line35()
		{
			// tag::194ef7d5af279d9ca4d639e0b7eb5cc3[]
			var response0 = new SearchResponse<object>();
			// end::194ef7d5af279d9ca4d639e0b7eb5cc3[]

			response0.MatchesExample(@"GET /my_index/_eql/search
			{
			  ""query"": """"""
			    process where process.name = ""regsvr32.exe""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/eql-search-api.asciidoc:310")]
		public void Line310()
		{
			// tag::5ee14675ed74281a57f348c08587a56d[]
			var response0 = new SearchResponse<object>();
			// end::5ee14675ed74281a57f348c08587a56d[]

			response0.MatchesExample(@"GET /my_index/_eql/search
			{
			  ""query"": """"""
			    file where (file.name == ""cmd.exe"" and agent.id != ""my_user"")
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/eql-search-api.asciidoc:420")]
		public void Line420()
		{
			// tag::e5da3fd0dc6911375c47412c74b21bfd[]
			var response0 = new SearchResponse<object>();
			// end::e5da3fd0dc6911375c47412c74b21bfd[]

			response0.MatchesExample(@"GET /my_index/_eql/search
			{
			  ""query"": """"""
			    sequence by agent.id
			      [ file where file.name == ""cmd.exe"" and agent.id != ""my_user"" ]
			      [ process where stringContains(process.path, ""regsvr32"") ]
			  """"""
			}");
		}
	}
}