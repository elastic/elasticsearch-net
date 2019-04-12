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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, IMultiGetRequest> selector = null) =>
			MultiGet(selector.InvokeOrDefault(new MultiGetDescriptor()));

		/// <inheritdoc />
		public IMultiGetResponse MultiGet(IMultiGetRequest request)
		{
			request.RequestParameters.DeserializationOverride = CreateMultiGetDeserializer(request);
			return DoRequest<IMultiGetRequest, MultiGetResponse>(request, request.RequestParameters);
		}

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(
			Func<MultiGetDescriptor, IMultiGetRequest> selector = null,
			CancellationToken ct = default
		) => MultiGetAsync(selector.InvokeOrDefault(new MultiGetDescriptor()), ct);

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request, CancellationToken ct = default)
		{
			request.RequestParameters.DeserializationOverride = CreateMultiGetDeserializer(request);
			return DoRequestAsync<IMultiGetRequest, IMultiGetResponse, MultiGetResponse>(request, request.RequestParameters, ct);
		}

		private Func<IApiCallDetails, Stream, object> CreateMultiGetDeserializer(IMultiGetRequest request)
		{
			var formatter = new MultiGetResponseFormatter(request);
			return (r, s) => ConnectionSettings.CreateStateful(formatter).Deserialize<MultiGetResponse>(s);
		}

	}
}
