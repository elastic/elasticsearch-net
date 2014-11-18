using System.Collections.Generic;

namespace Elasticsearch.Net.Tests.Unit.Memory.Helpers
{
	public interface IMemorySetup<T> where T : class
	{
		List<TrackableMemoryStream> CreatedMemoryStreams { get; }
		TrackableMemoryStream ResponseStream { get; }
		ElasticsearchResponse<T> Result { get; set; }
	}
}