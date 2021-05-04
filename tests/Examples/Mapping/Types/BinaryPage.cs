// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class BinaryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/binary.asciidoc:12")]
		public void Line12()
		{
			// tag::9296dd085f411739f5b0ec80eb9b9e27[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9296dd085f411739f5b0ec80eb9b9e27[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name"": {
			        ""type"": ""text""
			      },
			      ""blob"": {
			        ""type"": ""binary""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""name"": ""Some binary blob"",
			  ""blob"": ""U29tZSBiaW5hcnkgYmxvYg=="" \<1>
			}");
		}
	}
}
