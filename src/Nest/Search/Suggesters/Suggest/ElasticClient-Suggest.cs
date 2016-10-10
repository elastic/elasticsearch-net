using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The suggest feature suggests similar looking terms based on a provided text by using a suggester.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-suggesters.html
		/// </summary>
		/// <typeparam name="T">The type used to strongly type parts of the suggest operation</typeparam>
		/// <param name="selector">The suggesters to use this operation (can be multiple)</param>
		ISuggestResponse<T> Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector) where T : class;

		/// <inheritdoc/>
		ISuggestResponse<T> Suggest<T>(ISuggestRequest request) where T : class;

		/// <inheritdoc/>
		Task<ISuggestResponse<T>> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<ISuggestResponse<T>> SuggestAsync<T>(ISuggestRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

	}

	//TODO limit scope of fluent to IndexName of T

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISuggestResponse<T> Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector) where T : class =>
			this.Suggest<T>(selector?.Invoke(new SuggestDescriptor<T>()));

		/// <inheritdoc/>
		public ISuggestResponse<T> Suggest<T>(ISuggestRequest request) where T : class =>
			this.Dispatcher.Dispatch<ISuggestRequest, SuggestRequestParameters, SuggestResponse<T>>(
				request,
				this.LowLevelDispatch.SuggestDispatch<SuggestResponse<T>>
			);

		/// <inheritdoc/>
		public Task<ISuggestResponse<T>> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.SuggestAsync<T>(selector?.Invoke(new SuggestDescriptor<T>()), cancellationToken);

		/// <inheritdoc/>
		public Task<ISuggestResponse<T>> SuggestAsync<T>(ISuggestRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.Dispatcher.DispatchAsync<ISuggestRequest, SuggestRequestParameters, SuggestResponse<T>, ISuggestResponse<T>>(
				request,
				cancellationToken,
				this.LowLevelDispatch.SuggestDispatchAsync<SuggestResponse<T>>
			);
	}
}
