using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class QueryStringQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::81f09836772b03f6e7e8e7986409d67e[]
			var response0 = new SearchResponse<object>();
			// end::81f09836772b03f6e7e8e7986409d67e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""default_field"" : ""content"",
			            ""query"" : ""this AND that OR thus""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line28()
		{
			// tag::fe1dd67108b04e07d0832b539d4e0d99[]
			var response0 = new SearchResponse<object>();
			// end::fe1dd67108b04e07d0832b539d4e0d99[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""default_field"" : ""content"",
			            ""query"" : ""(new york city) OR (big apple)"" \<1>
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line168()
		{
			// tag::f2d68493abd3ca430bd03a7f7f8d18f9[]
			var response0 = new SearchResponse<object>();
			// end::f2d68493abd3ca430bd03a7f7f8d18f9[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""content"", ""name""],
			            ""query"" : ""this AND that""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line185()
		{
			// tag::e17e8852ec3f31781e1364f4dffeb6d0[]
			var response0 = new SearchResponse<object>();
			// end::e17e8852ec3f31781e1364f4dffeb6d0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""(content:this OR name:this) AND (content:that OR name:that)""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line202()
		{
			// tag::a2a25aad1fea9a541b52ac613c78fb64[]
			var response0 = new SearchResponse<object>();
			// end::a2a25aad1fea9a541b52ac613c78fb64[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""content"", ""name^5""],
			            ""query"" : ""this AND that OR thus"",
			            ""tie_breaker"" : 0
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line222()
		{
			// tag::28aad2c5942bfb221c2bf1bbdc01658e[]
			var response0 = new SearchResponse<object>();
			// end::28aad2c5942bfb221c2bf1bbdc01658e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""city.*""],
			            ""query"" : ""this AND that OR thus""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line240()
		{
			// tag::db6cba451ba562abe953d09ad80cc15c[]
			var response0 = new SearchResponse<object>();
			// end::db6cba451ba562abe953d09ad80cc15c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""query"" : ""city.\\*:(this AND that OR thus)""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line275()
		{
			// tag::58b5003c0a53a39bf509aa3797aad471[]
			var response0 = new SearchResponse<object>();
			// end::58b5003c0a53a39bf509aa3797aad471[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"" : {
			            ""fields"" : [""content"", ""name.*^5""],
			            ""query"" : ""this AND that OR thus""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line300()
		{
			// tag::f32f0c19b42de3b87dd764fe4ca17e7c[]
			var response0 = new SearchResponse<object>();
			// end::f32f0c19b42de3b87dd764fe4ca17e7c[]

			response0.MatchesExample(@"GET /_search
			{
			   ""query"": {
			       ""query_string"" : {
			           ""default_field"": ""title"",
			           ""query"" : ""ny city"",
			           ""auto_generate_synonyms_phrase_query"" : false
			       }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line329()
		{
			// tag::60ee33f3acfdd0fe6f288ac77312c780[]
			var response0 = new SearchResponse<object>();
			// end::60ee33f3acfdd0fe6f288ac77312c780[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title""
			            ],
			            ""query"": ""this that thus"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line356()
		{
			// tag::be1bd47393646ac6bbee177d1cdb7738[]
			var response0 = new SearchResponse<object>();
			// end::be1bd47393646ac6bbee177d1cdb7738[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title"",
			                ""content""
			            ],
			            ""query"": ""this that thus"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line381()
		{
			// tag::fdd38f0d248385a444c777e7acd97846[]
			var response0 = new SearchResponse<object>();
			// end::fdd38f0d248385a444c777e7acd97846[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title"",
			                ""content""
			            ],
			            ""query"": ""this OR that OR thus"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line411()
		{
			// tag::6f21a878fee3b43c5332b81aaddbeac7[]
			var response0 = new SearchResponse<object>();
			// end::6f21a878fee3b43c5332b81aaddbeac7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""fields"": [
			                ""title"",
			                ""content""
			            ],
			            ""query"": ""this OR that OR thus"",
			            ""type"": ""cross_fields"",
			            ""minimum_should_match"": 2
			        }
			    }
			}");
		}
	}
}