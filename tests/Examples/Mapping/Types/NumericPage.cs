// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class NumericPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/numeric.asciidoc:22")]
		public void Line22()
		{
			// tag::a71c438cc4df1cafe3109ccff475afdb[]
			var response0 = new SearchResponse<object>();
			// end::a71c438cc4df1cafe3109ccff475afdb[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""number_of_bytes"": {
			        ""type"": ""integer""
			      },
			      ""time_in_seconds"": {
			        ""type"": ""float""
			      },
			      ""price"": {
			        ""type"": ""scaled_float"",
			        ""scaling_factor"": 100
			      }
			    }
			  }
			}");
		}
	}
}