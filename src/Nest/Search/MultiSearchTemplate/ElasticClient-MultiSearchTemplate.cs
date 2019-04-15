using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	using MultiSearchTemplateCreator = Func<IApiCallDetails, Stream, MultiSearchResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the search operations on the multi search api</param>
		MultiSearchResponse MultiSearchTemplate(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector);

		/// <inheritdoc />
		MultiSearchResponse MultiSearchTemplate(IMultiSearchTemplateRequest request);

		/// <inheritdoc />
		Task<MultiSearchResponse> MultiSearchTemplateAsync(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<MultiSearchResponse> MultiSearchTemplateAsync(IMultiSearchTemplateRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public MultiSearchResponse MultiSearchTemplate(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector) =>
			MultiSearchTemplate(selector?.Invoke(new MultiSearchTemplateDescriptor()));

		/// <inheritdoc />
		public MultiSearchResponse MultiSearchTemplate(IMultiSearchTemplateRequest request)
		{
			CreateMultiSearchTemplateDeserializer(request);
			return DoRequest<IMultiSearchTemplateRequest, MultiSearchResponse>(request, request.RequestParameters);
		}

		/// <inheritdoc />
		public Task<MultiSearchResponse> MultiSearchTemplateAsync(
			Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector,
			CancellationToken ct = default
		) => MultiSearchTemplateAsync(selector?.Invoke(new MultiSearchTemplateDescriptor()), ct);

		/// <inheritdoc />
		public Task<MultiSearchResponse> MultiSearchTemplateAsync(IMultiSearchTemplateRequest request, CancellationToken ct = default)
		{
			CreateMultiSearchTemplateDeserializer(request);
			return DoRequestAsync<IMultiSearchTemplateRequest, MultiSearchResponse, MultiSearchResponse>(request, request.RequestParameters, ct);
		}

		private void CreateMultiSearchTemplateDeserializer(IMultiSearchTemplateRequest request)
		{
			var formatter = new MultiSearchResponseFormatter(request);
			var serializer = ConnectionSettings.CreateStateful(formatter);
			var creator = new MultiSearchTemplateCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
			request.RequestParameters.DeserializationOverride = creator;
		}

	}
}
