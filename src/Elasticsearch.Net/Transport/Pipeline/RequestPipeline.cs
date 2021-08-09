// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Diagnostics;
using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Specification.NodesApi;
using static Elasticsearch.Net.AuditEvent;

namespace Elasticsearch.Net
{
	public class RequestPipeline : IRequestPipeline
	{
		private const string ExpectedBuildFlavor = "default";
		private const string ExpectedProductName = "Elasticsearch";
		private const string ExpectedTagLine = "You Know, for Search";
		private const string NoNodesAttemptedMessage = "No nodes were attempted, this can happen when a node predicate does not match any nodes";

		internal const string ProductCheckTransientErrorWarning =
			"The client is unable to verify that the server is Elasticsearch due to an unsuccessful product check call. "
			+ "Some functionality may not be compatible if the server is running an unsupported product.";

		internal const string UndeterminedProductWarning =
			"The client is unable to verify that the server is Elasticsearch due to security privileges on the server side. "
			+ "Some functionality may not be compatible if the server is running an unsupported product.";

		private static readonly Version MinVersion = new(6, 0, 0);
		private static readonly Version Version7 = new(7, 0, 0);
		private static readonly Version Version714 = new(7, 14, 0);

		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IMemoryStreamFactory _memoryStreamFactory;
		private readonly IConnectionConfigurationValues _settings;

		// Used for enriching exceptions when the product check fails in a transient manor.
		// This is thread safe as product check runs behind semaphore and sequentially tries nodes until success, failure or timeout.
		private IApiCallDetails _lastProductCheckCallDetails;

		public RequestPipeline(
			IConnectionConfigurationValues configurationValues,
			IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory,
			IRequestParameters requestParameters
		)
		{
			_settings = configurationValues;
			_connectionPool = _settings.ConnectionPool;
			_connection = _settings.Connection;
			_dateTimeProvider = dateTimeProvider;
			_memoryStreamFactory = memoryStreamFactory;

			RequestConfiguration = requestParameters?.RequestConfiguration;
			StartedOn = dateTimeProvider.Now();
		}

		public List<Audit> AuditTrail { get; } = new();

		public bool DepletedRetries => Retried >= MaxRetries + 1 || IsTakingTooLong;

		public bool FirstPoolUsageNeedsSniffing =>
			!RequestDisabledSniff
			&& _connectionPool.SupportsReseeding && _settings.SniffsOnStartup && !_connectionPool.SniffedOnStartup;

		public bool IsTakingTooLong
		{
			get
			{
				var timeout = _settings.MaxRetryTimeout.GetValueOrDefault(RequestTimeout);
				var now = _dateTimeProvider.Now();

				// we apply a soft margin so that if a request times out at 59 seconds when the maximum is 60 we also abort.
				var margin = timeout.TotalMilliseconds / 100.0 * 98;
				var marginTimeSpan = TimeSpan.FromMilliseconds(margin);
				var timespanCall = now - StartedOn;
				var tookToLong = timespanCall >= marginTimeSpan;
				return tookToLong;
			}
		}

		public int MaxRetries =>
			RequestConfiguration?.ForceNode != null
				? 0
				: Math.Min(RequestConfiguration?.MaxRetries ?? _settings.MaxRetries.GetValueOrDefault(int.MaxValue), _connectionPool.MaxRetries);

		public bool Refresh { get; private set; }
		public int Retried { get; private set; }

		public IEnumerable<Node> SniffNodes => _connectionPool
			.CreateView(LazyAuditable)
			.ToList()
			.OrderBy(n => n.MasterEligible ? n.Uri.Port : int.MaxValue);

		public static string SniffPath => "_nodes/http,settings";

		public bool SniffsOnConnectionFailure =>
			!RequestDisabledSniff
			&& _connectionPool.SupportsReseeding && _settings.SniffsOnConnectionFault;

		public bool SniffsOnStaleCluster =>
			!RequestDisabledSniff
			&& _connectionPool.SupportsReseeding && _settings.SniffInformationLifeSpan.HasValue;

