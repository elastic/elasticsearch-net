// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Fields
{
	public class IndexFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/index-field.asciidoc:11")]
		public void Line11()
		{
			// tag::3c04f75bcbb07125d51b21b9b2c9f6f0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::3c04f75bcbb07125d51b21b9b2c9f6f0[]

			response0.MatchesExample(@"PUT index_1/_doc/1
			{
			  ""text"": ""Document in index 1""
			}");

			response1.MatchesExample(@"PUT index_2/_doc/2?refresh=true
			{
			  ""text"": ""Document in index 2""
			}");

			response2.MatchesExample(@"GET index_1,index_2/_search
			{
			  ""query"": {
			    ""terms"": {
			      ""_index"": [""index_1"", ""index_2""] <1>
			    }
			  },
			  ""aggs"": {
			    ""indices"": {
			      ""terms"": {
			        ""field"": ""_index"", <2>
			        ""size"": 10
			      }
			    }
			  },
			  ""sort"": [
			    {
			      ""_index"": { <3>
			        ""order"": ""asc""
			      }
			    }
			  ],
			  ""script_fields"": {
			    ""index_name"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""doc['_index']"" <4>
			      }
			    }
			  }
			}");
		}
	}
}
