using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SimpleQueryStringQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::0d49474511b236bc89e768c8ee91adf1[]
			var response0 = new SearchResponse<object>();
			// end::0d49474511b236bc89e768c8ee91adf1[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""simple_query_string"" : {
			        ""query"": ""\""fried eggs\"" +(eggplant | potato) -frittata"",
			        ""fields"": [""title^5"", ""body""],
			        ""default_operator"": ""and""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line157()
		{
			// tag::521aa59ae56681fd59ac5840cba6b6c5[]
			var response0 = new SearchResponse<object>();
			// end::521aa59ae56681fd59ac5840cba6b6c5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""simple_query_string"" : {
			            ""fields"" : [""content""],
			            ""query"" : ""foo bar -baz""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line186()
		{
			// tag::f686f52decb1d57356d42920f46d4d85[]
			var response0 = new SearchResponse<object>();
			// end::f686f52decb1d57356d42920f46d4d85[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""simple_query_string"" : {
			            ""query"" : ""foo | bar + baz*"",
			            ""flags"" : ""OR|AND|PREFIX""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line251()
		{
			// tag::e8ea65153d7775f25b08dfdfe6954498[]
			var response0 = new SearchResponse<object>();
			// end::e8ea65153d7775f25b08dfdfe6954498[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""simple_query_string"" : {
			      ""query"":    ""Will Smith"",
			      ""fields"": [ ""title"", ""*_name"" ] \<1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line268()
		{
			// tag::da8db0769dff7305f178c12b1111bc99[]
			var response0 = new SearchResponse<object>();
			// end::da8db0769dff7305f178c12b1111bc99[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""simple_query_string"" : {
			      ""query"" : ""this is a test"",
			      ""fields"" : [ ""subject^3"", ""message"" ] \<1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line295()
		{
			// tag::2e602d7fbad46132358f921dff7d1a26[]
			var response0 = new SearchResponse<object>();
			// end::2e602d7fbad46132358f921dff7d1a26[]

			response0.MatchesExample(@"GET /_search
			{
			   ""query"": {
			       ""simple_query_string"" : {
			           ""query"" : ""ny city"",
			           ""auto_generate_synonyms_phrase_query"" : false
			       }
			   }
			}");
		}
	}
}