		public bool StaleClusterState
		{
			get
			{
				if (!SniffsOnStaleCluster)
					return false;

				// ReSharper disable once PossibleInvalidOperationException
				// already checked by SniffsOnStaleCluster
				var sniffLifeSpan = _settings.SniffInformationLifeSpan.Value;

				var now = _dateTimeProvider.Now();
				var lastSniff = _connectionPool.LastUpdate;

				return sniffLifeSpan < now - lastSniff;
			}
		}

		public DateTime StartedOn { get; private set; }

		private static DiagnosticSource DiagnosticSource { get; } = new DiagnosticListener(DiagnosticSources.RequestPipeline.SourceName);

		private TimeSpan PingTimeout =>
			RequestConfiguration?.PingTimeout
			?? _settings.PingTimeout
			?? (_connectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

		private IRequestConfiguration RequestConfiguration { get; }

		private bool RequestDisabledSniff => RequestConfiguration != null && (RequestConfiguration.DisableSniff ?? false);

		private TimeSpan RequestTimeout => RequestConfiguration?.RequestTimeout ?? _settings.RequestTimeout;

		private NodesInfoRequestParameters SniffParameters => new NodesInfoRequestParameters { Timeout = PingTimeout, FlatSettings = true };

		void IDisposable.Dispose() => Dispose();

		public void AuditCancellationRequested() => Audit(CancellationRequested).Dispose();

		public void BadResponse<TResponse>(ref TResponse response, IApiCallDetails callDetails, RequestData data,
			ElasticsearchClientException exception
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (response == null)
			{
				//make sure we copy over the error body in case we disabled direct streaming.
				var s = callDetails?.ResponseBodyInBytes == null ? Stream.Null : _memoryStreamFactory.Create(callDetails.ResponseBodyInBytes);
				var m = callDetails?.ResponseMimeType ?? RequestData.DefaultJsonMimeType;
				response = ResponseBuilder.ToResponse<TResponse>(data, exception, callDetails?.HttpStatusCode, null, s, m);
			}

			response.ApiCall.AuditTrail = AuditTrail;
		}

		public TResponse CallElasticsearch<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new()
		{
			using var audit = Audit(HealthyResponse, requestData.Node);
			using var diagnostic =
				DiagnosticSource.Diagnose<RequestData, IApiCallDetails>(DiagnosticSources.RequestPipeline.CallElasticsearch, requestData);

			audit.Path = requestData.PathAndQuery;
			try
			{
				ThrowIfTransientProductCheckFailure();
				var response = _connection.Request<TResponse>(requestData);
				return PostCallElasticsearch(requestData, response, diagnostic, audit);
			}
			catch (Exception e)
			{
				audit.Event = requestData.OnFailureAuditEvent;
				audit.Exception = e;
				throw;
			}
		}

		public async Task<TResponse> CallElasticsearchAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new()
		{
			using var audit = Audit(HealthyResponse, requestData.Node);
			using var diagnostic =
				DiagnosticSource.Diagnose<RequestData, IApiCallDetails>(DiagnosticSources.RequestPipeline.CallElasticsearch, requestData);

			audit.Path = requestData.PathAndQuery;
			try
			{
				ThrowIfTransientProductCheckFailure();
				var response = await _connection.RequestAsync<TResponse>(requestData, cancellationToken).ConfigureAwait(false);
				return PostCallElasticsearch(requestData, response, diagnostic, audit);
			}
			catch (Exception e)
			{
				audit.Event = requestData.OnFailureAuditEvent;
				audit.Exception = e;
				throw;
			}
		}

		public ElasticsearchClientException CreateClientException<TResponse>(
			TResponse response, IApiCallDetails callDetails, RequestData data, List<PipelineException> pipelineExceptions
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (callDetails?.Success ?? false)
				return null;

			var innerException = pipelineExceptions.HasAny() ? pipelineExceptions.AsAggregateOrFirst() : callDetails?.OriginalException;

			if (_connectionPool.ProductCheckStatus == ProductCheckStatus.TransientFailure)
				callDetails = _lastProductCheckCallDetails;

			var statusCode = callDetails?.HttpStatusCode != null ? callDetails.HttpStatusCode.Value.ToString() : "unknown";
			var resource = callDetails == null
				? "unknown resource"
				: $"Status code {statusCode} from: {callDetails.HttpMethod} {callDetails.Uri.PathAndQuery}";

			var exceptionMessage = innerException?.Message ?? "Request failed to execute";

			var pipelineFailure = data.OnFailurePipelineFailure;
			if (pipelineExceptions.HasAny())
				pipelineFailure = pipelineExceptions.Last().FailureReason;

			if (IsTakingTooLong)
			{
				pipelineFailure = PipelineFailure.MaxTimeoutReached;
				Audit(MaxTimeoutReached);
				exceptionMessage = "Maximum timeout reached while retrying request";
			}
			else if (Retried >= MaxRetries && MaxRetries > 0)
			{
				pipelineFailure = PipelineFailure.MaxRetriesReached;
				Audit(MaxRetriesReached);
				exceptionMessage = "Maximum number of retries reached";

				var now = _dateTimeProvider.Now();
				var activeNodes = _connectionPool.Nodes.Count(n => n.IsAlive || n.DeadUntil <= now);
				if (Retried >= activeNodes)
				{
					Audit(FailedOverAllNodes);
					exceptionMessage += ", failed over to all the known alive nodes before failing";
				}
			}

			exceptionMessage += !exceptionMessage.EndsWith(".", StringComparison.Ordinal) ? $". Call: {resource}" : $" Call: {resource}";

			if (response != null && response.TryGetServerErrorReason(out var reason))
				exceptionMessage += $". ServerError: {reason}";

			var clientException = new ElasticsearchClientException(pipelineFailure, exceptionMessage, innerException)
			{
				Request = data, Response = callDetails, AuditTrail = AuditTrail
			};

			return clientException;
		}

		public void FirstPoolUsage(SemaphoreSlim semaphore)
		{
			// Fail early if the check has already run and indicates an unsupported product
			switch (_connectionPool.ProductCheckStatus)
			{
				case ProductCheckStatus.InvalidProduct:
					throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);
				case ProductCheckStatus.UnsupportedBuildFlavor:
					throw new UnsupportedProductException(UnsupportedProductException.InvalidBuildFlavorError);
			}

			// If sniffing has completed and the product check has run, we are done!
			if (!FirstPoolUsageNeedsSniffing && !RequiresProductCheck(_connectionPool.ProductCheckStatus))
				return;

			if (!semaphore.Wait(_settings.RequestTimeout.Add(_settings.RequestTimeout))) // Double the timeout to allow for product check delays
			{
				if (FirstPoolUsageNeedsSniffing)
					throw new PipelineException(PipelineFailure.CouldNotStartSniffOnStartup, null);

				// We don't report a product check failure here to avoid breaking in unusual situations.
				// Instead, we assume that subsequent requests will fail anyway.
				return;
			}

			try
			{
				if (RequiresProductCheck(_connectionPool.ProductCheckStatus))
					using (Audit(ProductCheckOnStartup))
					{
						if (RequestConfiguration?.ForceNode is not null)
						{
							var node = new Node(RequestConfiguration.ForceNode);
							ProductCheck(node);
						}
						else
						{
							var nodes = _connectionPool.Nodes.ToArray(); // Isolated copy of nodes for the product check

							// We determine the product from the first node which successfully responds.
							// If a node fails, we retry other available nodes until the request timeout is reached.
							for (var i = 0;
								i < nodes.Length && RequiresProductCheck(_connectionPool.ProductCheckStatus) && !IsTakingTooLong;
								i++)
								ProductCheck(nodes[i]);
						}

						// Set the request start time so that we allow the full request timeout
						// for the actual API call if it is sent.
						StartedOn = _dateTimeProvider.Now();
					}

				// Avoid proceeding to sniffing if the product is not valid
				switch (_connectionPool.ProductCheckStatus)
				{
					case ProductCheckStatus.InvalidProduct:
						throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);
					case ProductCheckStatus.UnsupportedBuildFlavor:
						throw new UnsupportedProductException(UnsupportedProductException.InvalidBuildFlavorError);
				}

				if (!FirstPoolUsageNeedsSniffing)
					return;

				using (Audit(SniffOnStartup))
				{
					Sniff();
					_connectionPool.SniffedOnStartup = true;
				}
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task FirstPoolUsageAsync(SemaphoreSlim semaphore, CancellationToken cancellationToken)
		{
			// Fail early if the check has already run and indicates an unsupported product
			switch (_connectionPool.ProductCheckStatus)
			{
				case ProductCheckStatus.InvalidProduct:
					throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);
				case ProductCheckStatus.UnsupportedBuildFlavor:
					throw new UnsupportedProductException(UnsupportedProductException.InvalidBuildFlavorError);
			}

			// If sniffing has completed and the product check has run, we are done!
			if (!FirstPoolUsageNeedsSniffing && !RequiresProductCheck(_connectionPool.ProductCheckStatus))
				return;

			// TODO cancellationToken could throw here and will bubble out as OperationCancelledException
			// everywhere else it would bubble out wrapped in a `UnexpectedElasticsearchClientException`
			var success = await semaphore.WaitAsync(_settings.RequestTimeout.Add(_settings.RequestTimeout), cancellationToken).ConfigureAwait(false);

			if (!success)
			{
				if (FirstPoolUsageNeedsSniffing)
					throw new PipelineException(PipelineFailure.CouldNotStartSniffOnStartup, null);

				// We don't report a product check failure here to avoid breaking in unusual situations.
				// Instead, we assume that subsequent requests will fail anyway.
				return;
			}

			try
			{
				if (RequiresProductCheck(_connectionPool.ProductCheckStatus))
					using (Audit(ProductCheckOnStartup))
					{
						if (RequestConfiguration?.ForceNode is not null)
						{
							var node = new Node(RequestConfiguration.ForceNode);
							await ProductCheckAsync(node, cancellationToken).ConfigureAwait(false);
						}
						else
						{
							var nodes = _connectionPool.Nodes.ToArray(); // Isolated copy of nodes for the product check

							// We determine the product from the first node which successfully responds.
							// If a node fails, we retry other available nodes until the request timeout is reached.
							for (var i = 0;
								i < nodes.Length && RequiresProductCheck(_connectionPool.ProductCheckStatus) && !IsTakingTooLong;
								i++)
								await ProductCheckAsync(nodes[i], cancellationToken).ConfigureAwait(false);
						}

						// Set the request start time so that we allow the full request timeout
						// for the actual API call if it is sent.
						StartedOn = _dateTimeProvider.Now();
					}

				// Avoid proceeding to sniffing if the product is not valid
				switch (_connectionPool.ProductCheckStatus)
				{
					case ProductCheckStatus.InvalidProduct:
						throw new UnsupportedProductException(UnsupportedProductException.InvalidProductError);
					case ProductCheckStatus.UnsupportedBuildFlavor:
						throw new UnsupportedProductException(UnsupportedProductException.InvalidBuildFlavorError);
				}

				if (FirstPoolUsageNeedsSniffing)
					using (Audit(SniffOnStartup))
					{
						await SniffAsync(cancellationToken).ConfigureAwait(false);
						_connectionPool.SniffedOnStartup = true;
					}
			}
			finally
			{
				semaphore.Release();
			}
		}

