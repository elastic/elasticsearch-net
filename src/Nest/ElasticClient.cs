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
		}

		public IConnectionSettingsValues ConnectionSettings => Transport.Settings;
		public Inferrer Infer => Transport.Settings.Inferrer;

		public IElasticLowLevelClient LowLevel { get; }
		public IElasticsearchSerializer RequestResponseSerializer => Transport.Settings.RequestResponseSerializer;

		public IElasticsearchSerializer SourceSerializer => Transport.Settings.SourceSerializer;

		private ITransport<IConnectionSettingsValues> Transport { get; }

		internal TResponse DoRequest<TRequest, TResponse>(TRequest p, IRequestParameters parameters, Action<IRequestConfiguration> forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (forceConfiguration != null) ForceConfiguration(p, forceConfiguration);

			p.RouteValues.Resolve(ConnectionSettings);
			var b = (p.HttpMethod == HttpMethod.GET || p.HttpMethod == HttpMethod.HEAD) ? null : new SerializableData<TRequest>(p);

			return LowLevel.DoRequest<TResponse>(p.HttpMethod, p.RouteValues.ToString(), b, parameters);
		}

		internal Task<TResponseInterface> DoRequestAsync<TRequest, TResponseInterface, TResponse>(
			TRequest p,
			IRequestParameters parameters,
			CancellationToken ct,
			Action<IRequestConfiguration> forceConfiguration = null
		)
			where TRequest : class, IRequest
			where TResponseInterface : IElasticsearchResponse
			where TResponse : class, TResponseInterface, IElasticsearchResponse, new()
		{
			if (forceConfiguration != null) ForceConfiguration(p, forceConfiguration);

			p.RouteValues.Resolve(ConnectionSettings);
			var b = (p.HttpMethod == HttpMethod.GET || p.HttpMethod == HttpMethod.HEAD) ? null : new SerializableData<TRequest>(p);

			return LowLevel.DoRequestAsync<TResponse>(p.HttpMethod, p.RouteValues.ToString(), ct, b, parameters)
				.ToBaseTask<TResponse, TResponseInterface>();
		}

		private static void ForceConfiguration(IRequest request, Action<IRequestConfiguration> forceConfiguration)
		{
			if (forceConfiguration == null) return;
			var configuration = request.RequestParametersInternal.RequestConfiguration ?? new RequestConfiguration();
			forceConfiguration(request.RequestParametersInternal.RequestConfiguration);
			request.RequestParametersInternal.RequestConfiguration = configuration;
		}

		private static readonly int[] AllStatusCodes = { -1 };
		private static void AcceptAllStatusCodesHandler(IRequestConfiguration requestConfiguration) =>
			requestConfiguration.AllowedStatusCodes = AllStatusCodes;

		private static void ForceJson(IRequestConfiguration requestConfiguration)
		{
			requestConfiguration.Accept = RequestData.MimeType;
			requestConfiguration.ContentType = RequestData.MimeType;
		}
	}
}
