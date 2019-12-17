using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Fields
{
	public class RoutingFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line15()
		{
			// tag::b684073ea8d34359c290c663d2a5e798[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b684073ea8d34359c290c663d2a5e798[]

			response0.MatchesExample(@"PUT my_index/_doc/1?routing=user1&refresh=true \<1>
			{
			  ""title"": ""This is a document""
			}");

			response1.MatchesExample(@"GET my_index/_doc/1?routing=user1 \<2>");
		}

		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::6817609dd2fcb73b9920327c5cf5ec77[]
			var response0 = new SearchResponse<object>();
			// end::6817609dd2fcb73b9920327c5cf5ec77[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""terms"": {
			      ""_routing"": [ ""user1"" ] \<1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line53()
		{
			// tag::134bdbfb50c81dd3c487514faabc81d3[]
			var response0 = new SearchResponse<object>();
			// end::134bdbfb50c81dd3c487514faabc81d3[]

			response0.MatchesExample(@"GET my_index/_search?routing=user1,user2 \<1>
			{
			  ""query"": {
			    ""match"": {
			      ""title"": ""document""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line78()
		{
			// tag::4f3089b403945e391f03280ae2f360a4[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4f3089b403945e391f03280ae2f360a4[]

			response0.MatchesExample(@"PUT my_index2
			{
			  ""mappings"": {
			    ""_routing"": {
			      ""required"": true \<1>
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index2/_doc/1 \<2>
			{
			  ""text"": ""No routing value provided""
			}");
		}
	}
}