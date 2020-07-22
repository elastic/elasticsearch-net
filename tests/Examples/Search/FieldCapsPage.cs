// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class FieldCapsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:9")]
		public void Line9()
		{
			// tag::38f7739f750f1411bccf511a0abaaea3[]
			var response0 = new SearchResponse<object>();
			// end::38f7739f750f1411bccf511a0abaaea3[]

			response0.MatchesExample(@"GET /_field_caps?fields=rating");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:118")]
		public void Line118()
		{
			// tag::614bd49400b6ebf47c5b12839dd1ecb8[]
			var response0 = new SearchResponse<object>();
			// end::614bd49400b6ebf47c5b12839dd1ecb8[]

			response0.MatchesExample(@"GET twitter/_field_caps?fields=rating");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:128")]
		public void Line128()
		{
			// tag::a985e6b7b2ead9c3f30a9bc97d8b598e[]
			var response0 = new SearchResponse<object>();
			// end::a985e6b7b2ead9c3f30a9bc97d8b598e[]

			response0.MatchesExample(@"GET _field_caps?fields=rating,title");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:176")]
		public void Line176()
		{
			// tag::4e931cfac74e46e221cf4a9ab88a182d[]
			var response0 = new SearchResponse<object>();
			// end::4e931cfac74e46e221cf4a9ab88a182d[]

			response0.MatchesExample(@"GET _field_caps?fields=rating,title&include_unmapped");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:230")]
		public void Line230()
		{
			// tag::4b50e414fddc2acd2e1d75c81f483b3c[]
			var response0 = new SearchResponse<object>();
			// end::4b50e414fddc2acd2e1d75c81f483b3c[]

			response0.MatchesExample(@"POST twitter*/_field_caps?fields=rating
			{
			    ""index_filter"": {
			        ""range"": {
			            ""@timestamp"": {
			                ""gte"": ""2018""
			             }
			        }
			    }
			}");
		}
	}
}
