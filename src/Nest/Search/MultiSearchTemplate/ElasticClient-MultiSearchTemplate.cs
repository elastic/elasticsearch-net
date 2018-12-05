using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;

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
		IMultiSearchResponse MultiSearchTemplate(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector);

		/// <inheritdoc />
		IMultiSearchResponse MultiSearchTemplate(IMultiSearchTemplateRequest request);

		/// <inheritdoc />
		Task<IMultiSearchResponse> MultiSearchTemplateAsync(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IMultiSearchResponse> MultiSearchTemplateAsync(IMultiSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiSearchResponse MultiSearchTemplate(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector) =>
			MultiSearchTemplate(selector?.Invoke(new MultiSearchTemplateDescriptor()));

		/// <inheritdoc />
		public IMultiSearchResponse MultiSearchTemplate(IMultiSearchTemplateRequest request) =>
			Dispatcher.Dispatch<IMultiSearchTemplateRequest, MultiSearchTemplateRequestParameters, MultiSearchResponse>(
				request,
				(p, d) =>
				{
					var formatter = CreateMultiSearchTemplateResponseFormatter(p);
					var serializer = ConnectionSettings.CreateStateful(formatter);
					var creator = new MultiSearchTemplateCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
					request.RequestParameters.DeserializationOverride = creator;
					return LowLevelDispatch.MsearchTemplateDispatch<MultiSearchResponse>(p, new SerializableData<IMultiSearchTemplateRequest>(p));
				}
			);

		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchTemplateAsync(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			MultiSearchTemplateAsync(selector?.Invoke(new MultiSearchTemplateDescriptor()), cancellationToken);


		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchTemplateAsync(IMultiSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) => Dispatcher.DispatchAsync<IMultiSearchTemplateRequest, MultiSearchTemplateRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
			request,
			cancellationToken,
			(p, d, c) =>
			{
				var formatter = CreateMultiSearchTemplateResponseFormatter(p);
				var serializer = ConnectionSettings.CreateStateful(formatter);
				var creator = new MultiSearchTemplateCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
				request.RequestParameters.DeserializationOverride = creator;
				return LowLevelDispatch.MsearchTemplateDispatchAsync<MultiSearchResponse>(p, new SerializableData<IMultiSearchTemplateRequest>(p), c);
			}
		);

		private MultiSearchResponseFormatter CreateMultiSearchTemplateResponseFormatter(IMultiSearchTemplateRequest request) =>
			new MultiSearchResponseFormatter(ConnectionSettings, request);
	}
}
