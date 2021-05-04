// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PutCalendarJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/put-calendar-job.asciidoc:38")]
		public void Line38()
		{
			// tag::1b2ab75d3c8064fac6ecc63104396c02[]
			var response0 = new SearchResponse<object>();
			// end::1b2ab75d3c8064fac6ecc63104396c02[]

			response0.MatchesExample(@"PUT _ml/calendars/planned-outages/jobs/total-requests");
		}
	}
}
