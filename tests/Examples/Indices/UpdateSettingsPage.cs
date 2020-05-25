// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class UpdateSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/update-settings.asciidoc:10")]
		public void Line10()
		{
			// tag::8653e76676de5d327201b77512afa3a0[]
			var response0 = new SearchResponse<object>();
			// end::8653e76676de5d327201b77512afa3a0[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""number_of_replicas"" : 2
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/update-settings.asciidoc:73")]
		public void Line73()
		{
			// tag::42744a175125df5be0ef77413bf8f608[]
			var response0 = new SearchResponse<object>();
			// end::42744a175125df5be0ef77413bf8f608[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""refresh_interval"" : null
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/update-settings.asciidoc:97")]
		public void Line97()
		{
			// tag::dfac8d098b50aa0181161bcd17b38ef4[]
			var response0 = new SearchResponse<object>();
			// end::dfac8d098b50aa0181161bcd17b38ef4[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""refresh_interval"" : ""-1""
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/update-settings.asciidoc:114")]
		public void Line114()
		{
			// tag::0be2c28ee65384774b1e479b47dc3d92[]
			var response0 = new SearchResponse<object>();
			// end::0be2c28ee65384774b1e479b47dc3d92[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index"" : {
			        ""refresh_interval"" : ""1s""
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/update-settings.asciidoc:127")]
		public void Line127()
		{
			// tag::fe5763d32955e8b65eb3048e97b1580c[]
			var response0 = new SearchResponse<object>();
			// end::fe5763d32955e8b65eb3048e97b1580c[]

			response0.MatchesExample(@"POST /twitter/_forcemerge?max_num_segments=5");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/update-settings.asciidoc:145")]
		public void Line145()
		{
			// tag::ba0b4081c98f3387f76b77847c52ee9a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::ba0b4081c98f3387f76b77847c52ee9a[]

			response0.MatchesExample(@"POST /twitter/_close");

			response1.MatchesExample(@"PUT /twitter/_settings
			{
			  ""analysis"" : {
			    ""analyzer"":{
			      ""content"":{
			        ""type"":""custom"",
			        ""tokenizer"":""whitespace""
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"POST /twitter/_open");
		}
	}
}
