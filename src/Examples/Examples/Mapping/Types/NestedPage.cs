using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class NestedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line19()
		{
			// tag::8baccd8688a6bad1749b8935f9601ea4[]
			var response0 = new SearchResponse<object>();
			// end::8baccd8688a6bad1749b8935f9601ea4[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""group"" : ""fans"",
			  ""user"" : [ \<1>
			    {
			      ""first"" : ""John"",
			      ""last"" :  ""Smith""
			    },
			    {
			      ""first"" : ""Alice"",
			      ""last"" :  ""White""
			    }
			  ]
			}");
		}

		[U]
		[SkipExample]
		public void Line55()
		{
			// tag::b214942b938e47f2c486e523546cb574[]
			var response0 = new SearchResponse<object>();
			// end::b214942b938e47f2c486e523546cb574[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": [
			        { ""match"": { ""user.first"": ""Alice"" }},
			        { ""match"": { ""user.last"":  ""Smith"" }}
			      ]
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line81()
		{
			// tag::b919f88e6f47a40d5793479440a90ba6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::b919f88e6f47a40d5793479440a90ba6[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""user"": {
			        ""type"": ""nested"" \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""group"" : ""fans"",
			  ""user"" : [
			    {
			      ""first"" : ""John"",
			      ""last"" :  ""Smith""
			    },
			    {
			      ""first"" : ""Alice"",
			      ""last"" :  ""White""
			    }
			  ]
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""nested"": {
			      ""path"": ""user"",
			      ""query"": {
			        ""bool"": {
			          ""must"": [
			            { ""match"": { ""user.first"": ""Alice"" }},
			            { ""match"": { ""user.last"":  ""Smith"" }} \<2>
			          ]
			        }
			      }
			    }
			  }
			}");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""nested"": {
			      ""path"": ""user"",
			      ""query"": {
			        ""bool"": {
			          ""must"": [
			            { ""match"": { ""user.first"": ""Alice"" }},
			            { ""match"": { ""user.last"":  ""White"" }} \<3>
			          ]
			        }
			      },
			      ""inner_hits"": { \<4>
			        ""highlight"": {
			          ""fields"": {
			            ""user.first"": {}
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}