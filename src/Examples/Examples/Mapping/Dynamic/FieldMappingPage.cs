using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Dynamic
{
	public class FieldMappingPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line50()
		{
			// tag::4909bf2f9e86b4bdd6af1d0b13c0015d[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4909bf2f9e86b4bdd6af1d0b13c0015d[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""create_date"": ""2015/09/02""
			}");

			response1.MatchesExample(@"GET my_index/_mapping \<1>");
		}

		[U]
		[SkipExample]
		public void Line68()
		{
			// tag::95fa846e5d0a75210f9ad1fa1acfa8f3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::95fa846e5d0a75210f9ad1fa1acfa8f3[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""date_detection"": false
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1 \<1>
			{
			  ""create"": ""2015/09/02""
			}");
		}

		[U]
		[SkipExample]
		public void Line91()
		{
			// tag::4eae628c9aaa259f80711c6e9cc6fd25[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4eae628c9aaa259f80711c6e9cc6fd25[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_date_formats"": [""MM/dd/yyyy""]
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""create_date"": ""09/25/2015""
			}");
		}

		[U]
		[SkipExample]
		public void Line117()
		{
			// tag::fa3cd4ffaec8273656a328ae29f32c65[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fa3cd4ffaec8273656a328ae29f32c65[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""numeric_detection"": true
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_float"":   ""1.0"", \<1>
			  ""my_integer"": ""1"" \<2>
			}");
		}
	}
}