// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetCalendarEvents
{
	public class GetCalendarEventsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_ml/calendars/1/events")
				.Request(c => c.MachineLearning.GetCalendarEvents(new GetCalendarEventsRequest(1)))
				.Fluent(c => c.MachineLearning.GetCalendarEvents(1, r => r))
				.FluentAsync(c => c.MachineLearning.GetCalendarEventsAsync(1, r => r))
				.RequestAsync(c => c.MachineLearning.GetCalendarEventsAsync(new GetCalendarEventsRequest(1)));
	}
}
