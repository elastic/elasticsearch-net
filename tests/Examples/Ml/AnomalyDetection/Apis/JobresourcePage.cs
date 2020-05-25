// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
