using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class AnalyzePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::06b4f0789d42d85d9af0780388feca83[]
			var response0 = new SearchResponse<object>();
			// end::06b4f0789d42d85d9af0780388feca83[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""analyzer"" : ""standard"",
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line23()
		{
			// tag::a158d87b517d0f7a3d14d8f4eb1c4036[]
			var response0 = new SearchResponse<object>();
			// end::a158d87b517d0f7a3d14d8f4eb1c4036[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""analyzer"" : ""standard"",
			  ""text"" : [""this is a test"", ""the second text""]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line37()
		{
			// tag::373dd9448e2cba5dd8dbf3a1aded9025[]
			var response0 = new SearchResponse<object>();
			// end::373dd9448e2cba5dd8dbf3a1aded9025[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""filter"" : [""lowercase""],
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line48()
		{
			// tag::61354fd8aad2ad07230c39339a2cd318[]
			var response0 = new SearchResponse<object>();
			// end::61354fd8aad2ad07230c39339a2cd318[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""filter"" : [""lowercase""],
			  ""char_filter"" : [""html_strip""],
			  ""text"" : ""this is a \<b>test</b>""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line64()
		{
			// tag::37b2f5dca993d15ed1711f3532a0f98d[]
			var response0 = new SearchResponse<object>();
			// end::37b2f5dca993d15ed1711f3532a0f98d[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""lowercase"", {""type"": ""stop"", ""stopwords"": [""a"", ""is"", ""this""]}],
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::9f67f5e5cd0355cebea6fb28d28a8b5f[]
			var response0 = new SearchResponse<object>();
			// end::9f67f5e5cd0355cebea6fb28d28a8b5f[]

			response0.MatchesExample(@"GET analyze_sample/_analyze
			{
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line91()
		{
			// tag::5c448f76daf65093e1893705e705ab3b[]
			var response0 = new SearchResponse<object>();
			// end::5c448f76daf65093e1893705e705ab3b[]

			response0.MatchesExample(@"GET analyze_sample/_analyze
			{
			  ""analyzer"" : ""whitespace"",
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line104()
		{
			// tag::ebc29a29598d16f3156cbbd0d1b0954a[]
			var response0 = new SearchResponse<object>();
			// end::ebc29a29598d16f3156cbbd0d1b0954a[]

			response0.MatchesExample(@"GET analyze_sample/_analyze
			{
			  ""field"" : ""obj1.field1"",
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line120()
		{
			// tag::441ba0438c9ab8e7ce3579277d98b9a7[]
			var response0 = new SearchResponse<object>();
			// end::441ba0438c9ab8e7ce3579277d98b9a7[]

			response0.MatchesExample(@"GET analyze_sample/_analyze
			{
			  ""normalizer"" : ""my_normalizer"",
			  ""text"" : ""BaR""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line133()
		{
			// tag::da69ca2ea849335e3a3ab9e15aa0e5f9[]
			var response0 = new SearchResponse<object>();
			// end::da69ca2ea849335e3a3ab9e15aa0e5f9[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""filter"" : [""lowercase""],
			  ""text"" : ""BaR""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line151()
		{
			// tag::98a28410e6c58919d7a700631138e775[]
			var response0 = new SearchResponse<object>();
			// end::98a28410e6c58919d7a700631138e775[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""snowball""],
			  ""text"" : ""detailed output"",
			  ""explain"" : true,
			  ""attributes"" : [""keyword""] \<1>
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line227()
		{
			// tag::cdc5155ae26dbaa25110a6a6a7e995fe[]
			var response0 = new SearchResponse<object>();
			// end::cdc5155ae26dbaa25110a6a6a7e995fe[]

			response0.MatchesExample(@"PUT analyze_sample
			{
			  ""settings"" : {
			    ""index.analyze.max_token_count"" : 20000
			  }
			}");
		}
	}
}