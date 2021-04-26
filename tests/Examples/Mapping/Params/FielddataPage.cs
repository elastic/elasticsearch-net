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
	public class FielddataPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/fielddata.asciidoc:56")]
		public void Line56()
		{
			// tag::ef9111c1648d7820925f12e07d1346c5[]
			var response0 = new SearchResponse<object>();
			// end::ef9111c1648d7820925f12e07d1346c5[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_field"": { \<1>
			        ""type"": ""text"",
			        ""fields"": {
			          ""keyword"": { \<2>
			            ""type"": ""keyword""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/fielddata.asciidoc:84")]
		public void Line84()
		{
			// tag::a7c15fe6b5779c84ce9a34bf4b2a7ab7[]
			var response0 = new SearchResponse<object>();
			// end::a7c15fe6b5779c84ce9a34bf4b2a7ab7[]

			response0.MatchesExample(@"PUT my_index/_mapping
			{
			  ""properties"": {
			    ""my_field"": { \<1>
			      ""type"":     ""text"",
			      ""fielddata"": true
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/fielddata.asciidoc:117")]
		public void Line117()
		{
			// tag::6a81d00f0d73bc5985e76b3cadab645e[]
			var response0 = new SearchResponse<object>();
			// end::6a81d00f0d73bc5985e76b3cadab645e[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""tag"": {
			        ""type"": ""text"",
			        ""fielddata"": true,
			        ""fielddata_frequency_filter"": {
			          ""min"": 0.001,
			          ""max"": 0.1,
			          ""min_segment_size"": 500
			        }
			      }
			    }
			  }
			}");
		}
	}
}