		public void MarkAlive(Node node) => node.MarkAlive();

		public void MarkDead(Node node)
		{
			var deadUntil = _dateTimeProvider.DeadTime(node.FailedAttempts, _settings.DeadTimeout, _settings.MaxDeadTimeout);
			node.MarkDead(deadUntil);
			Retried++;
		}

		public IEnumerable<Node> NextNode()
		{
			if (RequestConfiguration?.ForceNode != null)
			{
				yield return new Node(RequestConfiguration.ForceNode);

				yield break;
			}

			//This for loop allows to break out of the view state machine if we need to
			//force a refresh (after reseeding connection pool). We have a hardcoded limit of only
			//allowing 100 of these refreshes per call
			var refreshed = false;
			for (var i = 0; i < 100; i++)
			{
				if (DepletedRetries)
					yield break;

				foreach (var node in _connectionPool
					.CreateView(LazyAuditable)
					.TakeWhile(node => !DepletedRetries))
				{
					if (!_settings.NodePredicate(node))
						continue;

					yield return node;

					if (!Refresh)
						continue;

					Refresh = false;
					refreshed = true;
					break;
				}
				//unless a refresh was requested we will not iterate over more then a single view.
				//keep in mind refreshes are also still bound to overall max retry count/timeout.
				if (!refreshed)
					break;
			}
		}

