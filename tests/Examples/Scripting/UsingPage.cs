using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Scripting
{
	public class UsingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("scripting/using.asciidoc:24")]
		public void Line24()
		{
			// tag::e62cf588bfc891504bbf933af86eed7c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::e62cf588bfc891504bbf933af86eed7c[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_field"": 5
			}");

			response1.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"": {
			    ""my_doubled_field"": {
			      ""script"": {
			        ""lang"":   ""expression"",
			        ""source"": ""doc['my_field'] * multiplier"",
			        ""params"": {
			          ""multiplier"": 2
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("scripting/using.asciidoc:147")]
		public void Line147()
		{
			// tag::40a2bbc35a887d6c7dda3cca1fe7aa58[]
			var response0 = new SearchResponse<object>();
			// end::40a2bbc35a887d6c7dda3cca1fe7aa58[]

			response0.MatchesExample(@"POST _scripts/calculate-score
			{
			  ""script"": {
			    ""lang"": ""painless"",
			    ""source"": ""Math.log(_score * 2) + params.my_modifier""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("scripting/using.asciidoc:163")]
		public void Line163()
		{
			// tag::548a09b4630ae38cf8af33581ae614e6[]
			var response0 = new SearchResponse<object>();
			// end::548a09b4630ae38cf8af33581ae614e6[]

			response0.MatchesExample(@"POST _scripts/calculate-score/score
			{
			  ""script"": {
			    ""lang"": ""painless"",
			    ""source"": ""Math.log(_score * 2) + params.my_modifier""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("scripting/using.asciidoc:177")]
		public void Line177()
		{
			// tag::08e08feb514b24006e13f258d617d873[]
			var response0 = new SearchResponse<object>();
			// end::08e08feb514b24006e13f258d617d873[]

			response0.MatchesExample(@"GET _scripts/calculate-score");
		}

		[U(Skip = "Example not implemented")]
		[Description("scripting/using.asciidoc:185")]
		public void Line185()
		{
			// tag::b3423b00c6336ee0a1720b4ed7031cd7[]
			var response0 = new SearchResponse<object>();
			// end::b3423b00c6336ee0a1720b4ed7031cd7[]

			response0.MatchesExample(@"GET twitter/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match"": {
			            ""message"": ""some message""
			        }
			      },
			      ""script"": {
			        ""id"": ""calculate-score"",
			        ""params"": {
			          ""my_modifier"": 2
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("scripting/using.asciidoc:210")]
		public void Line210()
		{
			// tag::4061fd5ba7221ca85805ed14d59a6bc5[]
			var response0 = new SearchResponse<object>();
			// end::4061fd5ba7221ca85805ed14d59a6bc5[]

			response0.MatchesExample(@"DELETE _scripts/calculate-score");
		}
	}
}