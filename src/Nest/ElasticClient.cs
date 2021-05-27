// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	/// <inheritdoc />
	public partial class ElasticClient : IElasticClient
	{
		private readonly ITransport<IConnectionSettingsValues> _transport;

		/// <summary>
		/// Creates a client configured to connect to localhost:9200.
		/// </summary>
		public ElasticClient() : this(new ConnectionSettings(new Uri("http://localhost:9200"))) { }

		/// <summary>
		/// Creates a client configured to connect to a node reachable at the provided <paramref name="uri"/>.
		/// </summary>
		/// <param name="uri">The <see cref="Uri"/> to connect to.</param>
		public ElasticClient(Uri uri) : this(new ConnectionSettings(uri)) { }

		/// <summary>
		/// Creates a client configured to communicate with Elastic Cloud using the provided <paramref name="cloudId"/>.
		/// <para>See the <see cref="CloudConnectionPool"/> documentation for more information on how to obtain your Cloud Id.</para>
		/// <para>If you want more control, use the <see cref="ElasticClient(IConnectionSettingsValues)"/> constructor and pass an instance of
		/// <see cref="ConnectionSettings" /> that takes a <paramref name="cloudId"/> in its constructor as well.</para>
		/// </summary>
		/// <param name="cloudId">The Cloud ID of an Elastic Cloud deployment.</param>
		/// <param name="credentials">The credentials to use for the connection.</param>
		public ElasticClient(string cloudId, IAuthenticationHeader credentials) : this(new ConnectionSettings(cloudId, credentials)) { }

		/// <summary>
		/// TODO
		/// </summary>
		/// <param name="connectionSettings"></param>
		public ElasticClient(IConnectionSettingsValues connectionSettings)
			: this(new Transport<IConnectionSettingsValues>(connectionSettings)) { }

		/// <summary>
		/// TODO
		/// </summary>
		/// <param name="transport"></param>
		public ElasticClient(ITransport<IConnectionSettingsValues> transport)
		{
			transport.ThrowIfNull(nameof(transport));
			transport.Settings.ThrowIfNull(nameof(transport.Settings));
			transport.Settings.RequestResponseSerializer.ThrowIfNull(nameof(transport.Settings.RequestResponseSerializer));
			transport.Settings.Inferrer.ThrowIfNull(nameof(transport.Settings.Inferrer));

			_transport = transport;
			
			SetupNamespaces();
		}

		private partial void SetupNamespaces();

		public IConnectionSettingsValues ConnectionSettings => _transport.Settings;
		public Inferrer Infer => _transport.Settings.Inferrer;
		public ITransportSerializer RequestResponseSerializer => _transport.Settings.RequestResponseSerializer;
		public ITransportSerializer SourceSerializer => _transport.Settings.SourceSerializer;

		internal TResponse DoRequest<TRequest, TResponse>(
			TRequest request,
			IRequestParameters? parameters,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new()
		{
			var (url, postData) = PrepareRequest(request, forceConfiguration);
			return _transport.Request<TResponse>(request.HttpMethod, url, postData, parameters);
		}

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest request,
			IRequestParameters? parameters,
			CancellationToken cancellationToken = default,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new()
		{
			var (url, postData) = PrepareRequest(request, forceConfiguration);
			return _transport.RequestAsync<TResponse>(request.HttpMethod, url, cancellationToken, postData, parameters);
		}

		private (string url, PostData data) PrepareRequest<TRequest>(TRequest request, Action<IRequestConfiguration>? forceConfiguration)
			where TRequest : class, IRequest
		{
			request.ThrowIfNull(nameof(request), "A request is required.");

			if (forceConfiguration is not null)
				ForceConfiguration(request, forceConfiguration);
			if (request.ContentType is not null)
				ForceContentType(request, request.ContentType);

			var url = request.GetUrl(ConnectionSettings);

			// TODO: Left while we decide if we prefer this
			//PostData postData = null;
			//if (request is IProxyRequest proxyRequest)
			//{
			//	postData = PostData.ProxySerializable((stream, formatting) =>
			//		proxyRequest.WriteJson(stream, ConnectionSettings.SourceSerializer, formatting));
			//}

			var postData =
				(request.CanBeEmpty && request.IsEmpty) || request.HttpMethod == HttpMethod.GET ||
				request.HttpMethod == HttpMethod.HEAD || !request.SupportsBody
					? null
					: PostData.Serializable(request);

			return (url, postData);
		}

		private static void ForceConfiguration(IRequest request, Action<IRequestConfiguration> forceConfiguration)
		{
			var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
			forceConfiguration(configuration);
			request.RequestParameters.RequestConfiguration = configuration;
		}

		private static void ForceContentType<TRequest>(TRequest request, string contentType) where TRequest : class, IRequest
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
