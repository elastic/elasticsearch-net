// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.DataStreams.Create
{
	public class CreateDataStreamUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_data_stream/stream")
			.Fluent(c => c.Indices.CreateDataStream("stream", f => f))
			.Request(c => c.Indices.CreateDataStream(new CreateDataStreamRequest("stream")))
			.FluentAsync(c => c.Indices.CreateDataStreamAsync("stream", f => f))
			.RequestAsync(c => c.Indices.CreateDataStreamAsync(new CreateDataStreamRequest("stream")));
	}
}
