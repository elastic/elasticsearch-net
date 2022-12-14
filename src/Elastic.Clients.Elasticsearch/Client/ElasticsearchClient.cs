// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// A strongly-typed client for communicating with Elasticsearch server endpoints.
/// </summary>
public partial class ElasticsearchClient
{
	private readonly HttpTransport<IElasticsearchClientSettings> _transport;

	private readonly ActivitySource _activitySource = new("Elastic.Clients.Elasticsearch.ElasticsearchClient");

	internal static ConditionalWeakTable<JsonSerializerOptions, IElasticsearchClientSettings> SettingsTable { get; } = new();

	/// <summary>
	/// Creates a client configured to connect to http://localhost:9200.
	/// </summary>
	public ElasticsearchClient() : this(new ElasticsearchClientSettings(new Uri("http://localhost:9200"))) { }

	/// <summary>
	/// Creates a client configured to connect to a node reachable at the provided <paramref name="uri" />.
	/// </summary>
	/// <param name="uri">The <see cref="Uri" /> to connect to.</param>
	public ElasticsearchClient(Uri uri) : this(new ElasticsearchClientSettings(uri)) { }

	/// <summary>
	/// Creates a client configured to communicate with Elastic Cloud using the provided <paramref name="cloudId" />.
	/// <para>See the <see cref="CloudNodePool" /> documentation for more information on how to obtain your Cloud Id.</para>
	///   <para>
	///     If you want more control, use the <see cref="ElasticsearchClient(IElasticsearchClientSettings)" /> constructor and
	///     pass an instance of <see cref="ElasticsearchClientSettings" /> that takes a <paramref name="cloudId" /> in its constructor as well.
	///   </para>
	/// </summary>
	/// <param name="cloudId">The Cloud ID of an Elastic Cloud deployment.</param>
	/// <param name="credentials">The credentials to use for the connection.</param>
	public ElasticsearchClient(string cloudId, AuthorizationHeader credentials) : this(
		new ElasticsearchClientSettings(cloudId, credentials))
	{
	}

	/// <summary>
	/// Creates a client using the provided configuration to initialise the client.
	/// </summary>
	/// <param name="elasticsearchClientSettings">The <see cref="IElasticsearchClientSettings"/> used to configure the client.</param>
	public ElasticsearchClient(IElasticsearchClientSettings elasticsearchClientSettings)
		: this(new DefaultHttpTransport<IElasticsearchClientSettings>(elasticsearchClientSettings))
	{
	}

	internal ElasticsearchClient(HttpTransport<IElasticsearchClientSettings> transport)
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
	public HttpTransport Transport => _transport;

	private ProductCheckStatus _productCheckStatus;

	private enum ProductCheckStatus
	{
		NotChecked,
		Succeeded,
		Failed
	}

