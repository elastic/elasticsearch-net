// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Eql
{
	public class EqlSearchApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("eql/eql-search-api.asciidoc:38")]
		public void Line38()
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
		[Description("eql/eql-search-api.asciidoc:512")]
		public void Line512()
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
		[Description("eql/eql-search-api.asciidoc:629")]
		public void Line629()
		{
			// tag::6fc42a171ace53e4916e4fbb23c5c5f0[]
			var response0 = new SearchResponse<object>();
			// end::6fc42a171ace53e4916e4fbb23c5c5f0[]

			response0.MatchesExample(@"GET /my_index/_eql/search
			{
			  ""query"": """"""
			    sequence by agent.id
			      [ file where file.name == ""cmd.exe"" and agent.id != ""my_user"" ]
			      [ process where stringContains(process.executable, ""regsvr32"") ]
			  """"""
			}");
		}
	}
}