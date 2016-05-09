using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Register a percolator
		/// </summary>
		/// <typeparam name="T">The type to infer the index/type from, will also be used to strongly type the query</typeparam>
		/// <param name="name">The name for the percolator</param>
		/// <param name="selector">An optional descriptor describing the register percolator operation further</param>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		IRegisterPercolatorResponse RegisterPercolator<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class;

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		IRegisterPercolatorResponse RegisterPercolator(IRegisterPercolatorRequest request);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		Task<IRegisterPercolatorResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class;

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		Task<IRegisterPercolatorResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public IRegisterPercolatorResponse RegisterPercolator<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class =>
			this.RegisterPercolator(selector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public IRegisterPercolatorResponse RegisterPercolator(IRegisterPercolatorRequest request) =>
			this.Dispatcher.Dispatch<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolatorResponse>(
				request,
				this.LowLevelDispatch.IndexDispatch<RegisterPercolatorResponse>
			);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class =>
			this.RegisterPercolatorAsync(selector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request) =>
			this.Dispatcher.DispatchAsync<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolatorResponse, IRegisterPercolatorResponse>(
				request,
				this.LowLevelDispatch.IndexDispatchAsync<RegisterPercolatorResponse>
			);
	}
}
