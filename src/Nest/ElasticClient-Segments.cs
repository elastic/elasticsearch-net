using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISegmentsResponse Segments(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null)
		{
			segmentsSelector = segmentsSelector ?? (s => s);
			return this.Dispatch<SegmentsDescriptor, SegmentsQueryString, SegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatch<SegmentsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null)
		{
			segmentsSelector = segmentsSelector ?? (s => s);
			return this.DispatchAsync<SegmentsDescriptor, SegmentsQueryString, SegmentsResponse, ISegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatchAsync<SegmentsResponse>(p)
			);
		}
	}
}