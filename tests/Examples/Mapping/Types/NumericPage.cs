/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
