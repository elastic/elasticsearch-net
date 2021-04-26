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

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class JobresourcePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line374()
		{
			// tag::07daeea2d56f43ae1229860111dae8af[]
			var response0 = new SearchResponse<object>();
			// end::07daeea2d56f43ae1229860111dae8af[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/_validate
			{
			  ""analysis_config"" : {
			    ""categorization_analyzer"" : {
			      ""tokenizer"" : ""ml_classic"",
			      ""filter"" : [
			        { ""type"" : ""stop"", ""stopwords"": [
			          ""Monday"", ""Tuesday"", ""Wednesday"", ""Thursday"", ""Friday"", ""Saturday"", ""Sunday"",
			          ""Mon"", ""Tue"", ""Wed"", ""Thu"", ""Fri"", ""Sat"", ""Sun"",
			          ""January"", ""February"", ""March"", ""April"", ""May"", ""June"", ""July"", ""August"", ""September"", ""October"", ""November"", ""December"",
			          ""Jan"", ""Feb"", ""Mar"", ""Apr"", ""May"", ""Jun"", ""Jul"", ""Aug"", ""Sep"", ""Oct"", ""Nov"", ""Dec"",
			          ""GMT"", ""UTC""
			        ] }
			      ]
			    },
			    ""categorization_field_name"": ""message"",
			    ""detectors"" :[{
			      ""function"":""count"",
			      ""by_field_name"": ""mlcategory""
			    }]
			  },
			  ""data_description"" : {
			  }
			}");
		}
	}
}
