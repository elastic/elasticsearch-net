using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.Monitoring.Segments
{
	public class SegmentsUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_segments")
				.Fluent(c => c.Segments(Nest.Indices.All))
				.Request(c => c.Segments(new SegmentsRequest()))
				.FluentAsync(c => c.SegmentsAsync(Nest.Indices.All))
				.RequestAsync(c => c.SegmentsAsync(new SegmentsRequest()))
				;

			var index = "index1,index2";
			await GET($"/{index}/_segments")
				.Fluent(c => c.Segments(index))
				.Request(c => c.Segments(new SegmentsRequest(index)))
				.FluentAsync(c => c.SegmentsAsync(index))
				.RequestAsync(c => c.SegmentsAsync(new SegmentsRequest(index)))
				;
		}
	}
}