		public void Ping(Node node)
		{
			// We want to fail before attempting a ping in cases where the product check is not successful
			ThrowIfTransientProductCheckFailure();

			if (PingDisabled(node))
				return;

			var pingData = CreatePingRequestData(node);

			using var audit = Audit(PingSuccess, node);
			using var d = DiagnosticSource.Diagnose<RequestData, IApiCallDetails>(DiagnosticSources.RequestPipeline.Ping, pingData);

			audit.Path = pingData.PathAndQuery;
			try
			{
				var response = _connection.Request<VoidResponse>(pingData);
				d.EndState = response;
				audit.Stop();
				ThrowBadAuthPipelineExceptionWhenNeeded(response);
				//ping should not silently accept bad but valid http responses
				if (!response.Success)
					throw new PipelineException(pingData.OnFailurePipelineFailure, response.OriginalException) { ApiCall = response };
			}
			catch (Exception e)
			{
				var response = (e as PipelineException)?.ApiCall;
				audit.Event = PingFailure;
				audit.Exception = e;
				throw new PipelineException(PipelineFailure.PingFailure, e) { ApiCall = response };
			}
		}

		public async Task PingAsync(Node node, CancellationToken cancellationToken)
		{
			// We want to fail before attempting a ping in cases where the product check is not successful
			ThrowIfTransientProductCheckFailure();

			if (PingDisabled(node))
				return;

			var pingData = CreatePingRequestData(node);

			using var audit = Audit(PingSuccess, node);
			using var d = DiagnosticSource.Diagnose<RequestData, IApiCallDetails>(DiagnosticSources.RequestPipeline.Ping, pingData);

			audit.Path = pingData.PathAndQuery;
			try
			{
				var response = await _connection.RequestAsync<VoidResponse>(pingData, cancellationToken).ConfigureAwait(false);
				d.EndState = response;
				audit.Stop();
				ThrowBadAuthPipelineExceptionWhenNeeded(response);
				//ping should not silently accept bad but valid http responses
				if (!response.Success)
					throw new PipelineException(pingData.OnFailurePipelineFailure, response.OriginalException) { ApiCall = response };
			}
			catch (Exception e)
			{
				var response = (e as PipelineException)?.ApiCall;
				audit.Event = PingFailure;
				audit.Exception = e;
				throw new PipelineException(PipelineFailure.PingFailure, e) { ApiCall = response };
			}
		}

