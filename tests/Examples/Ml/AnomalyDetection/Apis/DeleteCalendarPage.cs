// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteCalendarPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-calendar.asciidoc:40")]
		public void Line40()
		{
			// tag::63893e7e9479a9b60db71dcddcc79aaf[]
			var response0 = new SearchResponse<object>();
			// end::63893e7e9479a9b60db71dcddcc79aaf[]

			response0.MatchesExample(@"DELETE _ml/calendars/planned-outages");
		}
	}
}
