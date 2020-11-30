// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.PointInTime.Close
{
	public class ClosePointInTimeUrlTests
	{
		[U] public async Task Urls() =>
			await DELETE("/_pit")
				.Fluent(c => c.ClosePointInTime())
				.Request(c => c.ClosePointInTime(new ClosePointInTimeRequest()))
				.FluentAsync(c => c.ClosePointInTimeAsync())
				.RequestAsync(c => c.ClosePointInTimeAsync(new ClosePointInTimeRequest()));
	}
}