		public void Sniff()
		{
			var exceptions = new List<Exception>();
			foreach (var node in SniffNodes)
			{
				var requestData = CreateSniffRequestData(node);
				using (var audit = Audit(SniffSuccess, node))
				using (var d = DiagnosticSource.Diagnose<RequestData, IApiCallDetails>(DiagnosticSources.RequestPipeline.Sniff, requestData))
				using (DiagnosticSource.Diagnose(DiagnosticSources.RequestPipeline.Sniff, requestData))
					try
					{
						audit.Path = requestData.PathAndQuery;
						var response = _connection.Request<SniffResponse>(requestData);
						d.EndState = response;
						audit.Stop();
						ThrowBadAuthPipelineExceptionWhenNeeded(response);
						//sniff should not silently accept bad but valid http responses
						if (!response.Success)
							throw new PipelineException(requestData.OnFailurePipelineFailure, response.OriginalException) { ApiCall = response };

						var nodes = response.ToNodes(_connectionPool.UsingSsl);
						_connectionPool.Reseed(nodes);
						Refresh = true;
						return;
					}
					catch (Exception e)
					{
						audit.Event = SniffFailure;
						audit.Exception = e;
						exceptions.Add(e);
					}
			}

			throw new PipelineException(PipelineFailure.SniffFailure, exceptions.AsAggregateOrFirst());
		}

