using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Multi termvectors API allows to get multiple termvectors based on an index, type and id.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html
		/// </summary>
		/// <param name="multiTermVectorsSelector">The descriptor describing the multi termvectors operation</param>
		IMultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> multiTermVectorsSelector = null);

		/// <inheritdoc/>
		IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest multiTermVectorsRequest);

		/// <inheritdoc/>
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> multiTermVectorsSelector = null);

		/// <inheritdoc/>
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest multiTermVectorsRequest);
	}

	public partial class ElasticClient
	{
		///<inheritdoc/>
		public IMultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> multiTermVectorsSelector = null) =>
			this.MultiTermVectors(multiTermVectorsSelector.InvokeOrDefault(new MultiTermVectorsDescriptor()));

		///<inheritdoc/>
		public IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest multiTermVectorsRequest) => 
			this.Dispatcher.Dispatch<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorsResponse>(
				multiTermVectorsRequest,
				this.LowLevelDispatch.MtermvectorsDispatch<MultiTermVectorsResponse>
			);

		///<inheritdoc/>
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> multiTermVectorsSelector = null) =>
			this.MultiTermVectorsAsync(multiTermVectorsSelector.InvokeOrDefault(new MultiTermVectorsDescriptor()));

		///<inheritdoc/>
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest multiTermVectorsRequest) => 
			this.Dispatcher.DispatchAsync<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorsResponse, IMultiTermVectorsResponse>(
				multiTermVectorsRequest,
				this.LowLevelDispatch.MtermvectorsDispatchAsync<MultiTermVectorsResponse>
			);
	}
}
