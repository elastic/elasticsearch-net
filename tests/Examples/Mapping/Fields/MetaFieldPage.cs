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

namespace Examples.Mapping.Fields
{
	public class MetaFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/meta-field.asciidoc:9")]
		public void Line9()
		{
			// tag::e10d7f411744eb1d5ddaa2f70a368490[]
			var response0 = new SearchResponse<object>();
			// end::e10d7f411744eb1d5ddaa2f70a368490[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""_meta"": { \<1>
			      ""class"": ""MyApp::User"",
			      ""version"": {
			        ""min"": ""1.0"",
			        ""max"": ""1.3""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/meta-field.asciidoc:31")]
		public void Line31()
		{
			// tag::019eab381444c3d77ad3bb4e39edfac6[]
			var response0 = new SearchResponse<object>();
			// end::019eab381444c3d77ad3bb4e39edfac6[]

			response0.MatchesExample(@"PUT my_index/_mapping
			{
			  ""_meta"": {
			    ""class"": ""MyApp2::User3"",
			    ""version"": {
			      ""min"": ""1.3"",
			      ""max"": ""1.5""
			    }
			  }
			}");
		}
	}
}
