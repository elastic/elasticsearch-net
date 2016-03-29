using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector) where T : class;

		/// <inheritdoc/>
		ISuggestResponse Suggest(ISuggestRequest request);

		/// <inheritdoc/>
		Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector) where T : class;

		/// <inheritdoc/>
		Task<ISuggestResponse> SuggestAsync(ISuggestRequest request);
	}

	//TODO limit scope of fluent to IndexName of T

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector) where T : class =>
			this.Suggest(selector?.Invoke(new SuggestDescriptor<T>()));

		/// <inheritdoc/>
		public ISuggestResponse Suggest(ISuggestRequest request) => 
			this.Dispatcher.Dispatch<ISuggestRequest, SuggestRequestParameters, SuggestResponse>(
				request,
				this.LowLevelDispatch.SuggestDispatch<SuggestResponse>
			);

		/// <inheritdoc/>
		public Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector) where T : class => 
			this.SuggestAsync(selector?.Invoke(new SuggestDescriptor<T>()));

		/// <inheritdoc/>
		public Task<ISuggestResponse> SuggestAsync(ISuggestRequest request) => 
			this.Dispatcher.DispatchAsync<ISuggestRequest, SuggestRequestParameters, SuggestResponse, ISuggestResponse>(
				request,
				this.LowLevelDispatch.SuggestDispatchAsync<SuggestResponse>
			);
	}
}