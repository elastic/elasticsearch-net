// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	public class NamespacedClientProxy
	{
		private readonly ElasticClient _client;

		protected NamespacedClientProxy(ElasticClient client) => _client = client;

		internal TResponse DoRequest<TRequest, TResponse>(
			TRequest p,
			IRequestParameters parameters,
			Action<IRequestConfiguration> forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
			_client.DoRequest<TRequest, TResponse>(p, parameters, forceConfiguration);

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest p,
			IRequestParameters parameters,
			CancellationToken ct,
			Action<IRequestConfiguration> forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
			_client.DoRequestAsync<TRequest, TResponse>(p, parameters, ct, forceConfiguration);
	}
	
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped Elasticsearch endpoints
	/// </summary>
	public partial class ElasticClient : IElasticClient
	{
		public ElasticClient() : this(new ConnectionSettings(new Uri("http://localhost:9200"))) { }

		public ElasticClient(Uri uri) : this(new ConnectionSettings(uri)) { }

		/// <summary>
		/// Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId"/>,
		/// <para><see cref="CloudConnectionPool"/> documentation for more information on how to obtain your Cloud Id</para>
		/// <para></para>If you want more control use the <see cref="ElasticClient(IConnectionSettingsValues)"/> constructor and pass an instance of
		/// <see cref="ConnectionSettings" /> that takes <paramref name="cloudId"/> in its constructor as well
		/// </summary>
		public ElasticClient(string cloudId, IAuthenticationHeader credentials) : this(new ConnectionSettings(cloudId, credentials)) { }

		public ElasticClient(IConnectionSettingsValues connectionSettings)
			: this(new Transport<IConnectionSettingsValues>(connectionSettings ?? new ConnectionSettings())) { }

		public ElasticClient(ITransport<IConnectionSettingsValues> transport)
		{
			transport.ThrowIfNull(nameof(transport));
			transport.Settings.ThrowIfNull(nameof(transport.Settings));
			transport.Settings.RequestResponseSerializer.ThrowIfNull(nameof(transport.Settings.RequestResponseSerializer));
			transport.Settings.Inferrer.ThrowIfNull(nameof(transport.Settings.Inferrer));

			Transport = transport;
			
			SetupNamespaces();
		}

		partial void SetupNamespaces();

		public IConnectionSettingsValues ConnectionSettings => Transport.Settings;
		public Inferrer Infer => Transport.Settings.Inferrer;
		public ITransportSerializer RequestResponseSerializer => Transport.Settings.RequestResponseSerializer;
		public ITransportSerializer SourceSerializer => Transport.Settings.SourceSerializer;
		private ITransport<IConnectionSettingsValues> Transport { get; }

		internal TResponse DoRequest<TRequest, TResponse>(TRequest p, IRequestParameters parameters, Action<IRequestConfiguration> forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new()
		{
			if (forceConfiguration != null) ForceConfiguration(p, forceConfiguration);
			if (p.ContentType != null) ForceContentType(p, p.ContentType);

			var url = p.GetUrl(ConnectionSettings);
			var b = (p.HttpMethod == HttpMethod.GET || p.HttpMethod == HttpMethod.HEAD || !p.SupportsBody) ? null : PostData.Serializable(p);
			
			return Transport.Request<TResponse>(p.HttpMethod, url, b, parameters);
		}

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest p,
			IRequestParameters parameters,
			CancellationToken ct,
			Action<IRequestConfiguration> forceConfiguration = null
		)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new()
		{
			if (forceConfiguration != null) ForceConfiguration(p, forceConfiguration);
			if (p.ContentType != null) ForceContentType(p, p.ContentType);

			var url = p.GetUrl(ConnectionSettings);
			var b = (p.HttpMethod == HttpMethod.GET || p.HttpMethod == HttpMethod.HEAD || !p.SupportsBody) ? null : PostData.Serializable<TRequest>(p);

			return Transport.RequestAsync<TResponse>(p.HttpMethod, url, ct, b, parameters);
		}

		private static void ForceConfiguration(IRequest request, Action<IRequestConfiguration> forceConfiguration)
		{
			if (forceConfiguration == null) return;

			var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
			forceConfiguration(configuration);
			request.RequestParameters.RequestConfiguration = configuration;
		}
		private void ForceContentType<TRequest>(TRequest request, string contentType) where TRequest : class, IRequest
		{
			var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
			configuration.Accept = contentType;
			configuration.ContentType = contentType;
			request.RequestParameters.RequestConfiguration = configuration;
		}

		internal static void ForceJson(IRequestConfiguration requestConfiguration)
		{
			requestConfiguration.Accept = RequestData.MimeType;
			requestConfiguration.ContentType = RequestData.MimeType;
		}
		internal static void ForceTextPlain(IRequestConfiguration requestConfiguration)
		{
			requestConfiguration.Accept = RequestData.MimeTypeTextPlain;
			requestConfiguration.ContentType = RequestData.MimeTypeTextPlain;
		}
	}
}
