using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a new typed document in a specific index. If a document with the same index, type and id already exists,
		/// A 409 Conflict HTTP response status code and error will be returned.
		/// </summary>
		/// <typeparam name="T">The document type used to infer the default index, type and id</typeparam>
		/// <param name="document">The document to be created. Id will be inferred from (in order):
		/// <para>1. Id property set up on <see cref="ConnectionSettings"/> for <typeparamref name="T"/></para>
		/// <para>2. <see cref="ElasticsearchTypeAttribute.IdProperty"/> property on <see cref="ElasticsearchTypeAttribute"/> applied to <typeparamref name="T"/></para>
		/// <para>3. A property named Id on <typeparamref name="T"/></para>
		/// </param>
		/// <param name="selector">Optionally further describe the create operation i.e override type/index/id</param>
		ICreateResponse Create<T>(T document, Func<CreateDescriptor<T>, ICreateRequest> selector = null) where T : class;

		/// <inheritdoc/>
		ICreateResponse Create(ICreateRequest request);

		/// <inheritdoc/>
		Task<ICreateResponse> CreateAsync<T>(T document, Func<CreateDescriptor<T>, ICreateRequest> selector = null) where T : class;

		/// <inheritdoc/>
		Task<ICreateResponse> CreateAsync(ICreateRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICreateResponse Create<T>(T document, Func<CreateDescriptor<T>, ICreateRequest> selector = null) where T : class
		{
			var request = document as ICreateRequest;
			return this.Create(request ?? selector.InvokeOrDefault(new CreateDescriptor<T>(document)));
		}

		/// <inheritdoc/>
		public ICreateResponse Create(ICreateRequest request) =>
			this.Dispatcher.Dispatch<ICreateRequest, CreateRequestParameters, CreateResponse>(
				request,
				this.LowLevelDispatch.CreateDispatch<CreateResponse>
			);

		/// <inheritdoc/>
		public Task<ICreateResponse> CreateAsync<T>(T document, Func<CreateDescriptor<T>, ICreateRequest> selector = null) where T : class
		{
			var request = document as ICreateRequest;
			return this.CreateAsync(request ?? selector.InvokeOrDefault(new CreateDescriptor<T>(document)));
		}

		/// <inheritdoc/>
		public Task<ICreateResponse> CreateAsync(ICreateRequest request) =>
			this.Dispatcher.DispatchAsync<ICreateRequest, CreateRequestParameters, CreateResponse, ICreateResponse>(
				request,
				this.LowLevelDispatch.CreateDispatchAsync<CreateResponse>
			);
	}
}
