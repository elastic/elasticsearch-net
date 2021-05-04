// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Fields
{
	public class SourceFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/source-field.asciidoc:16")]
		public void Line16()
		{
			// tag::50246e04b49dab320409b95526e6e34c[]
			var response0 = new SearchResponse<object>();
			// end::50246e04b49dab320409b95526e6e34c[]

			response0.MatchesExample(@"PUT tweets
			{
			  ""mappings"": {
			    ""_source"": {
			      ""enabled"": false
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/source-field.asciidoc:85")]
		public void Line85()
		{
			// tag::b557f114e21dbc6f531d4e7621a08e8f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::b557f114e21dbc6f531d4e7621a08e8f[]

			response0.MatchesExample(@"PUT logs
			{
			  ""mappings"": {
			    ""_source"": {
			      ""includes"": [
			        ""*.count"",
			        ""meta.*""
			      ],
			      ""excludes"": [
			        ""meta.description"",
			        ""meta.other.*""
			      ]
			    }
			  }
			}");

			response1.MatchesExample(@"PUT logs/_doc/1
			{
			  ""requests"": {
			    ""count"": 10,
			    ""foo"": ""bar"" \<1>
			  },
			  ""meta"": {
			    ""name"": ""Some metric"",
			    ""description"": ""Some metric description"", \<1>
			    ""other"": {
			      ""foo"": ""one"", \<1>
			      ""baz"": ""two"" \<1>
			    }
			  }
			}");

			response2.MatchesExample(@"GET logs/_search
			{
			  ""query"": {
			    ""match"": {
			      ""meta.other.foo"": ""one"" \<2>
			    }
			  }
			}");
		}
	}
}
