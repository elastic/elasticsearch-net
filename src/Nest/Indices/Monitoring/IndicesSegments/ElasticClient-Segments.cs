using System;
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
		/// <param name="selector">A descriptor that describes the parameters for the segments operation</param>
		ISegmentsResponse Segments(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector = null);

		/// <inheritdoc/>
		ISegmentsResponse Segments(ISegmentsRequest request);

		/// <inheritdoc/>
		Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector = null);

		/// <inheritdoc/>
		Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISegmentsResponse Segments(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector = null) =>
			this.Segments(selector.InvokeOrDefault(new SegmentsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public ISegmentsResponse Segments(ISegmentsRequest request) => 
			this.Dispatcher.Dispatch<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesSegmentsDispatch<SegmentsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector = null) => 
			this.SegmentsAsync(selector.InvokeOrDefault(new SegmentsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest request) => 
			this.Dispatcher.DispatchAsync<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse, ISegmentsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesSegmentsDispatchAsync<SegmentsResponse>(p)
			);
	}
}