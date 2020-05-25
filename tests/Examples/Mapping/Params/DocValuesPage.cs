// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class DocValuesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/doc-values.asciidoc:25")]
		public void Line25()
		{
			// tag::4e75503583efc222045e0be4430a2863[]
			var response0 = new SearchResponse<object>();
			// end::4e75503583efc222045e0be4430a2863[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""status_code"": { \<1>
			        ""type"":       ""keyword""
			      },
			      ""session_id"": { \<2>
			        ""type"":       ""keyword"",
			        ""doc_values"": false
			      }
			    }
			  }
			}");
		}
	}
}
