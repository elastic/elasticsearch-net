using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		///<inheritdoc />
		public IMultiTermVectorResponse MultiTermVectors<T>(Func<MultiTermVectorsDescriptor<T>, MultiTermVectorsDescriptor<T>> multiTermVectorsSelector)
			where T : class
		{
			return this.Dispatch<MultiTermVectorsDescriptor<T>, MultiTermVectorsRequestParameters, MultiTermVectorResponse>(
				multiTermVectorsSelector,
				(p, d) => this.RawDispatch.MtermvectorsDispatch<MultiTermVectorResponse>(p, d)
			);
		}

		///<inheritdoc />
		public IMultiTermVectorResponse MultiTermVectors(IMultiTermVectorsRequest multiTermVectorsRequest)
		{
			return this.Dispatch<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorResponse>(
				multiTermVectorsRequest,
				(p, d) => this.RawDispatch.MtermvectorsDispatch<MultiTermVectorResponse>(p, d)
			);
		}

		///<inheritdoc />
		public Task<IMultiTermVectorResponse> MultiTermVectorsAsync<T>(Func<MultiTermVectorsDescriptor<T>, MultiTermVectorsDescriptor<T>> multiTermVectorsSelector)
			where T : class
		{
			return this.DispatchAsync<MultiTermVectorsDescriptor<T>, MultiTermVectorsRequestParameters, MultiTermVectorResponse, IMultiTermVectorResponse>(
				multiTermVectorsSelector,
				(p, d) => this.RawDispatch.MtermvectorsDispatchAsync<MultiTermVectorResponse>(p, d)
			);
		}

		///<inheritdoc />
		public Task<IMultiTermVectorResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest multiTermVectorsRequest)
		{
			return this.DispatchAsync<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorResponse, IMultiTermVectorResponse>(
				multiTermVectorsRequest,
				(p, d) => this.RawDispatch.MtermvectorsDispatchAsync<MultiTermVectorResponse>(p, d)
			);
		}

	}
}
