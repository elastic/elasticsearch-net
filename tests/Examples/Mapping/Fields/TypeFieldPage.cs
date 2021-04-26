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
	public class TypeFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/type-field.asciidoc:14")]
		public void Line14()
		{
			// tag::cb639c02d28945379ba10dbfb982186f[]
			var response0 = new SearchResponse<object>();
			// end::cb639c02d28945379ba10dbfb982186f[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh=true
			{
			  ""text"": ""Document with type 'doc'""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/type-field.asciidoc:25")]
		public void Line25()
		{
			// tag::0d7b0f40446e2001c63bef29f84530eb[]
			var response0 = new SearchResponse<object>();
			// end::0d7b0f40446e2001c63bef29f84530eb[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""_type"": ""_doc""  <1>
			    }
			  },
			  ""aggs"": {
			    ""types"": {
			      ""terms"": {
			        ""field"": ""_type"", <2>
			        ""size"": 10
			      }
			    }
			  },
			  ""sort"": [
			    {
			      ""_type"": { <3>
			        ""order"": ""desc""
			      }
			    }
			  ],
			  ""script_fields"": {
			    ""type"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""doc['_type']"" <4>
			      }
			    }
			  }
			}");
		}
	}
}
