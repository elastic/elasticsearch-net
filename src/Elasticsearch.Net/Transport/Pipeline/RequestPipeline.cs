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
		private readonly IConnectionConfigurationValues _settings;
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IMemoryStreamFactory _memoryStreamFactory;

		private IRequestParameters RequestParameters { get; }
		private IRequestConfiguration RequestConfiguration { get; }

		public DateTime StartedOn { get; }

		public List<Audit> AuditTrail { get; } = new List<Audit>();
		private int _retried = 0;
		public int Retried => _retried;

		public RequestPipeline(
			IConnectionConfigurationValues configurationValues,
			IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory,
			IRequestParameters requestParameters)
		{
			this._settings = configurationValues;
			this._connectionPool = this._settings.ConnectionPool;
			this._connection = this._settings.Connection;
			this._dateTimeProvider = dateTimeProvider;
			this._memoryStreamFactory = memoryStreamFactory;

			this.RequestParameters = requestParameters;
			this.RequestConfiguration = requestParameters?.RequestConfiguration;
			this.StartedOn = dateTimeProvider.Now();
		}

		public int MaxRetries =>
			this.RequestConfiguration?.ForceNode != null
			? 0
			: Math.Min(this.RequestConfiguration?.MaxRetries ?? this._settings.MaxRetries.GetValueOrDefault(int.MaxValue), this._connectionPool.MaxRetries);

		private bool RequestDisabledSniff => this.RequestConfiguration != null && (this.RequestConfiguration.DisableSniff ?? false);

		public bool FirstPoolUsageNeedsSniffing =>
			!this.RequestDisabledSniff
				&& this._connectionPool.SupportsReseeding && this._settings.SniffsOnStartup && !this._connectionPool.SniffedOnStartup;

		public bool SniffsOnConnectionFailure =>
			!this.RequestDisabledSniff
				&& this._connectionPool.SupportsReseeding && this._settings.SniffsOnConnectionFault;

		public bool SniffsOnStaleCluster =>
			!this.RequestDisabledSniff
				&& this._connectionPool.SupportsReseeding && this._settings.SniffInformationLifeSpan.HasValue;

		public bool StaleClusterState
		{
			get
			{
				if (!SniffsOnStaleCluster) return false;
				// ReSharper disable once PossibleInvalidOperationException
				// already checked by SniffsOnStaleCluster
				var sniffLifeSpan = this._settings.SniffInformationLifeSpan.Value;

				var now = this._dateTimeProvider.Now();
				var lastSniff = this._connectionPool.LastUpdate;

				return sniffLifeSpan < (now - lastSniff);
			}
		}

		private bool PingDisabled(Node node) =>
			(this.RequestConfiguration?.DisablePing).GetValueOrDefault(false)
				|| this._settings.DisablePings || !this._connectionPool.SupportsPinging || !node.IsResurrected;

		TimeSpan PingTimeout =>
			 this.RequestConfiguration?.PingTimeout
			?? this._settings.PingTimeout
			?? (this._connectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

		TimeSpan RequestTimeout => this.RequestConfiguration?.RequestTimeout ?? this._settings.RequestTimeout;

		public bool IsTakingTooLong
		{
			get
			{
				var timeout = this._settings.MaxRetryTimeout.GetValueOrDefault(this.RequestTimeout);
				var now = this._dateTimeProvider.Now();

				//we apply a soft margin so that if a request timesout at 59 seconds when the maximum is 60 we also abort.
				var margin = (timeout.TotalMilliseconds / 100.0) * 98;
				var marginTimeSpan = TimeSpan.FromMilliseconds(margin);
				var timespanCall = (now - this.StartedOn);
				var tookToLong = timespanCall >= marginTimeSpan;
				return tookToLong;
			}
		}

		public bool Refresh { get; private set; }

		public bool DepleededRetries => this.Retried >= this.MaxRetries + 1 || this.IsTakingTooLong;

		private Auditable Audit(AuditEvent type) => new Auditable(type, this.AuditTrail, this._dateTimeProvider);

		private static string NoNodesAttemptedMessage = "No nodes were attempted, this can happen when a node predicate does not match any nodes";
		public void ThrowNoNodesAttempted(RequestData requestData, List<PipelineException> seenExceptions)
		{
			var clientException = new ElasticsearchClientException(PipelineFailure.NoNodesAttempted, NoNodesAttemptedMessage, (Exception) null);
			using(this.Audit(NoNodesAttempted))
				throw new UnexpectedElasticsearchClientException(clientException, seenExceptions)
				{
					Request  = requestData,
					AuditTrail = this.AuditTrail
				};
		}

		public void AuditCancellationRequested() => Audit(CancellationRequested).Dispose();

		public void MarkDead(Node node)
		{
			var deadUntil = this._dateTimeProvider.DeadTime(node.FailedAttempts, this._settings.DeadTimeout, this._settings.MaxDeadTimeout);
			node.MarkDead(deadUntil);
			this._retried++;
		}

		public void MarkAlive(Node node) => node.MarkAlive();

		public void FirstPoolUsage(SemaphoreSlim semaphore)
		{
			if (!this.FirstPoolUsageNeedsSniffing) return;
			if (!semaphore.Wait(this._settings.RequestTimeout))
			{
				if (this.FirstPoolUsageNeedsSniffing)
					throw new PipelineException(PipelineFailure.CouldNotStartSniffOnStartup, null);
				return;
			}

			if (!this.FirstPoolUsageNeedsSniffing)
			{
				semaphore.Release();
				return;
			}

			try
			{
				using (this.Audit(SniffOnStartup))
				{
					this.Sniff();
					this._connectionPool.SniffedOnStartup = true;
				}
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task FirstPoolUsageAsync(SemaphoreSlim semaphore, CancellationToken cancellationToken)
		{
			if (!this.FirstPoolUsageNeedsSniffing) return;
			var success = await semaphore.WaitAsync(this._settings.RequestTimeout, cancellationToken).ConfigureAwait(false);
			if (!success)
			{
				if(this.FirstPoolUsageNeedsSniffing)
					throw new PipelineException(PipelineFailure.CouldNotStartSniffOnStartup, null);
				return;
			}

			if (!this.FirstPoolUsageNeedsSniffing)
			{
				semaphore.Release();
				return;
			}
			try
			{
				using (this.Audit(SniffOnStartup))
				{
					await this.SniffAsync(cancellationToken).ConfigureAwait(false);
					this._connectionPool.SniffedOnStartup = true;
				}
			}
			finally
			{
				semaphore.Release();
			}
		}

		public void SniffOnStaleCluster()
		{
			if (!StaleClusterState) return;
			using (this.Audit(AuditEvent.SniffOnStaleCluster))
			{
				this.Sniff();
				this._connectionPool.SniffedOnStartup = true;
			}
		}

		public async Task SniffOnStaleClusterAsync(CancellationToken cancellationToken)
		{
			if (!StaleClusterState) return;
			using (this.Audit(AuditEvent.SniffOnStaleCluster))
			{
				await this.SniffAsync(cancellationToken).ConfigureAwait(false);
				this._connectionPool.SniffedOnStartup = true;
			}
		}

		public IEnumerable<Node> NextNode()
		{
			if (this.RequestConfiguration?.ForceNode != null)
			{
				yield return new Node(this.RequestConfiguration.ForceNode);
				yield break;
			}

			//This for loop allows to break out of the view state machine if we need to
			//force a refresh (after reseeding connectionpool). We have a hardcoded limit of only
			//allowing 100 of these refreshes per call
			var refreshed = false;
			for (var i = 0; i < 100; i++)
			{
				if (this.DepleededRetries) yield break;
				foreach (var node in this._connectionPool
					.CreateView(LazyAuditable)
					.TakeWhile(node => !this.DepleededRetries))
				{
					if (!this._settings.NodePredicate(node)) continue;
					yield return node;
					if (!this.Refresh) continue;
					this.Refresh = false;
					refreshed = true;
					break;
				}
				//unless a refresh was requested we will not iterate over more then a single view.
				//keep in mind refreshes are also still bound to overall maxretry count/timeout.
				if (!refreshed) break;
			}
		}

		private RequestData CreatePingRequestData(Node node, Auditable audit)
		{
			audit.Node = node;

			var requestOverrides = new RequestConfiguration
			{
				PingTimeout = this.PingTimeout,
				RequestTimeout = this.PingTimeout,
				BasicAuthenticationCredentials = this._settings.BasicAuthenticationCredentials,
				EnableHttpPipelining = this.RequestConfiguration?.EnableHttpPipelining ?? this._settings.HttpPipeliningEnabled,
				ForceNode = this.RequestConfiguration?.ForceNode
			};
			IRequestParameters requestParameters = new RootNodeInfoRequestParameters { };
			requestParameters.RequestConfiguration = requestOverrides;

			var data = new RequestData(HttpMethod.HEAD, "/", null, this._settings, requestParameters, this._memoryStreamFactory) { Node = node };
			audit.Path = data.PathAndQuery;
			return data;
		}

		public void Ping(Node node)
		{
			if (PingDisabled(node)) return;

			using (var audit = this.Audit(PingSuccess))
			{
				try
				{
					var pingData = CreatePingRequestData(node, audit);
					var response = this._connection.Request<VoidResponse>(pingData);
					ThrowBadAuthPipelineExceptionWhenNeeded(response);
					//ping should not silently accept bad but valid http responses
					if (!response.Success) throw new PipelineException(pingData.OnFailurePipelineFailure) { ApiCall = response };
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

			using (var audit = this.Audit(PingSuccess))
			{
				try
				{
					var pingData = CreatePingRequestData(node, audit);
					var response = await this._connection.RequestAsync<VoidResponse>(pingData, cancellationToken).ConfigureAwait(false);
					ThrowBadAuthPipelineExceptionWhenNeeded(response);
					//ping should not silently accept bad but valid http responses
					if (!response.Success) throw new PipelineException(pingData.OnFailurePipelineFailure) { ApiCall = response };
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

		private static void ThrowBadAuthPipelineExceptionWhenNeeded(IApiCallDetails details, IElasticsearchResponse response = null)
		{
			if (details?.HttpStatusCode == 401)
				throw new PipelineException(PipelineFailure.BadAuthentication, details.OriginalException)
				{
					Response = response,
					ApiCall = details
				};
		}

		public static string SniffPath => "_nodes/http,settings";
		private NodesInfoRequestParameters SniffParameters => new NodesInfoRequestParameters
		{
			Timeout = this.PingTimeout,
			FlatSettings = true
		};

		public IEnumerable<Node> SniffNodes => this._connectionPool
			.CreateView(LazyAuditable)
			.ToList()
			.OrderBy(n => n.MasterEligible ? n.Uri.Port : int.MaxValue);

		private void LazyAuditable(AuditEvent e, Node n)
		{
			using (new Auditable(e, this.AuditTrail, this._dateTimeProvider) { Node = n }) {};
		}

		public void SniffOnConnectionFailure()
		{
			if (!this.SniffsOnConnectionFailure) return;
			using (this.Audit(SniffOnFail))
				this.Sniff();
		}

		public async Task SniffOnConnectionFailureAsync(CancellationToken cancellationToken)
		{
			if (!this.SniffsOnConnectionFailure) return;
			using (this.Audit(SniffOnFail))
				await this.SniffAsync(cancellationToken).ConfigureAwait(false);
		}

		public void Sniff()
		{
			var exceptions = new List<Exception>();
			foreach (var node in this.SniffNodes)
			{
				using (var audit = this.Audit(SniffSuccess))
				{
					audit.Node = node;
					try
					{
						var requestData = CreateSniffRequestData(node);
						audit.Path = requestData.PathAndQuery;
						var response = this._connection.Request<SniffResponse>(requestData);
						ThrowBadAuthPipelineExceptionWhenNeeded(response);
						//sniff should not silently accept bad but valid http responses
						if (!response.Success) throw new PipelineException(requestData.OnFailurePipelineFailure) { ApiCall = response };
						var nodes = response.ToNodes(this._connectionPool.UsingSsl);
						this._connectionPool.Reseed(nodes);
						this.Refresh = true;
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
			throw new PipelineException(PipelineFailure.SniffFailure, new AggregateException(exceptions));
		}

		public async Task SniffAsync(CancellationToken cancellationToken)
		{
			var exceptions = new List<Exception>();
			foreach (var node in this.SniffNodes)
			{
				using (var audit = this.Audit(SniffSuccess))
				{
					audit.Node = node;
					try
					{
						var requestData = CreateSniffRequestData(node);
						audit.Path = requestData.PathAndQuery;
						var response = await this._connection.RequestAsync<SniffResponse>(requestData, cancellationToken).ConfigureAwait(false);
						ThrowBadAuthPipelineExceptionWhenNeeded(response);
						//sniff should not silently accept bad but valid http responses
						if (!response.Success) throw new PipelineException(requestData.OnFailurePipelineFailure) { ApiCall = response };
						this._connectionPool.Reseed(response.ToNodes(this._connectionPool.UsingSsl));
						this.Refresh = true;
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
			throw new PipelineException(PipelineFailure.SniffFailure, new AggregateException(exceptions));
		}

		private RequestData CreateSniffRequestData(Node node) =>
			new RequestData(HttpMethod.GET, SniffPath, null, this._settings, this.SniffParameters, this._memoryStreamFactory)
			{
				Node = node
			};

		public TResponse CallElasticsearch<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new()
		{
			using (var audit = this.Audit(HealthyResponse))
			{
				audit.Node = requestData.Node;
				audit.Path = requestData.PathAndQuery;

				TResponse response = null;
				try
				{
					response = this._connection.Request<TResponse>(requestData);
					response.ApiCall.AuditTrail = this.AuditTrail;
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
			using (var audit = this.Audit(HealthyResponse))
			{
				audit.Node = requestData.Node;
				audit.Path = requestData.PathAndQuery;

				TResponse response = null;
				try
				{
					response = await this._connection.RequestAsync<TResponse>(requestData, cancellationToken).ConfigureAwait(false);
					response.ApiCall.AuditTrail = this.AuditTrail;
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

		public void BadResponse<TResponse>(ref TResponse response, IApiCallDetails callDetails, RequestData data, ElasticsearchClientException exception)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (response == null)
			{
				//make sure we copy over the error body in case we disabled direct streaming.
				var s = callDetails?.ResponseBodyInBytes == null ? Stream.Null : new MemoryStream(callDetails.ResponseBodyInBytes);
				var m = callDetails?.ResponseMimeType ?? RequestData.MimeType;
				response = ResponseBuilder.ToResponse<TResponse>(data, exception, callDetails?.HttpStatusCode, null, s, m);
			}

			response.ApiCall.AuditTrail = this.AuditTrail;
		}

		public ElasticsearchClientException CreateClientException<TResponse>(
			TResponse response, IApiCallDetails callDetails, RequestData data, List<PipelineException> pipelineExceptions
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (callDetails?.Success ?? false) return null;
			var innerException = pipelineExceptions.HasAny() ? new AggregateException(pipelineExceptions) : callDetails?.OriginalException;

			var statusCode = callDetails?.HttpStatusCode != null ? callDetails.HttpStatusCode.Value.ToString() : "unknown";
			var resource = callDetails == null
				? "unknown resource"
				: $"Status code {statusCode} from: {callDetails.HttpMethod} {callDetails.Uri.PathAndQuery}";


			var exceptionMessage = innerException?.Message ?? $"Request failed to execute";

			var pipelineFailure = data.OnFailurePipelineFailure;
			if (pipelineExceptions.HasAny())
				pipelineFailure = pipelineExceptions.Last().FailureReason;

			if (this.IsTakingTooLong)
			{
				pipelineFailure = PipelineFailure.MaxTimeoutReached;
				this.Audit(MaxTimeoutReached);
				exceptionMessage = "Maximum timeout reached while retrying request";
			}
			else if (this.Retried >= this.MaxRetries && this.MaxRetries > 0)
			{
				pipelineFailure = PipelineFailure.MaxRetriesReached;
				this.Audit(MaxRetriesReached);
				exceptionMessage = "Maximum number of retries reached";
			}

			exceptionMessage += $". Call: {resource}";
			if (response != null && response.TryGetServerErrorReason(out var reason))
				exceptionMessage += $". ServerError: {reason}";


			var clientException = new ElasticsearchClientException(pipelineFailure, exceptionMessage, innerException)
			{
				Request = data,
				Response = callDetails,
				AuditTrail = this.AuditTrail
			};

			return clientException;
		}

		void IDisposable.Dispose() => this.Dispose();

		protected virtual void Dispose() { }
	}
}
