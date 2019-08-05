using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Scripting
{
	public class FieldsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line46()
		{
			// tag::729f4abc0b4edaf6b58bd9e7b3fd5a8b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::729f4abc0b4edaf6b58bd9e7b3fd5a8b[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""text"": ""quick brown fox"",
			  ""popularity"": 1
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""text"": ""quick fox"",
			  ""popularity"": 5
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""function_score"": {
			      ""query"": {
			        ""match"": {
			          ""text"": ""quick brown fox""
			        }
			      },
			      ""script_score"": {
			        ""script"": {
			          ""lang"": ""expression"",
			          ""source"": ""_score * doc['popularity']""
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line91()
		{
			// tag::0dfe9d6724c7bd11094bb4a0796e7ac7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::0dfe9d6724c7bd11094bb4a0796e7ac7[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""cost_price"": 100
			}");

			response1.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"": {
			    ""sales_price"": {
			      ""script"": {
			        ""lang"":   ""expression"",
			        ""source"": ""doc['cost_price'] * markup"",
			        ""params"": {
			          ""markup"": 0.2
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line174()
		{
			// tag::2a9c29afe23e30a68dd6e30ea22f5d42[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::2a9c29afe23e30a68dd6e30ea22f5d42[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""title"": { \<1>
			        ""type"": ""text""
			      },
			      ""first_name"": {
			        ""type"": ""text"",
			        ""store"": true
			      },
			      ""last_name"": {
			        ""type"": ""text"",
			        ""store"": true
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""title"": ""Mr"",
			  ""first_name"": ""Barry"",
			  ""last_name"": ""White""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"": {
			    ""source"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""params._source.title + ' ' + params._source.first_name + ' ' + params._source.last_name"" \<2>
			      }
			    },
			    ""stored_fields"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""params._fields['first_name'].value + ' ' + params._fields['last_name'].value""
			      }
			    }
			  }
			}");
		}
	}
}