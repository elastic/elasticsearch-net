// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.MachineLearning.DeleteCalendarJob
{
	public class DeleteCalendarJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.DELETE("/_ml/calendars/calendar_id/jobs/job_id")
			.Fluent(c => c.MachineLearning.DeleteCalendarJob("calendar_id", "job_id", p => p))
			.Request(c => c.MachineLearning.DeleteCalendarJob(new DeleteCalendarJobRequest("calendar_id", "job_id")))
			.FluentAsync(c => c.MachineLearning.DeleteCalendarJobAsync("calendar_id", "job_id", p => p))
			.RequestAsync(c => c.MachineLearning.DeleteCalendarJobAsync(new DeleteCalendarJobRequest("calendar_id", "job_id")));
	}
}
