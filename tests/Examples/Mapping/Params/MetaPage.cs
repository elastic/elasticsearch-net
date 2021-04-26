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

namespace Examples.Mapping.Params
{
	public class MetaPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/meta.asciidoc:9")]
		public void Line9()
		{
			// tag::bb49cbbeef6afe2dae0db46c4a10df3b[]
			var response0 = new SearchResponse<object>();
			// end::bb49cbbeef6afe2dae0db46c4a10df3b[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""latency"": {
			        ""type"": ""long"",
			        ""meta"": {
			          ""unit"": ""ms""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
