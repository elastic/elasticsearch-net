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

namespace Examples.Vectors
{
	public class VectorFunctionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("vectors/vector-functions.asciidoc:15")]
		public void Line15()
		{
			// tag::f4bdad6ecd4a53cabee95883731e1bc7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::f4bdad6ecd4a53cabee95883731e1bc7[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_dense_vector"": {
			        ""type"": ""dense_vector"",
			        ""dims"": 3
			      },
			      ""status"" : {
			        ""type"" : ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_dense_vector"": [0.5, 10, 6],
			  ""status"" : ""published""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""my_dense_vector"": [-0.5, 10, 10],
			  ""status"" : ""published""
			}");

			response3.MatchesExample(@"POST my_index/_refresh");
		}

		[U(Skip = "Example not implemented")]
		[Description("vectors/vector-functions.asciidoc:52")]
		public void Line52()
		{
			// tag::fb7eaa05e4b418cb3da04e56d3eefa71[]
			var response0 = new SearchResponse<object>();
			// end::fb7eaa05e4b418cb3da04e56d3eefa71[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published"" <1>
			            }
			          }
			        }
			      },
			      ""script"": {
			        ""source"": ""cosineSimilarity(params.query_vector, 'my_dense_vector') + 1.0"", <2>
			        ""params"": {
			          ""query_vector"": [4, 3.4, -0.2]  <3>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("vectors/vector-functions.asciidoc:88")]
		public void Line88()
		{
			// tag::5f3793dbe5223db53fc67861388ecb10[]
			var response0 = new SearchResponse<object>();
			// end::5f3793dbe5223db53fc67861388ecb10[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
			      },
			      ""script"": {
			        ""source"": """"""
			          double value = dotProduct(params.query_vector, 'my_dense_vector');
			          return sigmoid(1, Math.E, -value); <1>
			        """""",
			        ""params"": {
			          ""query_vector"": [4, 3.4, -0.2]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("vectors/vector-functions.asciidoc:123")]
		public void Line123()
		{
			// tag::7453c76da9d525b8c5fb5b86f1207667[]
			var response0 = new SearchResponse<object>();
			// end::7453c76da9d525b8c5fb5b86f1207667[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
			      },
			      ""script"": {
			        ""source"": ""1 / (1 + l1norm(params.queryVector, 'my_dense_vector'))"", <1>
			        ""params"": {
			          ""queryVector"": [4, 3.4, -0.2]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("vectors/vector-functions.asciidoc:162")]
		public void Line162()
		{
			// tag::98e4bd19706e57405b6e810de72ea4df[]
			var response0 = new SearchResponse<object>();
			// end::98e4bd19706e57405b6e810de72ea4df[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
			      },
			      ""script"": {
			        ""source"": ""1 / (1 + l2norm(params.queryVector, 'my_dense_vector'))"",
			        ""params"": {
			          ""queryVector"": [4, 3.4, -0.2]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
