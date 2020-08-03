using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.DataStreams.Stats
{
	public class DataStreamsStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_data_stream/stream/_stats")
			.Fluent(c => c.Indices.DataStreamsStats("stream", f => f))
			.Request(c => c.Indices.DataStreamsStats(new DataStreamsStatsRequest("stream")))
			.FluentAsync(c => c.Indices.DataStreamsStatsAsync("stream", f => f))
			.RequestAsync(c => c.Indices.DataStreamsStatsAsync(new DataStreamsStatsRequest("stream")));
	}
}
