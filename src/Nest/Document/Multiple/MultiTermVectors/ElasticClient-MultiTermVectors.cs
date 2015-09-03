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
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="multiTermVectorsSelector">The descriptor describing the multi termvectors operation</param>
		IMultiTermVectorResponse MultiTermVectors<T>(Func<MultiTermVectorsDescriptor<T>, IMultiTermVectorsRequest> multiTermVectorsSelector)
			where T : class;

		/// <inheritdoc/>
		IMultiTermVectorResponse MultiTermVectors(IMultiTermVectorsRequest multiTermVectorsRequest);

		/// <inheritdoc/>
		Task<IMultiTermVectorResponse> MultiTermVectorsAsync<T>(Func<MultiTermVectorsDescriptor<T>, IMultiTermVectorsRequest> multiTermVectorsSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IMultiTermVectorResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest multiTermVectorsRequest);
	}

	public partial class ElasticClient
	{
		///<inheritdoc/>
		public IMultiTermVectorResponse MultiTermVectors<T>(Func<MultiTermVectorsDescriptor<T>, IMultiTermVectorsRequest> multiTermVectorsSelector)
			where T : class =>
			this.MultiTermVectors(multiTermVectorsSelector?.Invoke(new MultiTermVectorsDescriptor<T>()));

		///<inheritdoc/>
		public IMultiTermVectorResponse MultiTermVectors(IMultiTermVectorsRequest multiTermVectorsRequest) => 
			this.Dispatcher.Dispatch<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorResponse>(
				multiTermVectorsRequest,
				this.LowLevelDispatch.MtermvectorsDispatch<MultiTermVectorResponse>
			);

		///<inheritdoc/>
		public Task<IMultiTermVectorResponse> MultiTermVectorsAsync<T>(Func<MultiTermVectorsDescriptor<T>, IMultiTermVectorsRequest> multiTermVectorsSelector)
			where T : class =>
			this.MultiTermVectorsAsync(multiTermVectorsSelector?.Invoke(new MultiTermVectorsDescriptor<T>()));

		///<inheritdoc/>
		public Task<IMultiTermVectorResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest multiTermVectorsRequest) => 
			this.Dispatcher.DispatchAsync<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorResponse, IMultiTermVectorResponse>(
				multiTermVectorsRequest,
				this.LowLevelDispatch.MtermvectorsDispatchAsync<MultiTermVectorResponse>
			);
	}
}
