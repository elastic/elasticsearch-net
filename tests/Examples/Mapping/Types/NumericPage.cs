// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class NumericPage : ExampleBase
	{
		[U]
		[Description("mapping/types/numeric.asciidoc:22")]
		public void Line22()
		{
			// tag::a71c438cc4df1cafe3109ccff475afdb[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Number(n => n
							.Name("number_of_bytes")
							.Type(NumberType.Integer)
						)
						.Number(n => n
							.Name("time_in_seconds")
							.Type(NumberType.Float)
						)
						.Number(n => n
							.Name("price")
							.Type(NumberType.ScaledFloat)
							.ScalingFactor(100)
						)
					)
				)
			);
			// end::a71c438cc4df1cafe3109ccff475afdb[]

			createIndexResponse.MatchesExample(@"PUT my_index
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
			}", (e, b) =>
			{
				b["mappings"]["properties"]["price"]["scaling_factor"] = 100d;
			});
		}
	}
}
