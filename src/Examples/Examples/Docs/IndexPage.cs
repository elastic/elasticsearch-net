using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class IndexPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::bb143628fd04070683eeeadc9406d9cc[]
			var response0 = new SearchResponse<object>();
			// end::bb143628fd04070683eeeadc9406d9cc[]

			response0.MatchesExample(@"PUT twitter/_doc/1
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[SkipExample]
		public void Line77()
		{
			// tag::804a97ff4d0613e6568e4efb19c52021[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::804a97ff4d0613e6568e4efb19c52021[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			    ""persistent"": {
			        ""action.auto_create_index"": ""twitter,index10,-index1*,+ind*"" \<1>
			    }
			}");

			response1.MatchesExample(@"PUT _cluster/settings
			{
			    ""persistent"": {
			        ""action.auto_create_index"": ""false"" \<2>
			    }
			}");

			response2.MatchesExample(@"PUT _cluster/settings
			{
			    ""persistent"": {
			        ""action.auto_create_index"": ""true"" \<3>
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line121()
		{
			// tag::d718b63cf1b6591a1d59a0cf4fd995eb[]
			var response0 = new SearchResponse<object>();
			// end::d718b63cf1b6591a1d59a0cf4fd995eb[]

			response0.MatchesExample(@"PUT twitter/_doc/1?op_type=create
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[SkipExample]
		public void Line134()
		{
			// tag::048d8abd42d094bbdcf4452a58ccb35b[]
			var response0 = new SearchResponse<object>();
			// end::048d8abd42d094bbdcf4452a58ccb35b[]

			response0.MatchesExample(@"PUT twitter/_create/1
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[SkipExample]
		public void Line153()
		{
			// tag::36818c6d9f434d387819c30bd9addb14[]
			var response0 = new SearchResponse<object>();
			// end::36818c6d9f434d387819c30bd9addb14[]

			response0.MatchesExample(@"POST twitter/_doc/
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[SkipExample]
		public void Line204()
		{
			// tag::625dc94df1f9affb49a082fd99d41620[]
			var response0 = new SearchResponse<object>();
			// end::625dc94df1f9affb49a082fd99d41620[]

			response0.MatchesExample(@"POST twitter/_doc?routing=kimchy
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[SkipExample]
		public void Line327()
		{
			// tag::b918d6b798da673a33e49b94f61dcdc0[]
			var response0 = new SearchResponse<object>();
			// end::b918d6b798da673a33e49b94f61dcdc0[]

			response0.MatchesExample(@"PUT twitter/_doc/1?timeout=5m
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[SkipExample]
		public void Line357()
		{
			// tag::1f336ecc62480c1d56351cc2f82d0d08[]
			var response0 = new SearchResponse<object>();
			// end::1f336ecc62480c1d56351cc2f82d0d08[]

			response0.MatchesExample(@"PUT twitter/_doc/1?version=2&version_type=external
			{
			    ""message"" : ""elasticsearch now has versioning support, double cool!""
			}");
		}
	}
}