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

namespace Examples.Transform
{
	public class EcommerceTutorialPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/ecommerce-tutorial.asciidoc:78")]
		public void Line78()
		{
			// tag::8345d2615f43a934fe1871a5120eca1d[]
			var response0 = new SearchResponse<object>();
			// end::8345d2615f43a934fe1871a5120eca1d[]

			response0.MatchesExample(@"POST _transform/_preview
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_ecommerce"",
			    ""query"": {
			      ""bool"": {
			        ""filter"": {
			          ""term"": {""currency"": ""EUR""}
			        }
			      }
			    }
			  },
			  ""pivot"": {
			    ""group_by"": {
			      ""customer_id"": {
			        ""terms"": {
			          ""field"": ""customer_id""
			        }
			      }
			    },
			    ""aggregations"": {
			      ""total_quantity.sum"": {
			        ""sum"": {
			          ""field"": ""total_quantity""
			        }
			      },
			      ""taxless_total_price.sum"": {
			        ""sum"": {
			          ""field"": ""taxless_total_price""
			        }
			      },
			      ""total_quantity.max"": {
			        ""max"": {
			          ""field"": ""total_quantity""
			        }
			      },
			      ""order_id.cardinality"": {
			        ""cardinality"": {
			          ""field"": ""order_id""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/ecommerce-tutorial.asciidoc:154")]
		public void Line154()
		{
			// tag::c68404749f090ab191c0fd5f651635cf[]
			var response0 = new SearchResponse<object>();
			// end::c68404749f090ab191c0fd5f651635cf[]

			response0.MatchesExample(@"PUT _transform/ecommerce-customer-transform
			{
			  ""source"": {
			    ""index"": [
			      ""kibana_sample_data_ecommerce""
			    ],
			    ""query"": {
			      ""bool"": {
			        ""filter"": {
			          ""term"": {
			            ""currency"": ""EUR""
			          }
			        }
			      }
			    }
			  },
			  ""pivot"": {
			    ""group_by"": {
			      ""customer_id"": {
			        ""terms"": {
			          ""field"": ""customer_id""
			        }
			      }
			    },
			    ""aggregations"": {
			      ""total_quantity.sum"": {
			        ""sum"": {
			          ""field"": ""total_quantity""
			        }
			      },
			      ""taxless_total_price.sum"": {
			        ""sum"": {
			          ""field"": ""taxless_total_price""
			        }
			      },
			      ""total_quantity.max"": {
			        ""max"": {
			          ""field"": ""total_quantity""
			        }
			      },
			      ""order_id.cardinality"": {
			        ""cardinality"": {
			          ""field"": ""order_id""
			        }
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""ecommerce-customers""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/ecommerce-tutorial.asciidoc:233")]
		public void Line233()
		{
			// tag::4ded8ad815ac0e83b1c21a6c18fd0763[]
			var response0 = new SearchResponse<object>();
			// end::4ded8ad815ac0e83b1c21a6c18fd0763[]

			response0.MatchesExample(@"POST _transform/ecommerce-customer-transform/_start");
		}
	}
}
