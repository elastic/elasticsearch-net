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

namespace Examples.Aggregations.Metrics
{
	public class MedianAbsoluteDeviationAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/median-absolute-deviation-aggregation.asciidoc:28")]
		public void Line28()
		{
			// tag::25ed47fcb890fcf8d8518ae067362d18[]
			var response0 = new SearchResponse<object>();
			// end::25ed47fcb890fcf8d8518ae067362d18[]

			response0.MatchesExample(@"GET reviews/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""review_average"": {
			      ""avg"": {
			        ""field"": ""rating""
			      }
			    },
			    ""review_variability"": {
			      ""median_absolute_deviation"": {
			        ""field"": ""rating"" \<1>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/median-absolute-deviation-aggregation.asciidoc:87")]
		public void Line87()
		{
			// tag::9d662fc9f943c287b7144f5e4e2ae358[]
			var response0 = new SearchResponse<object>();
			// end::9d662fc9f943c287b7144f5e4e2ae358[]

			response0.MatchesExample(@"GET reviews/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""review_variability"": {
			      ""median_absolute_deviation"": {
			        ""field"": ""rating"",
			        ""compression"": 100
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/median-absolute-deviation-aggregation.asciidoc:116")]
		public void Line116()
		{
			// tag::bb964122f7d31b2f17c299d47ab3bdf3[]
			var response0 = new SearchResponse<object>();
			// end::bb964122f7d31b2f17c299d47ab3bdf3[]

			response0.MatchesExample(@"GET reviews/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""review_variability"": {
			      ""median_absolute_deviation"": {
			        ""script"": {
			          ""lang"": ""painless"",
			          ""source"": ""doc['rating'].value * params.scaleFactor"",
			          ""params"": {
			            ""scaleFactor"": 2
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/median-absolute-deviation-aggregation.asciidoc:140")]
		public void Line140()
		{
			// tag::920362adc347f4268b29751d638b2e87[]
			var response0 = new SearchResponse<object>();
			// end::920362adc347f4268b29751d638b2e87[]

			response0.MatchesExample(@"GET reviews/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""review_variability"": {
			      ""median_absolute_deviation"": {
			        ""script"": {
			          ""id"": ""my_script"",
			          ""params"": {
			            ""field"": ""rating""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/median-absolute-deviation-aggregation.asciidoc:170")]
		public void Line170()
		{
			// tag::87f854393d715aabf4d45e90a8eb74ce[]
			var response0 = new SearchResponse<object>();
			// end::87f854393d715aabf4d45e90a8eb74ce[]

			response0.MatchesExample(@"GET reviews/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""review_variability"": {
			      ""median_absolute_deviation"": {
			        ""field"": ""rating"",
			        ""missing"": 5
			      }
			    }
			  }
			}");
		}
	}
}
