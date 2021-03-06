// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.DataStreams.Migrate
{
	public class MigrateToDataStreamUrlTests : UrlTestsBase
	{
		[U]
		public override async Task Urls() => await POST("/_data_stream/_migrate/stream")
			.Fluent(c => c.Indices.MigrateToDataStream("stream", f => f))
			.Request(c => c.Indices.MigrateToDataStream(new MigrateToDataStreamRequest("stream")))
			.FluentAsync(c => c.Indices.MigrateToDataStreamAsync("stream", f => f))
			.RequestAsync(c => c.Indices.MigrateToDataStreamAsync(new MigrateToDataStreamRequest("stream")));
	}
}
