using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class SuggestersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/suggesters.asciidoc:8")]
		public void Line8()
		{
			// tag::626f8c4b3e2cd3d9beaa63a7f5799d7a[]
			var response0 = new SearchResponse<object>();
			// end::626f8c4b3e2cd3d9beaa63a7f5799d7a[]

			response0.MatchesExample(@"POST twitter/_search
			{
			  ""query"" : {
			    ""match"": {
			      ""message"": ""tring out Elasticsearch""
			    }
			  },
			  ""suggest"" : {
			    ""my-suggestion"" : {
			      ""text"" : ""tring out Elasticsearch"",
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters.asciidoc:51")]
		public void Line51()
		{
			// tag::2533e4b36ae837eaecda08407ecb6383[]
			var response0 = new SearchResponse<object>();
			// end::2533e4b36ae837eaecda08407ecb6383[]

			response0.MatchesExample(@"POST _search
			{
			  ""suggest"": {
			    ""my-suggest-1"" : {
			      ""text"" : ""tring out Elasticsearch"",
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    },
			    ""my-suggest-2"" : {
			      ""text"" : ""kmichy"",
			      ""term"" : {
			        ""field"" : ""user""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters.asciidoc:127")]
		public void Line127()
		{
			// tag::5275842787967b6db876025f4a1c6942[]
			var response0 = new SearchResponse<object>();
			// end::5275842787967b6db876025f4a1c6942[]

			response0.MatchesExample(@"POST _search
			{
			  ""suggest"": {
			    ""text"" : ""tring out Elasticsearch"",
			    ""my-suggest-1"" : {
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    },
			    ""my-suggest-2"" : {
			       ""term"" : {
			        ""field"" : ""user""
			       }
			    }
			  }
			}");
		}
	}
}