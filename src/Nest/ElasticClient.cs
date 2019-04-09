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
	public partial class ElasticClient : IElasticClient
	{
		public ElasticClient() : this(new ConnectionSettings(new Uri("http://localhost:9200"))) { }

		public ElasticClient(Uri uri) : this(new ConnectionSettings(uri)) { }

		public ElasticClient(IConnectionSettingsValues connectionSettings)
			: this(new Transport<IConnectionSettingsValues>(connectionSettings ?? new ConnectionSettings())) { }

		public ElasticClient(ITransport<IConnectionSettingsValues> transport)
		{
			transport.ThrowIfNull(nameof(transport));
			transport.Settings.ThrowIfNull(nameof(transport.Settings));
			transport.Settings.RequestResponseSerializer.ThrowIfNull(nameof(transport.Settings.RequestResponseSerializer));
			transport.Settings.Inferrer.ThrowIfNull(nameof(transport.Settings.Inferrer));

			Transport = transport;
			LowLevel = new ElasticLowLevelClient(Transport);
			LowLevelDispatch = new LowLevelDispatch(LowLevel);
		}

		public IConnectionSettingsValues ConnectionSettings => Transport.Settings;
		public Inferrer Infer => Transport.Settings.Inferrer;

		public IElasticLowLevelClient LowLevel { get; }
		public IElasticsearchSerializer RequestResponseSerializer => Transport.Settings.RequestResponseSerializer;

		public IElasticsearchSerializer SourceSerializer => Transport.Settings.SourceSerializer;

		private LowLevelDispatch LowLevelDispatch { get; }

		private ITransport<IConnectionSettingsValues> Transport { get; }

		internal Task<TResponseInterface> Dispatch2Async<TRequest, TResponseInterface, TResponse>(
			TRequest p,
			IRequestParameters parameters,
			CancellationToken ct
		)
			where TRequest : class, IRequest
			where TResponseInterface : IElasticsearchResponse
			where TResponse : class, TResponseInterface, IElasticsearchResponse, new()
		{
			p.RouteValues.Resolve(ConnectionSettings);
			var b = (p.HttpMethod == HttpMethod.GET || p.HttpMethod == HttpMethod.HEAD) ? null : new SerializableData<TRequest>(p);

			return LowLevel.DoRequestAsync<TResponse>(p.HttpMethod, p.RouteValues.ToString(), ct, b, parameters)
				.ToBaseTask<TResponse, TResponseInterface>();
		}

		internal TResponse Dispatch2<TRequest, TResponse>(TRequest p, IRequestParameters parameters)
			where TRequest : class, IRequest
			where TResponse : class, IElasticsearchResponse, new()

		{
			p.RouteValues.Resolve(ConnectionSettings);
			var b = (p.HttpMethod == HttpMethod.GET || p.HttpMethod == HttpMethod.HEAD) ? null : new SerializableData<TRequest>(p);
			return LowLevel.DoRequest<TResponse>(p.HttpMethod, p.RouteValues.ToString(), b, parameters);
		}

		private static void ForceConfiguration<TParams>(IRequest<TParams> request, Action<IRequestConfiguration> setter)
			where TParams : IRequestParameters, new()
		{
			var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
			setter(configuration);
			request.RequestParameters.RequestConfiguration = configuration;
		}

		private static TRequest ForceConfiguration<TRequest, TParams>(TRequest request, Action<IRequestConfiguration> setter)
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
