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
	public class PropertiesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/properties.asciidoc:17")]
		public void Line17()
		{
			// tag::241df3bb0c16b4bd53ee569a45539184[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::241df3bb0c16b4bd53ee569a45539184[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": { \<1>
			      ""manager"": {
			        ""properties"": { \<2>
			          ""age"":  { ""type"": ""integer"" },
			          ""name"": { ""type"": ""text""  }
			        }
			      },
			      ""employees"": {
			        ""type"": ""nested"",
			        ""properties"": { \<3>
			          ""age"":  { ""type"": ""integer"" },
			          ""name"": { ""type"": ""text""  }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1 \<4>
			{
			  ""region"": ""US"",
			  ""manager"": {
			    ""name"": ""Alice White"",
			    ""age"": 30
			  },
			  ""employees"": [
			    {
			      ""name"": ""John Smith"",
			      ""age"": 34
			    },
			    {
			      ""name"": ""Peter Brown"",
			      ""age"": 26
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/properties.asciidoc:74")]
		public void Line74()
		{
			// tag::7f21b09b9306a03491ddcf0355f33860[]
			var response0 = new SearchResponse<object>();
			// end::7f21b09b9306a03491ddcf0355f33860[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""manager.name"": ""Alice White""
			    }
			  },
			  ""aggs"": {
			    ""Employees"": {
			      ""nested"": {
			        ""path"": ""employees""
			      },
			      ""aggs"": {
			        ""Employee Ages"": {
			          ""histogram"": {
			            ""field"": ""employees.age"",
			            ""interval"": 5
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
