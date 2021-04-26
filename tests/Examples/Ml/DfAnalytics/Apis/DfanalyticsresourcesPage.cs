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

namespace Examples.Ml.DfAnalytics.Apis
{
	public class DfanalyticsresourcesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::7b8afc2612fb2cdf2263cff1dead852c[]
			var response0 = new SearchResponse<object>();
			// end::7b8afc2612fb2cdf2263cff1dead852c[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/loganalytics
			{
			  ""source"": {
			    ""index"": ""logdata""
			  },
			  ""dest"": {
			    ""index"": ""logdata_out""
			  },
			  ""analysis"": {
			    ""outlier_detection"": {
			    }
			  },
			  ""analyzed_fields"": {
			        ""includes"": [ ""request.bytes"", ""response.counts.error"" ],
			        ""excludes"": [ ""source.geo"" ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line149()
		{
			// tag::45172a704c1144f8d0242499464bc9ea[]
			var response0 = new SearchResponse<object>();
			// end::45172a704c1144f8d0242499464bc9ea[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/house_price_regression_analysis
			{
			  ""source"": {
			    ""index"": ""houses_sold_last_10_yrs"" <1>
			  },
			  ""dest"": {
			    ""index"": ""house_price_predictions"" <2>
			  },
			  ""analysis"":
			    {
			      ""regression"": { <3>
			        ""dependent_variable"": ""price"" <4>
			      }
			    }
			}");
		}
	}
}
