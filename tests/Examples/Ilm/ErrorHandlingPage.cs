// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
			// tag::1b37e2237c9e3aaf84d56cc5c0bdb9ec[]
			var response0 = new SearchResponse<object>();
			// end::1b37e2237c9e3aaf84d56cc5c0bdb9ec[]

			response0.MatchesExample(@"PUT _ilm/policy/shrink-index
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
		[Description("ilm/error-handling.asciidoc:39")]
		public void Line39()
		{
			// tag::9b196e961ef4b8c7e07b4e3c8f94c647[]
			var response0 = new SearchResponse<object>();
			// end::9b196e961ef4b8c7e07b4e3c8f94c647[]

			response0.MatchesExample(@"PUT /myindex
			{
			  ""settings"": {
			    ""index.number_of_shards"": 2,
			    ""index.lifecycle.name"": ""shrink-index""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/error-handling.asciidoc:58")]
		public void Line58()
		{
			// tag::943f92e1d3fa566ef23659be2d96f222[]
			var response0 = new SearchResponse<object>();
			// end::943f92e1d3fa566ef23659be2d96f222[]

			response0.MatchesExample(@"GET /myindex/_ilm/explain");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/error-handling.asciidoc:117")]
		public void Line117()
		{
			// tag::bd5bd5d8b3d81241335fe1e5747080ac[]
			var response0 = new SearchResponse<object>();
			// end::bd5bd5d8b3d81241335fe1e5747080ac[]

			response0.MatchesExample(@"PUT _ilm/policy/shrink-index
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
		[Description("ilm/error-handling.asciidoc:143")]
		public void Line143()
		{
			// tag::235513edcb5ce3fe2e38a781eeefa6a0[]
			var response0 = new SearchResponse<object>();
			// end::235513edcb5ce3fe2e38a781eeefa6a0[]

			response0.MatchesExample(@"POST /myindex/_ilm/retry");
		}
	}
}
