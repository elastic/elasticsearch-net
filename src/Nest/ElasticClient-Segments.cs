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
			return this.Dispatch<SegmentsDescriptor, SegmentsRequestParameters, SegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatch<SegmentsResponse>(p)
			);
		}

		/// <inheritdoc />
		public ISegmentsResponse Segments(ISegmentsRequest segmentsRequest)
		{
			return this.Dispatch<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse>(
				segmentsRequest,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatch<SegmentsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null)
		{
			segmentsSelector = segmentsSelector ?? (s => s);
			return this.DispatchAsync<SegmentsDescriptor, SegmentsRequestParameters, SegmentsResponse, ISegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatchAsync<SegmentsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest segmentsRequest)
		{
			return this.DispatchAsync<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse, ISegmentsResponse>(
				segmentsRequest,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatchAsync<SegmentsResponse>(p)
			);
		}

	}
}