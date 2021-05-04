// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Scripting
{
	public class FieldsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("scripting/fields.asciidoc:48")]
		public void Line48()
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

		[U(Skip = "Example not implemented")]
		[Description("scripting/fields.asciidoc:92")]
		public void Line92()
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

		[U(Skip = "Example not implemented")]
		[Description("scripting/fields.asciidoc:169")]
		public void Line169()
		{
			// tag::9790a85b52fa851c8abe20d00ba03bc1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::9790a85b52fa851c8abe20d00ba03bc1[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""first_name"": {
			        ""type"": ""text""
			      },
			      ""last_name"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""first_name"": ""Barry"",
			  ""last_name"": ""White""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"": {
			    ""full_name"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""params._source.first_name + ' ' + params._source.last_name""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("scripting/fields.asciidoc:212")]
		public void Line212()
		{
			// tag::2548b8591b3e0d7ac95cafebac63a2a9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::2548b8591b3e0d7ac95cafebac63a2a9[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""full_name"": {
			        ""type"": ""text"",
			        ""store"": true
			      },
			      ""title"": {
			        ""type"": ""text"",
			        ""store"": true
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""full_name"": ""Alice Ball"",
			  ""title"": ""Professor""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"": {
			    ""name_with_title"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""params._fields['title'].value + ' ' + params._fields['full_name'].value""
			      }
			    }
			  }
			}");
		}
	}
}
