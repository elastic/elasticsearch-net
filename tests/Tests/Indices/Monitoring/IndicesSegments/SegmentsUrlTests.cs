// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Nest.Indices;

namespace Tests.Indices.Monitoring.IndicesSegments
{
	public class SegmentsUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET($"/_segments")
					.Request(c => c.Indices.Segments(new SegmentsRequest()))
					.RequestAsync(c => c.Indices.SegmentsAsync(new SegmentsRequest()))
				;

			await UrlTester.GET($"/_all/_segments")
					.Fluent(c => c.Indices.Segments(All))
					.Request(c => c.Indices.Segments(new SegmentsRequest(All)))
					.FluentAsync(c => c.Indices.SegmentsAsync(All))
					.RequestAsync(c => c.Indices.SegmentsAsync(new SegmentsRequest(All)))
				;

			var index = "index1,index2";
			await UrlTester.GET($"/index1%2Cindex2/_segments")
					.Fluent(c => c.Indices.Segments(index))
					.Request(c => c.Indices.Segments(new SegmentsRequest(index)))
					.FluentAsync(c => c.Indices.SegmentsAsync(index))
					.RequestAsync(c => c.Indices.SegmentsAsync(new SegmentsRequest(index)))
				;
		}
	}
}
