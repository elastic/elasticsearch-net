using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The count API allows to easily execute a query and get the number of matches for that query.
		/// It can be executed across one or more indices and across one or more types.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-count.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-count.html</a>
		/// </summary>
		/// <typeparam name="T">
		/// The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query
		/// </typeparam>
		/// <param name="selector">An optional descriptor to further describe the count operation</param>
		ICountResponse Count<T>(Func<CountDescriptor<T>, ICountRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		ICountResponse Count<T>(ICountRequest request)
			where T : class;

		/// <inheritdoc />
		Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, ICountRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<ICountResponse> CountAsync<T>(ICountRequest request, CancellationToken ct = default)
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICountResponse Count<T>(Func<CountDescriptor<T>, ICountRequest> selector = null)
			where T : class =>
			Count<T>(selector.InvokeOrDefault(new CountDescriptor<T>()));

		/// <inheritdoc />
		public ICountResponse Count<T>(ICountRequest request)
			where T : class =>
			DoRequest<ICountRequest, CountResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ICountResponse> CountAsync<T>(
			Func<CountDescriptor<T>, ICountRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			CountAsync<T>(selector.InvokeOrDefault(new CountDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<ICountResponse> CountAsync<T>(ICountRequest request, CancellationToken ct = default)
			where T : class =>
			DoRequestAsync<ICountRequest, ICountResponse, CountResponse>(request, request.RequestParameters, ct);
	}
}
