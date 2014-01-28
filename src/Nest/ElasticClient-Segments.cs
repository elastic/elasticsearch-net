using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public ISegmentsResponse Segments(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null)
		{
			segmentsSelector = segmentsSelector ?? (s => s);
			return this.Dispatch<SegmentsDescriptor, SegmentsQueryString, SegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatch(p)
			);
		}
	
		public Task<ISegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null)
		{
			segmentsSelector = segmentsSelector ?? (s => s);
			return this.DispatchAsync<SegmentsDescriptor, SegmentsQueryString, SegmentsResponse, ISegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatchAsync(p)
			);
		}
	}
}
