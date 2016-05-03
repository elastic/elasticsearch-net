using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped elasticsearch endpoints
	/// </summary>
	public partial class ElasticClient : IElasticClient, IHighLevelToLowLevelDispatcher
	{
		private IHighLevelToLowLevelDispatcher Dispatcher => this;

		private LowLevelDispatch LowLevelDispatch { get; }

		private ITransport<IConnectionSettingsValues> Transport { get; }

		public IElasticsearchSerializer Serializer => this.Transport.Settings.Serializer;
		public Inferrer Infer => this.Transport.Settings.Inferrer;
		public IConnectionSettingsValues ConnectionSettings => this.Transport.Settings;

		public IElasticLowLevelClient LowLevel { get; }

		public ElasticClient() : this(new ConnectionSettings(new Uri("http://localhost:9200"))) { }
		public ElasticClient(Uri uri) : this(new ConnectionSettings(uri)) { }
		public ElasticClient(IConnectionSettingsValues connectionSettings)
			: this(new Transport<IConnectionSettingsValues>(connectionSettings ?? new ConnectionSettings())) { }

		public ElasticClient(ITransport<IConnectionSettingsValues> transport)
		{
			transport.ThrowIfNull(nameof(transport));
			transport.Settings.ThrowIfNull(nameof(transport.Settings));
			transport.Settings.Serializer.ThrowIfNull(nameof(transport.Settings.Serializer));
			transport.Settings.Inferrer.ThrowIfNull(nameof(transport.Settings.Inferrer));

			this.Transport = transport;
			this.LowLevel = new ElasticLowLevelClient(this.Transport);
			this.LowLevelDispatch = new LowLevelDispatch(this.LowLevel);
		}

		TResponse IHighLevelToLowLevelDispatcher.Dispatch<TRequest, TQueryString, TResponse>(
			TRequest request,
			Func<TRequest, PostData<object>,
			ElasticsearchResponse<TResponse>> dispatch
			) => this.Dispatcher.Dispatch<TRequest,TQueryString,TResponse>(request, null, dispatch);

		TResponse IHighLevelToLowLevelDispatcher.Dispatch<TRequest, TQueryString, TResponse>(
			TRequest request, Func<IApiCallDetails, Stream, TResponse> responseGenerator,
			Func<TRequest, PostData<object>, ElasticsearchResponse<TResponse>> dispatch
			)
		{
			request.RouteValues.Resolve(this.ConnectionSettings);
			request.RequestParameters.DeserializationOverride(responseGenerator);

			var response = dispatch(request, request);
			return ResultsSelector(response);
		}

		Task<TResponseInterface> IHighLevelToLowLevelDispatcher.DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest descriptor,
			Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch
			) => this.Dispatcher.DispatchAsync<TRequest,TQueryString,TResponse,TResponseInterface>(descriptor, null, dispatch);

		async Task<TResponseInterface> IHighLevelToLowLevelDispatcher.DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest request,
			Func<IApiCallDetails, Stream, TResponse> responseGenerator,
			Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch
			)
		{
			request.RouteValues.Resolve(this.ConnectionSettings);
			request.RequestParameters.DeserializationOverride(responseGenerator);
			var response = await dispatch(request, request).ConfigureAwait(false);
			return ResultsSelector(response);
		}

		private static TResponse ResultsSelector<TResponse>(ElasticsearchResponse<TResponse> c)
			where TResponse : ResponseBase =>
			c.Body ?? CreateInvalidInstance<TResponse>(c);

		private static TResponse CreateInvalidInstance<TResponse>(IApiCallDetails response)
			where TResponse : ResponseBase
		{
			var r = typeof(TResponse).CreateInstance<TResponse>();
			((IBodyWithApiCallDetails)r).CallDetails = response;
			return r;
		}

		private TRequest ForceConfiguration<TRequest, TParams>(TRequest request, Action<IRequestConfiguration> setter)
			where TRequest : IRequest<TParams>
			where TParams : IRequestParameters, new()
		{
			var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
			setter(configuration);
			request.RequestParameters.RequestConfiguration = configuration;
			return request;
		}

	}
}