		public async Task SniffAsync(CancellationToken cancellationToken)
		{
			var exceptions = new List<Exception>();
			foreach (var node in SniffNodes)
			{
				var requestData = CreateSniffRequestData(node);
				using (var audit = Audit(SniffSuccess, node))
				using (var d = DiagnosticSource.Diagnose<RequestData, IApiCallDetails>(DiagnosticSources.RequestPipeline.Sniff, requestData))
					try
					{
						audit.Path = requestData.PathAndQuery;
						var response = await _connection.RequestAsync<SniffResponse>(requestData, cancellationToken).ConfigureAwait(false);
						d.EndState = response;
						audit.Stop();
						ThrowBadAuthPipelineExceptionWhenNeeded(response);
						//sniff should not silently accept bad but valid http responses
						if (!response.Success)
							throw new PipelineException(requestData.OnFailurePipelineFailure, response.OriginalException) { ApiCall = response };

						_connectionPool.Reseed(response.ToNodes(_connectionPool.UsingSsl));
						Refresh = true;
						return;
					}
					catch (Exception e)
					{
						audit.Event = SniffFailure;
						audit.Exception = e;
						exceptions.Add(e);
					}
			}

			throw new PipelineException(PipelineFailure.SniffFailure, exceptions.AsAggregateOrFirst());
		}

		public void SniffOnConnectionFailure()
		{
			if (!SniffsOnConnectionFailure)
				return;

			using (Audit(SniffOnFail))
				Sniff();
		}

		public async Task SniffOnConnectionFailureAsync(CancellationToken cancellationToken)
		{
			if (!SniffsOnConnectionFailure)
				return;

			using (Audit(SniffOnFail))
				await SniffAsync(cancellationToken).ConfigureAwait(false);
		}

		public void SniffOnStaleCluster()
		{
			if (!StaleClusterState)
				return;

			using (Audit(AuditEvent.SniffOnStaleCluster))
			{
				Sniff();
				_connectionPool.SniffedOnStartup = true;
			}
		}

		public async Task SniffOnStaleClusterAsync(CancellationToken cancellationToken)
		{
			if (!StaleClusterState)
				return;

			using (Audit(AuditEvent.SniffOnStaleCluster))
			{
				await SniffAsync(cancellationToken).ConfigureAwait(false);
				_connectionPool.SniffedOnStartup = true;
			}
		}

		public void ThrowNoNodesAttempted(RequestData requestData, List<PipelineException> seenExceptions)
		{
			var clientException = new ElasticsearchClientException(PipelineFailure.NoNodesAttempted, NoNodesAttemptedMessage, (Exception)null);
			using (Audit(NoNodesAttempted))
				throw new UnexpectedElasticsearchClientException(clientException, seenExceptions) { Request = requestData, AuditTrail = AuditTrail };
		}

		private void ThrowIfTransientProductCheckFailure()
		{
			if (_connectionPool.ProductCheckStatus == ProductCheckStatus.TransientFailure)
				throw new PipelineException(PipelineFailure.FailedProductCheck, _lastProductCheckCallDetails?.OriginalException);
		}

		private static bool RequiresProductCheck(ProductCheckStatus status) =>
			status is ProductCheckStatus.NotChecked or ProductCheckStatus.TransientFailure;

		private TResponse PostCallElasticsearch<TResponse>(RequestData requestData, TResponse response,
			Diagnostic<RequestData, IApiCallDetails> diagnostic, Auditable audit
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (response.ApiCall is ApiCallDetails callDetails)
				switch (_connectionPool.ProductCheckStatus)
				{
					// Add additional warning to debug information if the product could not be determined and may not be Elasticsearch
					case ProductCheckStatus.UndeterminedProduct:
						WriteProductCheckWarning(UndeterminedProductWarning);
						break;

					// Add additional warning to debug information if the product could not be determined due to a transient error.
					case ProductCheckStatus.TransientFailure:
						WriteProductCheckWarning(ProductCheckTransientErrorWarning);
						break;
				}

			diagnostic.EndState = response.ApiCall;
			response.ApiCall.AuditTrail = AuditTrail;
			audit.Stop();
			ThrowBadAuthPipelineExceptionWhenNeeded(response.ApiCall, response);
			if (!response.ApiCall.Success)
				audit.Event = requestData.OnFailureAuditEvent;
			return response;

			void WriteProductCheckWarning(string warning)
			{
				Debug.WriteLine(warning);
				callDetails.BuildDebugInformationPrefix = sb =>
				{
					sb.AppendLine("# Warnings:");
					sb.AppendLine($"- {warning}");
				};
			}
		}

