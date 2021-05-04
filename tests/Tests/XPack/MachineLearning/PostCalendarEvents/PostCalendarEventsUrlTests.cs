// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.MachineLearning.PostCalendarEvents
{
	public class PostCalendarEventsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("/_ml/calendars/calendar_id/events")
			.Fluent(c => c.MachineLearning.PostCalendarEvents("calendar_id", p => p))
			.Request(c => c.MachineLearning.PostCalendarEvents(new PostCalendarEventsRequest("calendar_id")))
			.FluentAsync(c => c.MachineLearning.PostCalendarEventsAsync("calendar_id", p => p))
			.RequestAsync(c => c.MachineLearning.PostCalendarEventsAsync(new PostCalendarEventsRequest("calendar_id")));
	}
}
