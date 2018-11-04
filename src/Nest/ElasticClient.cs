using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped Elasticsearch endpoints
	/// </summary>
	public partial class ElasticClient : IElasticClient, IHighLevelToLowLevelDispatcher
	{
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

			Transport = transport;
			LowLevel = new ElasticLowLevelClient(Transport);
			LowLevelDispatch = new LowLevelDispatch(LowLevel);
		}

		public IConnectionSettingsValues ConnectionSettings => Transport.Settings;
		public Inferrer Infer => Transport.Settings.Inferrer;

		public IElasticLowLevelClient LowLevel { get; }

		public IElasticsearchSerializer Serializer => Transport.Settings.Serializer;
		private IHighLevelToLowLevelDispatcher Dispatcher => this;

		private LowLevelDispatch LowLevelDispatch { get; }

		private ITransport<IConnectionSettingsValues> Transport { get; }

		TResponse IHighLevelToLowLevelDispatcher.Dispatch<TRequest, TQueryString, TResponse>(
			TRequest request,
			Func<TRequest, PostData<object>,
				ElasticsearchResponse<TResponse>> dispatch
		) => Dispatcher.Dispatch<TRequest, TQueryString, TResponse>(request, null, dispatch);

		TResponse IHighLevelToLowLevelDispatcher.Dispatch<TRequest, TQueryString, TResponse>(
			TRequest request, Func<IApiCallDetails, Stream, TResponse> responseGenerator,
			Func<TRequest, PostData<object>, ElasticsearchResponse<TResponse>> dispatch
		)
		{
			request.RouteValues.Resolve(ConnectionSettings);
			request.RequestParameters.DeserializationOverride(responseGenerator);

			var response = dispatch(request, request);
			return ResultsSelector(response);
		}

		Task<TResponseInterface> IHighLevelToLowLevelDispatcher.DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest descriptor,
			CancellationToken cancellationToken,
			Func<TRequest, PostData<object>, CancellationToken, Task<ElasticsearchResponse<TResponse>>> dispatch
		) => Dispatcher.DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(descriptor, cancellationToken, null, dispatch);

		async Task<TResponseInterface> IHighLevelToLowLevelDispatcher.DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest request,
			CancellationToken cancellationToken,
			Func<IApiCallDetails, Stream, TResponse> responseGenerator,
			Func<TRequest, PostData<object>, CancellationToken, Task<ElasticsearchResponse<TResponse>>> dispatch
		)
		{
			request.RouteValues.Resolve(ConnectionSettings);
			request.RequestParameters.DeserializationOverride(responseGenerator);
			var response = await dispatch(request, request, cancellationToken).ConfigureAwait(false);
			return ResultsSelector(response);
		}

		private static TResponse ResultsSelector<TResponse>(ElasticsearchResponse<TResponse> c)
			where TResponse : ResponseBase =>
			c.Body ?? CreateInvalidInstance<TResponse>(c);

		private static TResponse CreateInvalidInstance<TResponse>(IApiCallDetails response)
			where TResponse : ResponseBase
		{
			var r = typeof(TResponse).CreateInstance<TResponse>();
			((IBodyWithApiCallDetails)r).ApiCall = response;
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
