using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.DataStreams.Get
{
	public class GetDataStreamUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_data_stream/stream")
			.Fluent(c => c.Indices.GetDataStream("stream", f => f))
			.Request(c => c.Indices.GetDataStream(new GetDataStreamRequest("stream")))
			.FluentAsync(c => c.Indices.GetDataStreamAsync("stream", f => f))
			.RequestAsync(c => c.Indices.GetDataStreamAsync(new GetDataStreamRequest("stream")));
	}
}
