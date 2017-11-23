using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	using System.Threading;
	using MultiSearchTemplateCreator = Func<IApiCallDetails, Stream, MultiSearchResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the search operations on the multi search api</param>
		IMultiSearchResponse MultiSearchTemplate(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector);

		/// <inheritdoc/>
		IMultiSearchResponse MultiSearchTemplate(IMultiSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchTemplateAsync(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchTemplateAsync(IMultiSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiSearchResponse MultiSearchTemplate(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector) =>
			this.MultiSearchTemplate(selector?.Invoke(new MultiSearchTemplateDescriptor()));

		/// <inheritdoc />
		public IMultiSearchResponse MultiSearchTemplate(IMultiSearchTemplateRequest request)
		{
			return this.Dispatcher.Dispatch<IMultiSearchTemplateRequest, MultiSearchTemplateRequestParameters, MultiSearchResponse>(
				request,
				(p, d) =>
				{
					var converter = CreateMultiSearchTemplateDeserializer(p);
					var serializer = this.ConnectionSettings.StatefulSerializer(converter);
					var creator = new MultiSearchTemplateCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
					request.RequestParameters.DeserializationOverride(creator);
					return this.LowLevelDispatch.MsearchTemplateDispatch<MultiSearchResponse>(p, (object)p);
				}
			);
		}

		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchTemplateAsync(Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.MultiSearchTemplateAsync(selector?.Invoke(new MultiSearchTemplateDescriptor()), cancellationToken);


		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchTemplateAsync(IMultiSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Dispatcher.DispatchAsync<IMultiSearchTemplateRequest, MultiSearchTemplateRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				request,
				cancellationToken,
				(p, d, c) =>
				{
					var converter = CreateMultiSearchTemplateDeserializer(p);
					var serializer = this.ConnectionSettings.StatefulSerializer(converter);
					var creator = new MultiSearchTemplateCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
					request.RequestParameters.DeserializationOverride(creator);
					return this.LowLevelDispatch.MsearchTemplateDispatchAsync<MultiSearchResponse>(p, (object)p, c);
				}
			);
		}

		private JsonConverter CreateMultiSearchTemplateDeserializer(IMultiSearchTemplateRequest request)
		{
			if (request.Operations != null)
			{
				foreach (var operation in request.Operations.Values)
					CovariantSearch.CloseOverAutomagicCovariantResultSelector(this.Infer, operation);
			}

			return new MultiSearchResponseJsonConverter(this.ConnectionSettings, request);
		}
	}
}
