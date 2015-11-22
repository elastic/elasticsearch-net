using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Provide low level segments information that a Lucene index (shard level) is built with. 
		/// Allows to be used to provide more information on the state of a shard and an index, possibly optimization information,
		/// data "wasted" on deletes, and so on.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-segments.html
		/// </summary>
		/// <param name="segmentsSelector">A descriptor that describes the parameters for the segments operation</param>
		ISegmentsResponse Segments(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> segmentsSelector = null);

		/// <inheritdoc/>
		ISegmentsResponse Segments(ISegmentsRequest segmentsRequest);

		/// <inheritdoc/>
		Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> segmentsSelector = null);

		/// <inheritdoc/>
		Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest segmentsRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISegmentsResponse Segments(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> segmentsSelector = null) =>
			this.Segments(segmentsSelector.InvokeOrDefault(new SegmentsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public ISegmentsResponse Segments(ISegmentsRequest segmentsRequest) => 
			this.Dispatcher.Dispatch<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse>(
				segmentsRequest,
				(p, d) => this.LowLevelDispatch.IndicesSegmentsDispatch<SegmentsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> segmentsSelector = null) => 
			this.SegmentsAsync(segmentsSelector.InvokeOrDefault(new SegmentsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest segmentsRequest) => 
			this.Dispatcher.DispatchAsync<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse, ISegmentsResponse>(
				segmentsRequest,
				(p, d) => this.LowLevelDispatch.IndicesSegmentsDispatchAsync<SegmentsResponse>(p)
			);
	}
}