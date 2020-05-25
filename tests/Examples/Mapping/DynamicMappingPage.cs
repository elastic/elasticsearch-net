// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping
{
	public class DynamicMappingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/dynamic-mapping.asciidoc:11")]
		public void Line11()
		{
			// tag::61c49cee90c6aa0eafbdd5cc03936e7d[]
			var response0 = new SearchResponse<object>();
			// end::61c49cee90c6aa0eafbdd5cc03936e7d[]

			response0.MatchesExample(@"PUT data/_doc/1 \<1>
			{ ""count"": 5 }");
		}
	}
}
