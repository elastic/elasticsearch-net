// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.ReindexOnServer
{
	public class ReindexOnServerUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_reindex")
			.Fluent(c => c.ReindexOnServer(f => f))
			.Request(c => c.ReindexOnServer(new ReindexOnServerRequest()))
			.FluentAsync(c => c.ReindexOnServerAsync(f => f))
			.RequestAsync(c => c.ReindexOnServerAsync(new ReindexOnServerRequest()));
	}
}
