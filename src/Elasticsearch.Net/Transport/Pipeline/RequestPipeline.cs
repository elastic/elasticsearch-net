using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Elasticsearch.Net.AuditEvent;

namespace Elasticsearch.Net
{
	public class RequestPipeline : IRequestPipeline
	{
		private static readonly string NoNodesAttemptedMessage =
			"No nodes were attempted, this can happen when a node predicate does not match any nodes";

		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IMemoryStreamFactory _memoryStreamFactory;
		private readonly IConnectionConfigurationValues _settings;

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

			RequestParameters = requestParameters;
			RequestConfiguration = requestParameters?.RequestConfiguration;
			StartedOn = dateTimeProvider.Now();
		}

		public List<Audit> AuditTrail { get; } = new List<Audit>();

		// TODO: rename to DepeletedRetries in 7.x
		public bool DepleededRetries => Retried >= MaxRetries + 1 || IsTakingTooLong;

		public bool FirstPoolUsageNeedsSniffing =>
			!RequestDisabledSniff
			&& _connectionPool.SupportsReseeding && _settings.SniffsOnStartup && !_connectionPool.SniffedOnStartup;

		public bool IsTakingTooLong
		{
			get
			{
				var timeout = _settings.MaxRetryTimeout.GetValueOrDefault(RequestTimeout);
				var now = _dateTimeProvider.Now();

				//we apply a soft margin so that if a request timesout at 59 seconds when the maximum is 60 we also abort.
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
		public int Retried { get; private set; } = 0;

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
				if (!SniffsOnStaleCluster) return false;

				// ReSharper disable once PossibleInvalidOperationException
				// already checked by SniffsOnStaleCluster
				var sniffLifeSpan = _settings.SniffInformationLifeSpan.Value;

				var now = _dateTimeProvider.Now();
				var lastSniff = _connectionPool.LastUpdate;

				return sniffLifeSpan < now - lastSniff;
			}
		}

		public DateTime StartedOn { get; }

		private TimeSpan PingTimeout =>
			RequestConfiguration?.PingTimeout
			?? _settings.PingTimeout
			?? (_connectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

		private IRequestConfiguration RequestConfiguration { get; }

		private bool RequestDisabledSniff => RequestConfiguration != null && (RequestConfiguration.DisableSniff ?? false);

		private IRequestParameters RequestParameters { get; }

		private TimeSpan RequestTimeout => RequestConfiguration?.RequestTimeout ?? _settings.RequestTimeout;

		private NodesInfoRequestParameters SniffParameters => new NodesInfoRequestParameters
		{
			Timeout = PingTimeout,
			FlatSettings = true
		};

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
				var s = callDetails?.ResponseBodyInBytes == null ? Stream.Null : new MemoryStream(callDetails.ResponseBodyInBytes);
				var m = callDetails?.ResponseMimeType ?? RequestData.MimeType;
				response = ResponseBuilder.ToResponse<TResponse>(data, exception, callDetails?.HttpStatusCode, null, s, m);
			}

