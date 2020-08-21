// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.Ping
{
	public class PingUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await HEAD("/")
			.Fluent(c => c.Ping())
			.Request(c => c.Ping(new PingRequest()))
			.FluentAsync(c => c.PingAsync())
			.RequestAsync(c => c.PingAsync(new PingRequest()));
	}
}