	private partial void SetupNamespaces();

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		TRequestParameters? parameters,
		Action<IRequestConfiguration>? forceConfiguration = null)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var requestModified = false;
		var hadRequestConfig = false;
		HeadersList? originalHeaders = null;

		// If we have not yet checked the product name, add the product header to the list of headers to parse.
		if (_productCheckStatus == ProductCheckStatus.NotChecked)
		{
			requestModified = true;
			if (request.RequestParameters.RequestConfiguration is null)
			{
				request.RequestParameters.RequestConfiguration = new RequestConfiguration();
			}
			else
			{
				originalHeaders = request.RequestParameters.RequestConfiguration.ResponseHeadersToParse;
				hadRequestConfig = true;
			}

			if (request.RequestParameters.RequestConfiguration.ResponseHeadersToParse.Count == 0)
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList("x-elastic-product");
			}
			else
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList(request.RequestParameters.RequestConfiguration.ResponseHeadersToParse, "x-elastic-product");
			}
		}

		var (resolvedUrl, urlTemplate, postData) = PrepareRequest<TRequest, TRequestParameters>(request, forceConfiguration);

		TResponse response;

		using (var activity = _activitySource.StartActivity($"Elasticsearch: {request.HttpMethod} {urlTemplate}", ActivityKind.Client))
		{
			activity?.AddTag("db.system", "elasticsearch");
			response = _transport.Request<TResponse>(request.HttpMethod, resolvedUrl, postData, parameters);
		}
		
		PostRequestProductCheck<TRequest, TResponse>(request, response);

		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		if (requestModified)
		{
			if (!hadRequestConfig)
			{
				request.RequestParameters.RequestConfiguration = null;
			}
			else if (originalHeaders.HasValue && originalHeaders.Value.Count > 0)
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = originalHeaders.Value;
			}
		}

		return response;
	}

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		Action<IRequestConfiguration>? forceConfiguration = null)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var requestModified = false;
		var hadRequestConfig = false;
		HeadersList? originalHeaders = null;

		// If we have not yet checked the product name, add the product header to the list of headers to parse.
		if (_productCheckStatus == ProductCheckStatus.NotChecked)
		{
			requestModified = true;
			if (request.RequestParameters.RequestConfiguration is null)
			{
				request.RequestParameters.RequestConfiguration = new RequestConfiguration();
			}
			else
			{
				originalHeaders = request.RequestParameters.RequestConfiguration.ResponseHeadersToParse;
				hadRequestConfig = true;
			}

			if (request.RequestParameters.RequestConfiguration.ResponseHeadersToParse.Count == 0)
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList("x-elastic-product");
			}
			else
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList(request.RequestParameters.RequestConfiguration.ResponseHeadersToParse, "x-elastic-product");
			}
		}

		var (resolvedUrl, urlTemplate, postData) = PrepareRequest<TRequest, TRequestParameters>(request, forceConfiguration);

		TResponse response;

		using (var activity = _activitySource.StartActivity($"Elasticsearch: {request.HttpMethod} {urlTemplate}", ActivityKind.Client))
		{
			activity?.AddTag("db.system", "elasticsearch");
			activity?.SetCustomProperty("elastic.transport.client", true);

			response = _transport.Request<TResponse>(request.HttpMethod, resolvedUrl, postData, request.RequestParameters);

			if (response.ApiCallDetails.RequestBodyInBytes is not null)
				activity?.AddTag("db.statement", System.Text.Encoding.UTF8.GetString(response.ApiCallDetails.RequestBodyInBytes));
		}

		PostRequestProductCheck<TRequest, TResponse>(request, response);

		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		if (requestModified)
		{
			if (!hadRequestConfig)
			{
				request.RequestParameters.RequestConfiguration = null;
			}
			else if (originalHeaders.HasValue && originalHeaders.Value.Count > 0)
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = originalHeaders.Value;
			}
		}

		return response;
	}

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		RequestParameters? parameters,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var requestModified = false;
		var hadRequestConfig = false;
		HeadersList? originalHeaders = null;

		// If we have not yet checked the product name, add the product header to the list of headers to parse.
		if (_productCheckStatus == ProductCheckStatus.NotChecked)
		{
			requestModified = true;

			if (request.RequestParameters.RequestConfiguration is null)
			{
				request.RequestParameters.RequestConfiguration = new RequestConfiguration();
			}
			else
			{
				originalHeaders = request.RequestParameters.RequestConfiguration.ResponseHeadersToParse;
				hadRequestConfig = true;
			}

			if (request.RequestParameters.RequestConfiguration.ResponseHeadersToParse.Count == 0)
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList("x-elastic-product");
			}
			else
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList(request.RequestParameters.RequestConfiguration.ResponseHeadersToParse, "x-elastic-product");
			}
		}

		var (resolvedUrl, urlTemplate, postData) = PrepareRequest<TRequest, TRequestParameters>(request, null);

		if (_productCheckStatus == ProductCheckStatus.Succeeded && !requestModified && !_activitySource.HasListeners())
			return _transport.RequestAsync<TResponse>(request.HttpMethod, resolvedUrl, postData, parameters, cancellationToken);

		return SendRequest(request, parameters, resolvedUrl, postData, hadRequestConfig, originalHeaders);

		async Task<TResponse> SendRequest(TRequest request, RequestParameters? parameters, string url, PostData postData, bool hadRequestConfig, HeadersList? originalHeaders)
		{
			TResponse response;

			using (var activity = _activitySource.StartActivity($"Elasticsearch: {request.HttpMethod} {urlTemplate}", ActivityKind.Client))
			{
				activity?.AddTag("db.system", "elasticsearch");
				activity?.SetCustomProperty("elastic.transport.client", true);

				response = await _transport.RequestAsync<TResponse>(request.HttpMethod, url, postData, parameters).ConfigureAwait(false);

				if (response.ApiCallDetails.RequestBodyInBytes is not null)
					activity?.AddTag("db.statement", System.Text.Encoding.UTF8.GetString(response.ApiCallDetails.RequestBodyInBytes));
			}

			PostRequestProductCheck<TRequest, TResponse>(request, response);

			if (_productCheckStatus == ProductCheckStatus.Failed)
				throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

			if (request.RequestParameters.RequestConfiguration is not null)
			{
				if (!hadRequestConfig)
				{
					request.RequestParameters.RequestConfiguration = null;
				}
				else if (originalHeaders.HasValue && originalHeaders.Value.Count > 0)
				{
					request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = originalHeaders.Value;
				}
			}

			return response;
		}
	}

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var requestModified = false;
		var hadRequestConfig = false;
		HeadersList? originalHeaders = null;

		// If we have not yet checked the product name, add the product header to the list of headers to parse.
		if (_productCheckStatus == ProductCheckStatus.NotChecked)
		{
			requestModified = true;

			if (request.RequestParameters.RequestConfiguration is null)
			{
				request.RequestParameters.RequestConfiguration = new RequestConfiguration();
			}
			else
			{
				originalHeaders = request.RequestParameters.RequestConfiguration.ResponseHeadersToParse;
				hadRequestConfig = true;
			}

			if (request.RequestParameters.RequestConfiguration.ResponseHeadersToParse.Count == 0)
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList("x-elastic-product");
			}
			else
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList(request.RequestParameters.RequestConfiguration.ResponseHeadersToParse, "x-elastic-product");
			}
		}

		var (resolvedUrl, urlTemplate, postData) = PrepareRequest<TRequest, TRequestParameters>(request, null);

		if (_productCheckStatus == ProductCheckStatus.Succeeded && !requestModified && !_activitySource.HasListeners())
			return _transport.RequestAsync<TResponse>(request.HttpMethod, resolvedUrl, postData, request.RequestParameters, cancellationToken);

		return SendRequest(request, request.RequestParameters, resolvedUrl, postData, hadRequestConfig, originalHeaders);

		async Task<TResponse> SendRequest(TRequest request, RequestParameters? parameters, string url, PostData postData, bool hadRequestConfig, HeadersList? originalHeaders)
		{
			TResponse response;

			using (var activity = _activitySource.StartActivity($"Elasticsearch: {request.HttpMethod} {urlTemplate}", ActivityKind.Client))
			{
				activity?.AddTag("db.system", "elasticsearch");				
				activity?.SetCustomProperty("elastic.transport.client", true);

				response = await _transport.RequestAsync<TResponse>(request.HttpMethod, url, postData, parameters).ConfigureAwait(false);

				if (response.ApiCallDetails.RequestBodyInBytes is not null)
					activity?.AddTag("db.statement", System.Text.Encoding.UTF8.GetString(response.ApiCallDetails.RequestBodyInBytes));
			}

			PostRequestProductCheck<TRequest, TResponse>(request, response);

			if (_productCheckStatus == ProductCheckStatus.Failed)
				throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

			if (request.RequestParameters.RequestConfiguration is not null)
			{
				if (!hadRequestConfig)
				{
					request.RequestParameters.RequestConfiguration = null;
				}
				else if (originalHeaders.HasValue && originalHeaders.Value.Count > 0)
				{
					request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = originalHeaders.Value;
				}
			}

			return response;
		}
	}

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		RequestParameters? parameters,
		Action<IRequestConfiguration>? forceConfiguration = null,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var requestModified = false;
		var hadRequestConfig = false;
		HeadersList? originalHeaders = null;

		// If we have not yet checked the product name, add the product header to the list of headers to parse.
		if (_productCheckStatus == ProductCheckStatus.NotChecked)
		{
			requestModified = true;

			if (request.RequestParameters.RequestConfiguration is null)
			{
				request.RequestParameters.RequestConfiguration = new RequestConfiguration();
			}
			else
			{
				originalHeaders = request.RequestParameters.RequestConfiguration.ResponseHeadersToParse;
				hadRequestConfig = true;
			}

			if (request.RequestParameters.RequestConfiguration.ResponseHeadersToParse.Count == 0)
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList("x-elastic-product");
			}
			else
			{
				request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = new HeadersList(request.RequestParameters.RequestConfiguration.ResponseHeadersToParse, "x-elastic-product");
			}
		}

		var (resolvedUrl, urlTemplate, postData) = PrepareRequest<TRequest, TRequestParameters>(request, null);

		if (_productCheckStatus == ProductCheckStatus.Succeeded && !requestModified && !_activitySource.HasListeners())
			return _transport.RequestAsync<TResponse>(request.HttpMethod, resolvedUrl, postData, parameters, cancellationToken);

		return SendRequest(request, parameters, resolvedUrl, postData, hadRequestConfig, originalHeaders);

		async Task<TResponse> SendRequest(TRequest request, RequestParameters? parameters, string url, PostData postData, bool hadRequestConfig, HeadersList? originalHeaders)
		{
			TResponse response;

			using (var activity = _activitySource.StartActivity($"Elasticsearch: {request.HttpMethod} {urlTemplate}", ActivityKind.Client))
			{
				activity?.AddTag("db.system", "elasticsearch");
				activity?.SetCustomProperty("elastic.transport.client", true);

				response = await _transport.RequestAsync<TResponse>(request.HttpMethod, url, postData, parameters).ConfigureAwait(false);

				if (response.ApiCallDetails.RequestBodyInBytes is not null)
					activity?.AddTag("db.statement", System.Text.Encoding.UTF8.GetString(response.ApiCallDetails.RequestBodyInBytes));
			}

			PostRequestProductCheck<TRequest, TResponse>(request, response);

			if (_productCheckStatus == ProductCheckStatus.Failed)
				throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

			if (request.RequestParameters.RequestConfiguration is not null)
			{
				if (!hadRequestConfig)
				{
					request.RequestParameters.RequestConfiguration = null;
				}
				else if (originalHeaders.HasValue && originalHeaders.Value.Count > 0)
				{
					request.RequestParameters.RequestConfiguration.ResponseHeadersToParse = originalHeaders.Value;
				}
			}

			return response;
		}
	}

	private (string resolvedUrl, string urlTemplate, PostData data) PrepareRequest<TRequest, TRequestParameters>(TRequest request,
		Action<IRequestConfiguration>? forceConfiguration)
		where TRequest : Request<TRequestParameters>
		where TRequestParameters : RequestParameters, new()
	{
		request.ThrowIfNull(nameof(request), "A request is required.");

		if (forceConfiguration is not null)
			ForceConfiguration(request, forceConfiguration);

		if (request.ContentType is not null)
			ForceContentType<TRequest, TRequestParameters>(request, request.ContentType);

		if (request.Accept is not null)
			ForceAccept<TRequest, TRequestParameters>(request, request.Accept);

		var (resolvedUrl, urlTemplate) = request.GetUrl(ElasticsearchClientSettings);

		var postData =
			request.HttpMethod == HttpMethod.GET ||
			request.HttpMethod == HttpMethod.HEAD || !request.SupportsBody
				? null
				: PostData.Serializable(request);

		return (resolvedUrl, urlTemplate, postData);
	}

	private void PostRequestProductCheck<TRequest, TResponse>(TRequest request, TResponse response)
		where TRequest : Request
		where TResponse : ElasticsearchResponse, new()
	{
		if (response.ApiCallDetails.HttpStatusCode.HasValue && response.ApiCallDetails.HttpStatusCode.Value >= 200 && response.ApiCallDetails.HttpStatusCode.Value <= 299 && _productCheckStatus == ProductCheckStatus.NotChecked)
		{
			if (!response.ApiCallDetails.TryGetHeader("x-elastic-product", out var values) || !values.Single().Equals("Elasticsearch", StringComparison.Ordinal))
			{
				_productCheckStatus = ProductCheckStatus.Failed;
			}

			_productCheckStatus = ProductCheckStatus.Succeeded;
		}
	}

	private static void ForceConfiguration<TRequestParameters>(Request<TRequestParameters> request, Action<IRequestConfiguration> forceConfiguration)
		where TRequestParameters : RequestParameters, new()
	{
		var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
		forceConfiguration(configuration);
		request.RequestParameters.RequestConfiguration = configuration;
	}

	private static void ForceContentType<TRequest, TRequestParameters>(TRequest request, string contentType)
		where TRequest : Request<TRequestParameters>
		where TRequestParameters : RequestParameters, new()
	{
		var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
		configuration.Accept = contentType;
		configuration.ContentType = contentType;
		request.RequestParameters.RequestConfiguration = configuration;
	}

	private static void ForceAccept<TRequest, TRequestParameters>(TRequest request, string acceptType)
		where TRequest : Request<TRequestParameters>
		where TRequestParameters : RequestParameters, new()
	{
		var configuration = request.RequestParameters.RequestConfiguration ?? new RequestConfiguration();
		configuration.Accept = acceptType;
		request.RequestParameters.RequestConfiguration = configuration;
	}

	internal static void ForceJson(IRequestConfiguration requestConfiguration)
	{
		requestConfiguration.Accept = RequestData.DefaultMimeType;
		requestConfiguration.ContentType = RequestData.DefaultMimeType;
	}

	internal static void ForceTextPlain(IRequestConfiguration requestConfiguration)
	{
		requestConfiguration.Accept = RequestData.MimeTypeTextPlain;
		requestConfiguration.ContentType = RequestData.MimeTypeTextPlain;
	}
}
