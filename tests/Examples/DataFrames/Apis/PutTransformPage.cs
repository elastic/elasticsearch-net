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

namespace Examples.DataFrames.Apis
{
	public class PutTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line143()
		{
			// tag::6e2c969573581cc1c4ae83f59ef8d2f0[]
			var response0 = new SearchResponse<object>();
			// end::6e2c969573581cc1c4ae83f59ef8d2f0[]

			response0.MatchesExample(@"PUT _data_frame/transforms/ecommerce_transform
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_ecommerce"",
			    ""query"": {
			      ""term"": {
			        ""geoip.continent_name"": {
			          ""value"": ""Asia""
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
			      ""max_price"": {
			        ""max"": {
			          ""field"": ""taxful_total_price""
			        }
			      }
			    }
			  },
			  ""description"": ""Maximum priced ecommerce data by customer_id in Asia"",
			  ""dest"": {
			    ""index"": ""kibana_sample_data_ecommerce_transform"",
			    ""pipeline"": ""add_timestamp_pipeline""
			  },
			  ""frequency"": ""5m"",
			  ""sync"": {
			    ""time"": {
			      ""field"": ""order_date"",
			      ""delay"": ""60s""
			    }
			  }
			}");
		}
	}
}
