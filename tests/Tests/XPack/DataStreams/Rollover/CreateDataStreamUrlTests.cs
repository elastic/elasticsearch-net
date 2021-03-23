using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.DataStreams.Create
{
	public class DataStreamRolloverUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/stream/_rollover")
			.Fluent(c => c.Indices.DataStreamRollover("stream", f => f))
			.Request(c => c.Indices.DataStreamRollover(new DataStreamRolloverRequest("stream")))
			.FluentAsync(c => c.Indices.DataStreamRolloverAsync("stream", f => f))
			.RequestAsync(c => c.Indices.DataStreamRolloverAsync(new DataStreamRolloverRequest("stream")));
	}
}
