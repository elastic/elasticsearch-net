// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Transport;
using Elastic.Transport.Diagnostics;
using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// A strongly-typed client for communicating with Elasticsearch server endpoints.
/// </summary>
public partial class ElasticsearchClient
{
	private const string OpenTelemetrySpanAttributePrefix = "db.elasticsearch.";
	// This should be updated if any of the code uses semantic conventions defined in newer schema versions.
	private const string OpenTelemetrySchemaVersion = "https://opentelemetry.io/schemas/1.21.0";

	private readonly ITransport<IElasticsearchClientSettings> _transport;
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
		: this(new DistributedTransport<IElasticsearchClientSettings>(elasticsearchClientSettings))
	{
	}

	internal ElasticsearchClient(ITransport<IElasticsearchClientSettings> transport)
	{
		transport.ThrowIfNull(nameof(transport));
		transport.Configuration.ThrowIfNull(nameof(transport.Configuration));
		transport.Configuration.RequestResponseSerializer.ThrowIfNull(
			nameof(transport.Configuration.RequestResponseSerializer));
		transport.Configuration.Inferrer.ThrowIfNull(nameof(transport.Configuration.Inferrer));

		_transport = transport;

		SetupNamespaces();
	}

	public IElasticsearchClientSettings ElasticsearchClientSettings => _transport.Configuration;
	public Inferrer Infer => _transport.Configuration.Inferrer;
	public Serializer RequestResponseSerializer => _transport.Configuration.RequestResponseSerializer;
	public Serializer SourceSerializer => _transport.Configuration.SourceSerializer;
	public ITransport<IElasticsearchClientSettings> Transport => _transport;

	private ProductCheckStatus _productCheckStatus;

	private enum ProductCheckStatus
	{
		NotChecked,
		Succeeded,
		Failed
	}

	private partial void SetupNamespaces();

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(TRequest request)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new() =>
			DoRequest<TRequest, TResponse, TRequestParameters>(request, null);

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		Action<IRequestConfiguration>? forceConfiguration)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
			=> DoRequestCoreAsync<TRequest, TResponse, TRequestParameters>(false, request, forceConfiguration).EnsureCompleted();

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
			=> DoRequestAsync<TRequest, TResponse, TRequestParameters>(request, null, cancellationToken);

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		Action<IRequestConfiguration>? forceConfiguration,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
			=> DoRequestCoreAsync<TRequest, TResponse, TRequestParameters>(true, request, forceConfiguration, cancellationToken).AsTask();

	private ValueTask<TResponse> DoRequestCoreAsync<TRequest, TResponse, TRequestParameters>(
		bool isAsync,
		TRequest request,
		Action<IRequestConfiguration>? forceConfiguration,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (_productCheckStatus == ProductCheckStatus.Failed)
			throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

		var (requestModified, hadRequestConfig, originalHeaders) = AttachProductCheckHeaderIfRequired<TRequest, TRequestParameters>(request);
		var (resolvedUrl, urlTemplate, resolvedRouteValues, postData) = PrepareRequest<TRequest, TRequestParameters>(request, forceConfiguration);
		var openTelemetryData = PrepareOpenTelemetryData<TRequest, TRequestParameters>(request, resolvedRouteValues);

		if (_productCheckStatus == ProductCheckStatus.Succeeded && !requestModified)
		{
			if (isAsync)
				return new ValueTask<TResponse>(_transport.RequestAsync<TResponse>(request.HttpMethod, resolvedUrl, postData, request.RequestParameters, in openTelemetryData, cancellationToken));
			else
				return new ValueTask<TResponse>(_transport.Request<TResponse>(request.HttpMethod, resolvedUrl, postData, request.RequestParameters, in openTelemetryData));
		}

		return SendRequest(isAsync);

		async ValueTask<TResponse> SendRequest(bool isAsync)
		{
			TResponse response;

			if (isAsync)
				response = await _transport.RequestAsync<TResponse>(request.HttpMethod, resolvedUrl, postData, request.RequestParameters, in openTelemetryData, cancellationToken).ConfigureAwait(false);
			else
				response = _transport.Request<TResponse>(request.HttpMethod, resolvedUrl, postData, request.RequestParameters, in openTelemetryData);

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

	private static OpenTelemetryData PrepareOpenTelemetryData<TRequest, TRequestParameters>(TRequest request, Dictionary<string, string> resolvedRouteValues)
		where TRequest : Request<TRequestParameters>
		where TRequestParameters : RequestParameters, new()
	{
		// If there are no subscribed listeners, we avoid some work and allocations
		if (!Elastic.Transport.Diagnostics.OpenTelemetry.ElasticTransportActivitySourceHasListeners)
			return default;

		// We fall back to a general operation name in cases where the derived request fails to override the property
		var operationName = !string.IsNullOrEmpty(request.OperationName) ? request.OperationName : request.HttpMethod.GetStringValue();

		// TODO: Optimisation: We should consider caching these, either for cases where resolvedRouteValues is null, or
		// caching per combination of route values.
		// We should benchmark this first to assess the impact for common workloads.
		// The former is likely going to save some short-lived allocations, but only for requests to endpoints without required path parts.
		// The latter may bloat the cache as some combinations of path parts may rarely re-occur.
		var attributes = new Dictionary<string, object>
		{
			[OpenTelemetry.SemanticConventions.DbOperation] = !string.IsNullOrEmpty(request.OperationName) ? request.OperationName : "unknown",
			[$"{OpenTelemetrySpanAttributePrefix}schema_url"] = OpenTelemetrySchemaVersion
		};

		if (resolvedRouteValues is not null)
		{
			foreach (var value in resolvedRouteValues)
			{
				if (!string.IsNullOrEmpty(value.Key) && !string.IsNullOrEmpty(value.Value))
					attributes.Add($"{OpenTelemetrySpanAttributePrefix}path_parts.{value.Key}", value.Value);
			}
		}

		var openTelemetryData = new OpenTelemetryData { SpanName = operationName, SpanAttributes = attributes };
		return openTelemetryData;
	}

	private (bool requestModified, bool hadRequestConfig, HeadersList? originalHeaders) AttachProductCheckHeaderIfRequired<TRequest, TRequestParameters>(TRequest request)
		where TRequest : Request<TRequestParameters>
		where TRequestParameters : RequestParameters, new()
	{
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

		return (requestModified, hadRequestConfig, originalHeaders);
	}

	private (string resolvedUrl, string urlTemplate, Dictionary<string, string>? resolvedRouteValues, PostData data) PrepareRequest<TRequest, TRequestParameters>(TRequest request,
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

		var (resolvedUrl, urlTemplate, routeValues) = request.GetUrl(ElasticsearchClientSettings);

		var postData =
			request.HttpMethod == HttpMethod.GET ||
			request.HttpMethod == HttpMethod.HEAD || !request.SupportsBody
				? null
				: PostData.Serializable(request);

		return (resolvedUrl, urlTemplate, routeValues, postData);
	}

		private void PostRequestProductCheck<TRequest, TResponse>(TRequest request, TResponse response)
		where TRequest : Request
		where TResponse : TransportResponse, new()
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
