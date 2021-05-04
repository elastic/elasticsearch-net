// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.DataStreams.Promote
{
	public class PromoteDataStreamUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_data_stream/_promote/stream")
			.Fluent(c => c.Indices.PromoteDataStream("stream", f => f))
			.Request(c => c.Indices.PromoteDataStream(new PromoteDataStreamRequest("stream")))
			.FluentAsync(c => c.Indices.PromoteDataStreamAsync("stream", f => f))
			.RequestAsync(c => c.Indices.PromoteDataStreamAsync(new PromoteDataStreamRequest("stream")));
	}
}
