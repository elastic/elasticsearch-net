// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.DataStreams.Delete
{
	public class DeleteDataStreamUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_data_stream/stream")
			.Fluent(c => c.Indices.DeleteDataStream("stream", f => f))
			.Request(c => c.Indices.DeleteDataStream(new DeleteDataStreamRequest("stream")))
			.FluentAsync(c => c.Indices.DeleteDataStreamAsync("stream", f => f))
			.RequestAsync(c => c.Indices.DeleteDataStreamAsync(new DeleteDataStreamRequest("stream")));
	}
}
