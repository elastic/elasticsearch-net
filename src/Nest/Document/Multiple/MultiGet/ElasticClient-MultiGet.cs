using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <param name="selector">A descriptor describing which documents should be fetched</param>
		IMultiGetResponse MultiGet(Func<MultiGetDescriptor, IMultiGetRequest> selector = null);

		/// <inheritdoc />
		IMultiGetResponse MultiGet(IMultiGetRequest request);

		/// <inheritdoc />
		Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, IMultiGetRequest> selector = null) =>
			MultiGet(selector.InvokeOrDefault(new MultiGetDescriptor()));

		/// <inheritdoc />
		public IMultiGetResponse MultiGet(IMultiGetRequest request) =>
			Dispatcher.Dispatch<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse>(
				request,
				(r, s) => DeserializeMultiGetResponse(r, s, CreateCovariantMultiGetResponseFormatter(request)),
				LowLevelDispatch.MgetDispatch<MultiGetResponse>
			);

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			MultiGetAsync(selector.InvokeOrDefault(new MultiGetDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse, IMultiGetResponse>(
				request,
				cancellationToken,
				(r, s) => DeserializeMultiGetResponse(r, s, CreateCovariantMultiGetResponseFormatter(request)),
				LowLevelDispatch.MgetDispatchAsync<MultiGetResponse>
			);

		private MultiGetResponse DeserializeMultiGetResponse(IApiCallDetails response, Stream stream, IJsonFormatter<MultiGetResponse> formatter) =>
			ConnectionSettings.CreateStateful(formatter).Deserialize<MultiGetResponse>(stream);

		private MultiGetResponseFormatter CreateCovariantMultiGetResponseFormatter(IMultiGetRequest descriptor) =>
			new MultiGetResponseFormatter(descriptor);
	}
}
