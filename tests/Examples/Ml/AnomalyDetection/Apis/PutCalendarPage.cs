// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PutCalendarPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/put-calendar.asciidoc:46")]
		public void Line46()
		{
			// tag::e61b5abe85000cc954a42e2cd74f3a26[]
			var response0 = new SearchResponse<object>();
			// end::e61b5abe85000cc954a42e2cd74f3a26[]

			response0.MatchesExample(@"PUT _ml/calendars/planned-outages");
		}
	}
}
