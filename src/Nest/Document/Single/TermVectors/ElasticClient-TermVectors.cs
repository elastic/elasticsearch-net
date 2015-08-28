using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		///<inheritdoc/>
		public ITermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, TermVectorsDescriptor<T>> termVectorSelector)
			where T : class
		{
			return this.Dispatcher.Dispatch<TermVectorsDescriptor<T>, TermVectorsRequestParameters, TermVectorsResponse>(
				termVectorSelector,
				(p, d) => this.LowLevelDispatch.TermvectorsDispatch<TermVectorsResponse>(p, d)
			);
		}

		///<inheritdoc/>
		public ITermVectorsResponse TermVectors(ITermVectorsRequest termvectorRequest)
		{
			return this.Dispatcher.Dispatch<ITermVectorsRequest, TermVectorsRequestParameters, TermVectorsResponse>(
				termvectorRequest,
				(p, d) => this.LowLevelDispatch.TermvectorsDispatch<TermVectorsResponse>(p, d)
			);
		}

		///<inheritdoc/>
		public Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, TermVectorsDescriptor<T>> termVectorSelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<TermVectorsDescriptor<T>, TermVectorsRequestParameters, TermVectorsResponse, ITermVectorsResponse>(
				termVectorSelector,
				(p, d) => this.LowLevelDispatch.TermvectorsDispatchAsync<TermVectorsResponse>(p, d)
			);
		}

		///<inheritdoc/>
		public Task<ITermVectorsResponse> TermVectorsAsync(ITermVectorsRequest termvectorRequest)
		{
			return this.Dispatcher.DispatchAsync<ITermVectorsRequest , TermVectorsRequestParameters, TermVectorsResponse, ITermVectorsResponse>(
				termvectorRequest,
				(p, d) => this.LowLevelDispatch.TermvectorsDispatchAsync<TermVectorsResponse>(p, d)
			);
		}

	}
}
