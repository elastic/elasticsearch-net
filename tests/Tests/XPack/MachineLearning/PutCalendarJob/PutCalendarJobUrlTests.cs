// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.PutCalendarJob
{
	public class PutCalendarJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_ml/calendars/calendar_id/jobs/job_id")
			.Fluent(c => c.MachineLearning.PutCalendarJob("calendar_id", "job_id", p => p))
			.Request(c => c.MachineLearning.PutCalendarJob(new PutCalendarJobRequest("calendar_id", "job_id")))
			.FluentAsync(c => c.MachineLearning.PutCalendarJobAsync("calendar_id", "job_id", p => p))
			.RequestAsync(c => c.MachineLearning.PutCalendarJobAsync(new PutCalendarJobRequest("calendar_id", "job_id")));
	}
}