		internal void ProductCheck(Node node)
		{
			// We don't throw an exception on failure here since we don't want this new check to break consumers on upgrade.

			var requestData = CreateRootPathRequestData(node);
			using var audit = Audit(ProductCheckSuccess, node);

			try
			{
				audit.Path = requestData.PathAndQuery;
				var response = _connection.Request<RootResponse>(requestData);
				_lastProductCheckCallDetails = response?.ApiCall;
				var succeeded = ApplyProductCheckRules(response);
				audit.Stop();

				if (!succeeded)
					audit.Event = ProductCheckFailure;
			}
			catch (Exception e)
			{
				audit.Event = ProductCheckFailure;
				audit.Exception = e;
			}
		}

		internal async Task ProductCheckAsync(Node node, CancellationToken cancellationToken)
		{
			// We don't throw an exception on failure here since we don't want this new check to break consumers on upgrade.

			var requestData = CreateRootPathRequestData(node);
			using var audit = Audit(ProductCheckSuccess, node);

			try
			{
				audit.Path = requestData.PathAndQuery;
				var response = await _connection.RequestAsync<RootResponse>(requestData, cancellationToken).ConfigureAwait(false);
				_lastProductCheckCallDetails = response?.ApiCall;
				var succeeded = ApplyProductCheckRules(response);
				audit.Stop();

				if (!succeeded)
					audit.Event = ProductCheckFailure;
			}
			catch (Exception e)
			{
				audit.Event = ProductCheckFailure;
				audit.Exception = e;
			}
		}

		private bool ApplyProductCheckRules(RootResponse response)
		{
			var productName = response.ApiCall?.ProductName;

			// Fast path for v7.14+ where the header should have been sent
			if (response.Success && !string.IsNullOrEmpty(productName))
			{
				_connectionPool.ProductCheckStatus = ExpectedProductName.Equals(productName, StringComparison.Ordinal)
					? ProductCheckStatus.ValidProduct
					: ProductCheckStatus.InvalidProduct;

				return true;
			}

			// We should always have a status code!
			var statusCode = response.HttpStatusCode ?? 0;

			// The call to the root path requires monitor permissions. If the current use lacks those, we cannot perform product validation.
			if (statusCode is 401 or 403)
			{
				_connectionPool.ProductCheckStatus = ProductCheckStatus.UndeterminedProduct;
				return true;
			}

			// Any response besides a 200, 401 or 403 is considered a failure and the check is considered incomplete and marked as a likely transient failure.
			// This means that the check will run on subsequent requests until we have a valid response to evaluate.
			// By returning false, the failure to perform the product check will be included in the audit log.
			if (!response.Success)
			{
				_connectionPool.ProductCheckStatus = ProductCheckStatus.TransientFailure;
				return false;
			}

			// Start by assuming the product is valid
			_connectionPool.ProductCheckStatus = ProductCheckStatus.ValidProduct;

			// We expect to have a version number from the build version.
			// If we don't, the product is not Elasticsearch
			if (string.IsNullOrEmpty(response.Version?.Number))
				_connectionPool.ProductCheckStatus = ProductCheckStatus.InvalidProduct;
			else
			{
				var versionNumber = response.Version.Number;
				var indexOfSuffix = versionNumber.IndexOf("-", StringComparison.Ordinal);

				if (indexOfSuffix > 0)
					versionNumber = versionNumber.Substring(0, indexOfSuffix);

				var version = new Version(versionNumber);

				if (VersionTooLow(version) ||
					TagLineInvalid(version, response) ||
					Version714InvalidHeader(version, productName))
					_connectionPool.ProductCheckStatus = ProductCheckStatus.InvalidProduct;

				ValidateBuildFlavor(version);
			}

			return true;

			// Elasticsearch should be version 6.0.0 or greater
			// Note: For best compatibility, the client should not be used with versions prior to 7.0.0, but we do not enforce that here
			static bool VersionTooLow(Version version)
			{
				return version < MinVersion;
			}

			// Between v6.0.0 and 6.99.99, we expect the tag line to match the expected value
			static bool TagLineInvalid(Version version, RootResponse response)
			{
				return version > MinVersion && !ExpectedTagLine.Equals(response.Tagline, StringComparison.Ordinal);
			}

			// After v7.14.0 we expect the product header to be present and include the expected product name
			static bool Version714InvalidHeader(Version version, string productName)
			{
				return version >= Version714 && !ExpectedProductName.Equals(productName, StringComparison.Ordinal);
			}

			// After v7.0.0, we expect the build flavor to match expected values.
			void ValidateBuildFlavor(Version version)
			{
				if (version < Version7) // We only check this on releases since 7.0.0
					return;

				if (!ExpectedBuildFlavor.Equals(response.Version?.BuildFlavor, StringComparison.Ordinal))
					_connectionPool.ProductCheckStatus = ProductCheckStatus.UnsupportedBuildFlavor;
			}
		}

