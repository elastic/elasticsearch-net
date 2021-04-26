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

namespace Examples.Transform.Apis
{
	public class PreviewTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/preview-transform.asciidoc:196")]
		public void Line196()
		{
			// tag::a5ee3f40c34bd913a12e0069b6e42611[]
			var response0 = new SearchResponse<object>();
			// end::a5ee3f40c34bd913a12e0069b6e42611[]

			response0.MatchesExample(@"POST _transform/_preview
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_ecommerce""
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
			      ""max_price"": {
			        ""max"": {
			          ""field"": ""taxful_total_price""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