			response.ApiCall.AuditTrail = AuditTrail;
		}

		public TResponse CallElasticsearch<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new()
		{
			using (var audit = Audit(HealthyResponse))
			{
				audit.Node = requestData.Node;
				audit.Path = requestData.PathAndQuery;

				TResponse response = null;
				try
				{
					response = _connection.Request<TResponse>(requestData);
					response.ApiCall.AuditTrail = AuditTrail;
					ThrowBadAuthPipelineExceptionWhenNeeded(response.ApiCall, response);
					if (!response.ApiCall.Success) audit.Event = requestData.OnFailureAuditEvent;
					return response;
				}
				catch (Exception e)
				{
					(response as ElasticsearchResponse<Stream>)?.Body?.Dispose();
					audit.Event = requestData.OnFailureAuditEvent;
					audit.Exception = e;
					throw;
				}
			}
		}

		public async Task<TResponse> CallElasticsearchAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new()
		{
			using (var audit = Audit(HealthyResponse))
			{
				audit.Node = requestData.Node;
				audit.Path = requestData.PathAndQuery;

				TResponse response = null;
				try
				{
					response = await _connection.RequestAsync<TResponse>(requestData, cancellationToken).ConfigureAwait(false);
					response.ApiCall.AuditTrail = AuditTrail;
					ThrowBadAuthPipelineExceptionWhenNeeded(response.ApiCall, response);
					if (!response.ApiCall.Success) audit.Event = requestData.OnFailureAuditEvent;
					return response;
				}
				catch (Exception e)
				{
					(response as ElasticsearchResponse<Stream>)?.Body?.Dispose();
					audit.Event = requestData.OnFailureAuditEvent;
					audit.Exception = e;
					throw;
				}
			}
		}

		public ElasticsearchClientException CreateClientException<TResponse>(
			TResponse response, IApiCallDetails callDetails, RequestData data, List<PipelineException> pipelineExceptions
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (callDetails?.Success ?? false) return null;

			var innerException = pipelineExceptions.HasAny() ? pipelineExceptions.AsAggregateOrFirst() : callDetails?.OriginalException;

			var statusCode = callDetails?.HttpStatusCode != null ? callDetails.HttpStatusCode.Value.ToString() : "unknown";
			var resource = callDetails == null
				? "unknown resource"
				: $"Status code {statusCode} from: {callDetails.HttpMethod} {callDetails.Uri.PathAndQuery}";


			var exceptionMessage = innerException?.Message ?? $"Request failed to execute";

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
				// TODO make AliveNodes on IConnectionPool public in 7.0 (default interface C# 8 FTW)
				var activeNodes = _connectionPool.Nodes.Count(n => n.IsAlive || n.DeadUntil <= now);
				if (Retried >= activeNodes)
				{
					Audit(FailedOverAllNodes);
					exceptionMessage += ", failed over to all the known alive nodes before failing";
				}
			}

			exceptionMessage += $". Call: {resource}";
			if (response != null && response.TryGetServerErrorReason(out var reason))
				exceptionMessage += $". ServerError: {reason}";

			var clientException = new ElasticsearchClientException(pipelineFailure, exceptionMessage, innerException)
			{
				Request = data,
				Response = callDetails,
				AuditTrail = AuditTrail
			};

			return clientException;
		}

		public void FirstPoolUsage(SemaphoreSlim semaphore)
		{
			if (!FirstPoolUsageNeedsSniffing) return;

			if (!semaphore.Wait(_settings.RequestTimeout))
			{
				if (FirstPoolUsageNeedsSniffing)
					throw new PipelineException(PipelineFailure.CouldNotStartSniffOnStartup, null);

				return;
			}

			if (!FirstPoolUsageNeedsSniffing)
			{
				semaphore.Release();
				return;
			}

			try
			{
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
			if (!FirstPoolUsageNeedsSniffing) return;

			var success = await semaphore.WaitAsync(_settings.RequestTimeout, cancellationToken).ConfigureAwait(false);
			if (!success)
			{
				if (FirstPoolUsageNeedsSniffing)
					throw new PipelineException(PipelineFailure.CouldNotStartSniffOnStartup, null);

				return;
			}

			if (!FirstPoolUsageNeedsSniffing)
			{
				semaphore.Release();
				return;
			}
			try
			{
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
			//force a refresh (after reseeding connectionpool). We have a hardcoded limit of only
			//allowing 100 of these refreshes per call
			var refreshed = false;
			for (var i = 0; i < 100; i++)
			{
				if (DepleededRetries) yield break;

				foreach (var node in _connectionPool
					.CreateView(LazyAuditable)
					.TakeWhile(node => !DepleededRetries))
				{
					if (!_settings.NodePredicate(node)) continue;

					yield return node;

					if (!Refresh) continue;

					Refresh = false;
					refreshed = true;
					break;
				}
				//unless a refresh was requested we will not iterate over more then a single view.
				//keep in mind refreshes are also still bound to overall maxretry count/timeout.
				if (!refreshed) break;
			}
		}

		public void Ping(Node node)
		{
			if (PingDisabled(node)) return;

			using (var audit = Audit(PingSuccess))
			{
				try
				{
					var pingData = CreatePingRequestData(node, audit);
					var response = _connection.Request<VoidResponse>(pingData);
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
		}

		public async Task PingAsync(Node node, CancellationToken cancellationToken)
		{
			if (PingDisabled(node)) return;

			using (var audit = Audit(PingSuccess))
			{
				try
				{
					var pingData = CreatePingRequestData(node, audit);
					var response = await _connection.RequestAsync<VoidResponse>(pingData, cancellationToken).ConfigureAwait(false);
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
		}

		public void Sniff()
		{
			var exceptions = new List<Exception>();
			foreach (var node in SniffNodes)
			{
				using (var audit = Audit(SniffSuccess))
				{
					audit.Node = node;
					try
					{
						var requestData = CreateSniffRequestData(node);
						audit.Path = requestData.PathAndQuery;
						var response = _connection.Request<SniffResponse>(requestData);
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
						continue;
					}
				}
			}
			throw new PipelineException(PipelineFailure.SniffFailure, exceptions.AsAggregateOrFirst());
		}

		public async Task SniffAsync(CancellationToken cancellationToken)
		{
			var exceptions = new List<Exception>();
			foreach (var node in SniffNodes)
			{
				using (var audit = Audit(SniffSuccess))
				{
					audit.Node = node;
					try
					{
						var requestData = CreateSniffRequestData(node);
						audit.Path = requestData.PathAndQuery;
						var response = await _connection.RequestAsync<SniffResponse>(requestData, cancellationToken).ConfigureAwait(false);
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
						continue;
					}
				}
			}
			throw new PipelineException(PipelineFailure.SniffFailure, exceptions.AsAggregateOrFirst());
		}

		public void SniffOnConnectionFailure()
		{
			if (!SniffsOnConnectionFailure) return;

			using (Audit(SniffOnFail))
				Sniff();
		}

		public async Task SniffOnConnectionFailureAsync(CancellationToken cancellationToken)
		{
			if (!SniffsOnConnectionFailure) return;

			using (Audit(SniffOnFail))
				await SniffAsync(cancellationToken).ConfigureAwait(false);
		}

		public void SniffOnStaleCluster()
		{
			if (!StaleClusterState) return;

			using (Audit(AuditEvent.SniffOnStaleCluster))
			{
				Sniff();
				_connectionPool.SniffedOnStartup = true;
			}
		}

		public async Task SniffOnStaleClusterAsync(CancellationToken cancellationToken)
		{
			if (!StaleClusterState) return;

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
				throw new UnexpectedElasticsearchClientException(clientException, seenExceptions)
				{
					Request = requestData,
					AuditTrail = AuditTrail
				};
		}

		private bool PingDisabled(Node node) =>
			(RequestConfiguration?.DisablePing).GetValueOrDefault(false)
			|| _settings.DisablePings || !_connectionPool.SupportsPinging || !node.IsResurrected;

		private Auditable Audit(AuditEvent type) => new Auditable(type, AuditTrail, _dateTimeProvider);

		private RequestData CreatePingRequestData(Node node, Auditable audit)
		{
			audit.Node = node;

			var requestOverrides = new RequestConfiguration
			{
				PingTimeout = PingTimeout,
				RequestTimeout = PingTimeout,
				BasicAuthenticationCredentials = _settings.BasicAuthenticationCredentials,
				EnableHttpPipelining = RequestConfiguration?.EnableHttpPipelining ?? _settings.HttpPipeliningEnabled,
				ForceNode = RequestConfiguration?.ForceNode
			};
			IRequestParameters requestParameters = new RootNodeInfoRequestParameters();
			requestParameters.RequestConfiguration = requestOverrides;

			var data = new RequestData(HttpMethod.HEAD, "/", null, _settings, requestParameters, _memoryStreamFactory) { Node = node };
			audit.Path = data.PathAndQuery;
			return data;
		}

		private static void ThrowBadAuthPipelineExceptionWhenNeeded(IApiCallDetails details, IElasticsearchResponse response = null)
		{
			if (details?.HttpStatusCode == 401)
				throw new PipelineException(PipelineFailure.BadAuthentication, details.OriginalException)
				{
					Response = response,
					ApiCall = details
				};
		}

		private void LazyAuditable(AuditEvent e, Node n)
		{
			using (new Auditable(e, AuditTrail, _dateTimeProvider) { Node = n }) { }
			;
		}

		private RequestData CreateSniffRequestData(Node node) =>
			new RequestData(HttpMethod.GET, SniffPath, null, _settings, SniffParameters, _memoryStreamFactory)
			{
				Node = node
			};

		protected virtual void Dispose() { }
	}
}
