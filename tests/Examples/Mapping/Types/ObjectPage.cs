// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class ObjectPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/object.asciidoc:11")]
		public void Line11()
		{
			// tag::9bb2dc0500011e0774f4bdfebf57a7a0[]
			var response0 = new SearchResponse<object>();
			// end::9bb2dc0500011e0774f4bdfebf57a7a0[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{ \<1>
			  ""region"": ""US"",
			  ""manager"": { \<2>
			    ""age"":     30,
			    ""name"": { \<3>
			      ""first"": ""John"",
			      ""last"":  ""Smith""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/object.asciidoc:46")]
		public void Line46()
		{
			// tag::8e907d7533581efadf7831b05dd9f794[]
			var response0 = new SearchResponse<object>();
			// end::8e907d7533581efadf7831b05dd9f794[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": { \<1>
			      ""region"": {
			        ""type"": ""keyword""
			      },
			      ""manager"": { \<2>
			        ""properties"": {
			          ""age"":  { ""type"": ""integer"" },
			          ""name"": { \<3>
			            ""properties"": {
			              ""first"": { ""type"": ""text"" },
			              ""last"":  { ""type"": ""text"" }
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
