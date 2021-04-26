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
