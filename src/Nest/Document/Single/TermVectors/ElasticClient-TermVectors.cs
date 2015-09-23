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
		/// Returns information and statistics on terms in the fields of a particular document as stored in the index.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="termVectorSelector"></param>
		ITermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest> termVectorSelector)
			where T : class;

		/// <inheritdoc/>
		ITermVectorsResponse TermVectors(ITermVectorsRequest termvectorRequest);

		/// <inheritdoc/>
		Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest> termVectorSelector)
			where T : class;

		/// <inheritdoc/>
		Task<ITermVectorsResponse> TermVectorsAsync(ITermVectorsRequest termvectorRequest);

	}
	//TODO discuss with @gmarz, termvectors has a required id|doc but its not reflected in our constructors
	public partial class ElasticClient
	{
		///<inheritdoc/>
		public ITermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest> termVectorSelector) where T : class =>
			this.TermVectors(termVectorSelector?.Invoke(new TermVectorsDescriptor<T>(typeof(T), typeof(T))));

		///<inheritdoc/>
		public ITermVectorsResponse TermVectors(ITermVectorsRequest termvectorRequest)
		{
			return this.Dispatcher.Dispatch<ITermVectorsRequest, TermVectorsRequestParameters, TermVectorsResponse>(
				termvectorRequest,
				this.LowLevelDispatch.TermvectorsDispatch<TermVectorsResponse>
			);
		}

		///<inheritdoc/>
		public Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest> termVectorSelector) where T : class =>
			this.TermVectorsAsync(termVectorSelector?.Invoke(new TermVectorsDescriptor<T>(typeof(T), typeof(T))));

		///<inheritdoc/>
		public Task<ITermVectorsResponse> TermVectorsAsync(ITermVectorsRequest termvectorRequest) => 
			this.Dispatcher.DispatchAsync<ITermVectorsRequest, TermVectorsRequestParameters, TermVectorsResponse, ITermVectorsResponse>(
				termvectorRequest,
				this.LowLevelDispatch.TermvectorsDispatchAsync<TermVectorsResponse>
			);
	}
}