		private bool PingDisabled(Node node) =>
			(RequestConfiguration?.DisablePing).GetValueOrDefault(false)
			|| _settings.DisablePings || !_connectionPool.SupportsPinging || !node.IsResurrected;

		private Auditable Audit(AuditEvent type, Node node = null) => new Auditable(type, AuditTrail, _dateTimeProvider, node);

		private RequestData CreatePingRequestData(Node node)
		{
			var requestOverrides = new RequestConfiguration
			{
				PingTimeout = PingTimeout,
				RequestTimeout = PingTimeout,
				BasicAuthenticationCredentials = _settings.BasicAuthenticationCredentials,
				ApiKeyAuthenticationCredentials = _settings.ApiKeyAuthenticationCredentials,
				EnableHttpPipelining = RequestConfiguration?.EnableHttpPipelining ?? _settings.HttpPipeliningEnabled,
				ForceNode = RequestConfiguration?.ForceNode
			};

			IRequestParameters requestParameters = new RootNodeInfoRequestParameters { RequestConfiguration = requestOverrides };

			var data = new RequestData(HttpMethod.HEAD, string.Empty, null, _settings, requestParameters, _memoryStreamFactory) { Node = node };
			return data;
		}

		private RequestData CreateRootPathRequestData(Node node)
		{
			var requestOverrides = new RequestConfiguration
			{
				BasicAuthenticationCredentials = _settings.BasicAuthenticationCredentials,
				ApiKeyAuthenticationCredentials = _settings.ApiKeyAuthenticationCredentials,
				EnableHttpPipelining = RequestConfiguration?.EnableHttpPipelining ?? _settings.HttpPipeliningEnabled,
				ForceNode = RequestConfiguration?.ForceNode
			};

			IRequestParameters requestParameters = new RootNodeInfoRequestParameters { RequestConfiguration = requestOverrides };

			return new RequestData(HttpMethod.GET, string.Empty, null, _settings, requestParameters, _memoryStreamFactory) { Node = node };
		}

		private static void ThrowBadAuthPipelineExceptionWhenNeeded(IApiCallDetails details, IElasticsearchResponse response = null)
		{
			if (details?.HttpStatusCode == 401)
				throw new PipelineException(PipelineFailure.BadAuthentication, details.OriginalException) { Response = response, ApiCall = details };
		}

		private void LazyAuditable(AuditEvent e, Node n)
		{
			using (new Auditable(e, AuditTrail, _dateTimeProvider, n)) { }
		}

		private RequestData CreateSniffRequestData(Node node) =>
			new(HttpMethod.GET, SniffPath, null, _settings, SniffParameters, _memoryStreamFactory) { Node = node };

		protected virtual void Dispose() { }
	}
}
