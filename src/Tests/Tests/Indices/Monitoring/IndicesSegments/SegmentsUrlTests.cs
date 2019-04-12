using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Nest.Indices;

namespace Tests.Indices.Monitoring.IndicesSegments
{
	public class SegmentsUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET($"/_segments")
					.Request(c => c.Segments(new SegmentsRequest()))
					.RequestAsync(c => c.SegmentsAsync(new SegmentsRequest()))
				;
			
			await UrlTester.GET($"/_all/_segments")
					.Fluent(c => c.Segments(All))
					.Request(c => c.Segments(new SegmentsRequest(All)))
					.FluentAsync(c => c.SegmentsAsync(All))
					.RequestAsync(c => c.SegmentsAsync(new SegmentsRequest(All)))
				;

			var index = "index1,index2";
			await UrlTester.GET($"/index1%2Cindex2/_segments")
					.Fluent(c => c.Segments(index))
					.Request(c => c.Segments(new SegmentsRequest(index)))
					.FluentAsync(c => c.SegmentsAsync(index))
					.RequestAsync(c => c.SegmentsAsync(new SegmentsRequest(index)))
				;
		}
	}
}
