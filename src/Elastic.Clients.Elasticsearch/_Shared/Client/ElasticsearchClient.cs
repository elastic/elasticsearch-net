// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using Elastic.Transport;
using Elastic.Transport.Diagnostics;

using Elastic.Clients.Elasticsearch.Requests;

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

	private int _productCheckStatus;

	private enum ProductCheckStatus
	{
		NotChecked = 0,
		InProgress = 1,
		Succeeded = 2,
		Failed = 3
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
		// The product check modifies request parameters and therefore must not be executed concurrently.
		// We use a lockless CAS approach to make sure that only a single product check request is executed at a time.
		// We do not guarantee that the product check is always performed on the first request.

		var productCheckStatus = Interlocked.CompareExchange(
			ref _productCheckStatus,
			(int)ProductCheckStatus.InProgress,
			(int)ProductCheckStatus.NotChecked
		);

		return productCheckStatus switch
		{
			(int)ProductCheckStatus.NotChecked => SendRequestWithProductCheck(),
			(int)ProductCheckStatus.InProgress or
			(int)ProductCheckStatus.Succeeded => SendRequest(),
			(int)ProductCheckStatus.Failed => throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError),
			_ => throw new InvalidOperationException("unreachable")
		};

		ValueTask<TResponse> SendRequest()
		{
			var (endpointPath, resolvedRouteValues, postData) = PrepareRequest<TRequest, TRequestParameters>(request);
			var openTelemetryDataMutator = GetOpenTelemetryDataMutator<TRequest, TRequestParameters>(request, resolvedRouteValues);

			return isAsync
				? new ValueTask<TResponse>(_transport.RequestAsync<TResponse>(endpointPath, postData, openTelemetryDataMutator, request.RequestConfig, cancellationToken))
				: new ValueTask<TResponse>(_transport.Request<TResponse>(endpointPath, postData, openTelemetryDataMutator, request.RequestConfig));
		}

		async ValueTask<TResponse> SendRequestWithProductCheck()
		{
			try
			{
				return await SendRequestWithProductCheckCore().ConfigureAwait(false);
			}
			catch
			{
				// Re-try product check on next request.

				// 32-bit read/write operations are atomic and due to the initial memory barrier, we can be sure that
				// no other thread executes the product check at the same time. Locked access is not required here.
				if (_productCheckStatus is (int)ProductCheckStatus.InProgress)
					_productCheckStatus = (int)ProductCheckStatus.NotChecked;

				throw;
			}
		}

		async ValueTask<TResponse> SendRequestWithProductCheckCore()
		{
			// Attach product check header

			// TODO: The copy constructor should accept null values
			var requestConfig = (request.RequestConfig is null)
				? new RequestConfiguration()
				{
					ResponseHeadersToParse = new HeadersList("x-elastic-product")
				}
				: new RequestConfiguration(request.RequestConfig)
				{
					ResponseHeadersToParse = (request.RequestConfig.ResponseHeadersToParse is { Count: > 0 })
						? new HeadersList(request.RequestConfig.ResponseHeadersToParse, "x-elastic-product")
						: new HeadersList("x-elastic-product")
				};

			// Send request

			var (endpointPath, resolvedRouteValues, postData) = PrepareRequest<TRequest, TRequestParameters>(request);
			var openTelemetryDataMutator = GetOpenTelemetryDataMutator<TRequest, TRequestParameters>(request, resolvedRouteValues);

			TResponse response;

			if (isAsync)
			{
				response = await _transport
					.RequestAsync<TResponse>(endpointPath, postData, openTelemetryDataMutator, requestConfig, cancellationToken)
					.ConfigureAwait(false);
			}
			else
			{
				response = _transport.Request<TResponse>(endpointPath, postData, openTelemetryDataMutator, requestConfig);
			}

			// Evaluate product check result

			var hasSuccessStatusCode = response.ApiCallDetails.HttpStatusCode is >= 200 and <= 299;
			if (!hasSuccessStatusCode)
			{
				// The product check is unreliable for non success status codes.
				// We have to re-try on the next request.
				_productCheckStatus = (int)ProductCheckStatus.NotChecked;

				return response;
			}

			var productCheckSucceeded = response.ApiCallDetails.TryGetHeader("x-elastic-product", out var values) &&
										values.FirstOrDefault(x => x.Equals("Elasticsearch", StringComparison.Ordinal)) is not null;

			_productCheckStatus = productCheckSucceeded
				? (int)ProductCheckStatus.Succeeded
				: (int)ProductCheckStatus.Failed;

			if (_productCheckStatus == (int)ProductCheckStatus.Failed)
				throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);

			return response;
		}
	}

	private static Action<Activity>? GetOpenTelemetryDataMutator<TRequest, TRequestParameters>(TRequest request, Dictionary<string, string>? resolvedRouteValues)
		where TRequest : Request<TRequestParameters>
		where TRequestParameters : RequestParameters, new()
	{
		// If there are no subscribed listeners, we avoid some work and allocations
		if (!Elastic.Transport.Diagnostics.OpenTelemetry.ElasticTransportActivitySourceHasListeners)
			return null;

		return OpenTelemetryDataMutator;

		void OpenTelemetryDataMutator(Activity activity)
		{
			// We fall back to a general operation name in cases where the derived request fails to override the property
			var operationName = !string.IsNullOrEmpty(request.OperationName) ? request.OperationName : request.HttpMethod.GetStringValue();

			// TODO: Optimisation: We should consider caching these, either for cases where resolvedRouteValues is null, or
			// caching per combination of route values.
			// We should benchmark this first to assess the impact for common workloads.
			// The former is likely going to save some short-lived allocations, but only for requests to endpoints without required path parts.
			// The latter may bloat the cache as some combinations of path parts may rarely re-occur.

			activity.DisplayName = operationName;
			
			activity.SetTag(OpenTelemetry.SemanticConventions.DbOperation, !string.IsNullOrEmpty(request.OperationName) ? request.OperationName : "unknown");
			activity.SetTag($"{OpenTelemetrySpanAttributePrefix}schema_url", OpenTelemetrySchemaVersion);

			if (resolvedRouteValues is null)
				return;

			foreach (var value in resolvedRouteValues)
			{
				if (!string.IsNullOrEmpty(value.Key) && !string.IsNullOrEmpty(value.Value))
					activity.SetTag($"{OpenTelemetrySpanAttributePrefix}path_parts.{value.Key}", value.Value);
			}
		}
	}

	private (EndpointPath endpointPath, Dictionary<string, string>? resolvedRouteValues, PostData data) PrepareRequest<TRequest, TRequestParameters>(TRequest request)
		where TRequest : Request<TRequestParameters>
		where TRequestParameters : RequestParameters, new()
	{
		request.ThrowIfNull(nameof(request), "A request is required.");

		var (resolvedUrl, _, routeValues) = request.GetUrl(ElasticsearchClientSettings);
		var pathAndQuery = request.RequestParameters.CreatePathWithQueryStrings(resolvedUrl, ElasticsearchClientSettings);

		var postData =
			request.HttpMethod == HttpMethod.GET ||
			request.HttpMethod == HttpMethod.HEAD || !request.SupportsBody
				? null
				: PostData.Serializable(request);

		return (new EndpointPath(request.HttpMethod, pathAndQuery), routeValues, postData);
	}
}
