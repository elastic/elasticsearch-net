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
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class TextPage : ExampleBase
	{
		[U]
		[Description("mapping/types/text.asciidoc:22")]
		public void Line22()
		{
			// tag::24ea1c6cdf10165228951e562b7ec0ef[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p.Text(t => t.Name("full_name")))
				)
			);
			// end::24ea1c6cdf10165228951e562b7ec0ef[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""full_name"": {
			        ""type"":  ""text""
			      }
			    }
			  }
			}");
		}
	}
}
