// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

/// <inheritdoc />
public partial class ElasticClient
{
	private readonly ITransport<IElasticsearchClientSettings> _transport;

	/// <summary>
	///     Creates a client configured to connect to localhost:9200.
	/// </summary>
	public ElasticClient() : this(new ElasticsearchClientSettings(new Uri("http://localhost:9200"))) { }

	/// <summary>
	///     Creates a client configured to connect to a node reachable at the provided <paramref name="uri" />.
	/// </summary>
	/// <param name="uri">The <see cref="Uri" /> to connect to.</param>
	public ElasticClient(Uri uri) : this(new ElasticsearchClientSettings(uri)) { }

	/// <summary>
	///     Creates a client configured to communicate with Elastic Cloud using the provided <paramref name="cloudId" />.
	///     <para>See the <see cref="CloudConnectionPool" /> documentation for more information on how to obtain your Cloud Id.</para>
	///     <para>
	///         If you want more control, use the <see cref="ElasticClient(IElasticsearchClientSettings)" /> constructor and
	///         pass
	///         an instance of
	///         <see cref="ElasticsearchClientSettings" /> that takes a <paramref name="cloudId" /> in its constructor as well.
	///     </para>
	/// </summary>
	/// <param name="cloudId">The Cloud ID of an Elastic Cloud deployment.</param>
	/// <param name="credentials">The credentials to use for the connection.</param>
	public ElasticClient(string cloudId, IAuthenticationHeader credentials) : this(
		new ElasticsearchClientSettings(cloudId, credentials))
	{
	}

	/// <summary>
	///     TODO
	/// </summary>
	/// <param name="elasticsearchClientSettings"></param>
	public ElasticClient(IElasticsearchClientSettings elasticsearchClientSettings)
		: this(new Transport<IElasticsearchClientSettings>(elasticsearchClientSettings))
	{
	}

	/// <summary>
	///     TODO
	/// </summary>
	/// <param name="transport"></param>
	public ElasticClient(ITransport<IElasticsearchClientSettings> transport)
	{
		transport.ThrowIfNull(nameof(transport));
		transport.Settings.ThrowIfNull(nameof(transport.Settings));
		transport.Settings.RequestResponseSerializer.ThrowIfNull(
			nameof(transport.Settings.RequestResponseSerializer));
		transport.Settings.Inferrer.ThrowIfNull(nameof(transport.Settings.Inferrer));

		_transport = transport;

		SetupNamespaces();
	}

	public IElasticsearchClientSettings ElasticsearchClientSettings => _transport.Settings;
	public Inferrer Infer => _transport.Settings.Inferrer;
	public Serializer RequestResponseSerializer => _transport.Settings.RequestResponseSerializer;
	public Serializer SourceSerializer => _transport.Settings.SourceSerializer;

	private ProductCheckStatus _productCheckStatus;

	private enum ProductCheckStatus
	{
		NotChecked,
		Succeeded,
		Failed
	}

	private partial void SetupNamespaces();

	internal TResponse DoRequest<TRequest, TResponse>(
		TRequest request,
		IRequestParameters? parameters,
		Action<IRequestConfiguration>? forceConfiguration = null)
		where TRequest : class, IRequest
		where TResponse : class, ITransportResponse, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var (url, postData) = PrepareRequest(request, forceConfiguration);
		var response = _transport.Request<TResponse>(request.HttpMethod, url, postData, parameters);
		PostRequestProductCheck<TRequest, TResponse>(request, response);
		return response;
	}	

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
		TRequest request,
		IRequestParameters? parameters,
		CancellationToken cancellationToken = default)
		where TRequest : class, IRequest
		where TResponse : class, ITransportResponse, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var (url, postData) = PrepareRequest(request, null);

		if (_productCheckStatus == ProductCheckStatus.Succeeded)
			return _transport.RequestAsync<TResponse>(request.HttpMethod, url, postData, parameters, cancellationToken);

		return SendRequest(request, parameters, url, postData);

		async Task<TResponse> SendRequest(TRequest request, IRequestParameters? parameters, string url, PostData postData)
		{
			var response = await _transport.RequestAsync<TResponse>(request.HttpMethod, url, postData, parameters).ConfigureAwait(false);
			PostRequestProductCheck<TRequest, TResponse>(request, response);
			return response;
		}
	}

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
		TRequest request,
		IRequestParameters? parameters,
		Action<IRequestConfiguration>? forceConfiguration = null,
		CancellationToken cancellationToken = default)
		where TRequest : class, IRequest
		where TResponse : class, ITransportResponse, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var (url, postData) = PrepareRequest(request, forceConfiguration);

		if (_productCheckStatus == ProductCheckStatus.Succeeded)
			return _transport.RequestAsync<TResponse>(request.HttpMethod, url, postData, parameters, cancellationToken);

		return SendRequest(request, parameters, url, postData);

		async Task<TResponse> SendRequest(TRequest request, IRequestParameters? parameters, string url, PostData postData)
		{
			var response = await _transport.RequestAsync<TResponse>(request.HttpMethod, url, postData, parameters).ConfigureAwait(false);
			PostRequestProductCheck<TRequest, TResponse>(request, response);
			return response;
		}
	}

	private (string url, PostData data) PrepareRequest<TRequest>(TRequest request,
		Action<IRequestConfiguration>? forceConfiguration)
		where TRequest : class, IRequest
	{
		request.ThrowIfNull(nameof(request), "A request is required.");

		// If we have not yet checked the product name, add the product header to the list of headers to parse.
		if (_productCheckStatus == ProductCheckStatus.NotChecked)
			ElasticsearchClientSettings.ResponseHeadersToParse.TryAdd("x-elastic-product");

		if (forceConfiguration is not null)
			ForceConfiguration(request, forceConfiguration);

		if (request.ContentType is not null)
			ForceContentType(request, request.ContentType);

		var url = request.GetUrl(ElasticsearchClientSettings);

		// TODO: Left while we decide if we prefer this
		//PostData postData = null;
		//if (request is IProxyRequest proxyRequest)
		//{
		//	postData = PostData.ProxySerializable((stream, formatting) =>
		//		proxyRequest.WriteJson(stream, ConnectionSettings.SourceSerializer, formatting));
		//}

		var postData =
			/*request.CanBeEmpty && request.IsEmpty || */request.HttpMethod == HttpMethod.GET ||
			request.HttpMethod == HttpMethod.HEAD || !request.SupportsBody
				? null
				: PostData.Serializable(request);

		return (url, postData);
	}

	private void PostRequestProductCheck<TRequest, TResponse>(TRequest request, TResponse response)
		where TRequest : class, IRequest
		where TResponse : class, ITransportResponse, new()
	{
		if (_productCheckStatus == ProductCheckStatus.NotChecked)
		{
			if (!response.ApiCall.ParsedHeaders.TryGetValue("x-elastic-product", out var values) || values.Single().Equals("elasticsearch", StringComparison.Ordinal))
			{
				_productCheckStatus = ProductCheckStatus.Failed;
				throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);
			}

			// Once validated, remove the header so that subsequent requests do not need to attempt to parse it.
			ElasticsearchClientSettings.ResponseHeadersToParse.Remove("x-elastic-product");

			_productCheckStatus = ProductCheckStatus.Succeeded;
		}
	}

	private static void ForceConfiguration(IRequest request, Action<IRequestConfiguration> forceConfiguration)
	{
		var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
		forceConfiguration(configuration);
		request.RequestParameters.RequestConfiguration = configuration;
	}

	private static void ForceContentType<TRequest>(TRequest request, string contentType)
		where TRequest : class, IRequest
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
