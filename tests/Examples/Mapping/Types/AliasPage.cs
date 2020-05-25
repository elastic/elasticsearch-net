// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class AliasPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/alias.asciidoc:12")]
		public void Line12()
		{
			// tag::2716453454dbf9c6dde2ea6850a62214[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2716453454dbf9c6dde2ea6850a62214[]

			response0.MatchesExample(@"PUT trips
			{
			  ""mappings"": {
			    ""properties"": {
			      ""distance"": {
			        ""type"": ""long""
			      },
			      ""route_length_miles"": {
			        ""type"": ""alias"",
			        ""path"": ""distance"" \<1>
			      },
			      ""transit_mode"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""range"" : {
			      ""route_length_miles"" : {
			        ""gte"" : 39
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/alias.asciidoc:55")]
		public void Line55()
		{
			// tag::a2dabdcbb661e7690166ae6d0de27e46[]
			var response0 = new SearchResponse<object>();
			// end::a2dabdcbb661e7690166ae6d0de27e46[]

			response0.MatchesExample(@"GET trips/_field_caps?fields=route_*,transit_mode");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/alias.asciidoc:86")]
		public void Line86()
		{
			// tag::f6c9d72fa26cbedd0c3f9fa64a88c38a[]
			var response0 = new SearchResponse<object>();
			// end::f6c9d72fa26cbedd0c3f9fa64a88c38a[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"" : {
			    ""match_all"": {}
			  },
			  ""_source"": ""route_length_miles""
			}");
		}
	}
}
