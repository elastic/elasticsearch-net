using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm
{
	public class ErrorHandlingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/error-handling.asciidoc:16")]
		public void Line16()
		{
			// tag::9d211c6226d0b4434f01cceb76ab6ffa[]
			var response0 = new SearchResponse<object>();
			// end::9d211c6226d0b4434f01cceb76ab6ffa[]

			response0.MatchesExample(@"PUT _ilm/policy/shrink-the-index
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""5d"",
			        ""actions"": {
			          ""shrink"": {
			            ""number_of_shards"": 4
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/error-handling.asciidoc:42")]
		public void Line42()
		{
			// tag::3d0b9acdacc7ecec380c57e814256472[]
			var response0 = new SearchResponse<object>();
			// end::3d0b9acdacc7ecec380c57e814256472[]

			response0.MatchesExample(@"PUT /myindex
			{
			  ""settings"": {
			    ""index.number_of_shards"": 2,
			    ""index.lifecycle.name"": ""shrink-the-index""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/error-handling.asciidoc:60")]
		public void Line60()
		{
			// tag::943f92e1d3fa566ef23659be2d96f222[]
			var response0 = new SearchResponse<object>();
			// end::943f92e1d3fa566ef23659be2d96f222[]

			response0.MatchesExample(@"GET /myindex/_ilm/explain");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/error-handling.asciidoc:124")]
		public void Line124()
		{
			// tag::7bee02e8962e355a23559b6eaa6678f2[]
			var response0 = new SearchResponse<object>();
			// end::7bee02e8962e355a23559b6eaa6678f2[]

			response0.MatchesExample(@"PUT _ilm/policy/shrink-the-index
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""5d"",
			        ""actions"": {
			          ""shrink"": {
			            ""number_of_shards"": 1
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/error-handling.asciidoc:151")]
		public void Line151()
		{
			// tag::235513edcb5ce3fe2e38a781eeefa6a0[]
			var response0 = new SearchResponse<object>();
			// end::235513edcb5ce3fe2e38a781eeefa6a0[]

			response0.MatchesExample(@"POST /myindex/_ilm/retry");
		}
	}
}