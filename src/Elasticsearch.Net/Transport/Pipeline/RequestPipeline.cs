using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Connection.Sniff;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{

	public class RequestPipeline : IRequestPipeline
	{
		private readonly IConnectionConfigurationValues _settings;
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IMemoryStreamFactory _memoryStreamFactory;
		private readonly CancellationToken _cancellationToken;

		public IRequestParameters RequestParameters { get; }
		public IRequestConfiguration RequestConfiguration { get; }
		public DateTime StartedOn { get; }
		public virtual DateTime CompletedOn { get; }

		public List<Audit> AuditTrail { get; } = new List<Audit>();

		public Node CurrentNode { get; private set; }

		public int MaxRetries { get; }
		private int _retried = 0;
		public int Retried => _retried;

		private int? _cursor = null;

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

			this.MaxRetries = this._settings.MaxRetries ?? this._connectionPool.MaxRetries;
			this.RequestParameters = requestParameters;
			this.RequestConfiguration = requestParameters?.RequestConfiguration;
			this._cancellationToken = this.RequestConfiguration?.CancellationToken ?? CancellationToken.None;
			this.StartedOn = dateTimeProvider.Now();
		}

		private RequestData CreateRequestData(HttpMethod method, string path, PostData<object> postData, IRequestConfiguration requestOverrides)
		{
			var requestData = new RequestData(method, path, postData, this._settings, requestOverrides, this._memoryStreamFactory);
			requestData.Uri = this.CurrentNode.CreatePath(requestData.Path);
			return requestData;
		}

		public void Dispose()
		{
		}

		public void MarkDead()
		{
			var node = this.CurrentNode;
			var deadUntil = this._dateTimeProvider.DeadTime(node.FailedAttempts, this._settings.DeadTimeout, this._settings.MaxDeadTimeout);
			node.MarkDead(deadUntil);
		}

		public void MarkAlive() => this.CurrentNode.MarkAlive();

		public bool FirstPoolUsageNeedsSniffing =>
			this._connectionPool.SupportsReseeding && this._settings.SniffsOnStartup && !this._connectionPool.SniffedOnStartup;

		public void FirstPoolUsage(SemaphoreSlim semaphore)
		{
			if (!this.FirstPoolUsageNeedsSniffing) return;
			if (!semaphore.Wait(this._settings.Timeout))
				throw new ElasticsearchException(PipelineFailure.CouldNotStartSniffOnStartup, (Exception)null);
			if (!this.FirstPoolUsageNeedsSniffing) return;
			try
			{
				using (this.Audit(AuditEvent.SniffOnStartup))
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

		public async Task FirstPoolUsageAsync(SemaphoreSlim semaphore)
		{
			if (!this.FirstPoolUsageNeedsSniffing) return;
			var success = await semaphore.WaitAsync(this._settings.Timeout, this._cancellationToken);
			if (!success)
				throw new ElasticsearchException(PipelineFailure.CouldNotStartSniffOnStartup, (Exception)null);

			if (!this.FirstPoolUsageNeedsSniffing) return;
			try
			{
				using (this.Audit(AuditEvent.SniffOnStartup))
				{
					await this.SniffAsync();
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
			if (!OutOfDateClusterInformation) return;
			using (this.Audit(AuditEvent.SniffOnStaleCluster))
			{
				this.Sniff();
				this._connectionPool.SniffedOnStartup = true;
			}
		}

		public async Task SniffOnStaleClusterAsync()
		{
			if (!OutOfDateClusterInformation) return;
			using (this.Audit(AuditEvent.SniffOnStaleCluster))
			{
				this.Sniff();
				this._connectionPool.SniffedOnStartup = true;
			}
		}

		public bool OutOfDateClusterInformation
		{
			get
			{
				if (!this._connectionPool.SupportsReseeding) return false;
				var sniffLifeSpan = this._settings.SniffInformationLifeSpan;
				if (!sniffLifeSpan.HasValue) return false;

				var now = this._dateTimeProvider.Now();
				var lastSniff = this._connectionPool.LastUpdate;

				return !lastSniff.HasValue || (lastSniff.HasValue && sniffLifeSpan.Value < (now - lastSniff.Value));
			}
		}

		public bool IsTakingTooLong
		{
			get
			{
				var timeout = this._settings.MaxRetryTimeout.GetValueOrDefault(this._settings.Timeout);
				var now = this._dateTimeProvider.Now();

				//we apply a soft margin so that if a request timesout at 59 seconds when the maximum is 60 we also abort.
				var margin = (timeout.TotalMilliseconds / 100.0) * 98;
				var marginTimeSpan = TimeSpan.FromMilliseconds(margin);
				var timespanCall = (now - this.StartedOn);
				var tookToLong = timespanCall >= marginTimeSpan;
				return tookToLong;
			}
		}

		public bool NextNode()
		{
			if (this.Retried >= this.MaxRetries + 1) return false;

			var node = this._connectionPool.GetNext(_cursor, out _cursor);
			this.CurrentNode = node;
			return true;
		}

		public void BadResponse(IApiCallDetails response, List<ElasticsearchException> seenExceptions)
		{
			this.MarkDead();
			var currentRetryCount = this._retried;
			this._retried++;

			if (!this.IsTakingTooLong || currentRetryCount < this.MaxRetries)
				return;

			Exception seenAggregate = seenExceptions.HasAny() ? new AggregateException(seenExceptions) : null;

			if (this.IsTakingTooLong) throw new ElasticsearchException(PipelineFailure.RetryTimeout, response, seenAggregate);
			throw new ElasticsearchException(PipelineFailure.RetryMaximum, response, seenAggregate);
		}

		TimeSpan PingTimeout =>
			 this.RequestConfiguration?.ConnectTimeout
			?? this._settings.PingTimeout
			?? (this._connectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

		private RequestData PingRequestData(Auditable audit)
		{
			audit.Node = this.CurrentNode;

			var requestOverrides = this.RequestConfiguration ?? new RequestConfiguration { };
			requestOverrides.ConnectTimeout = requestOverrides.RequestTimeout = PingTimeout;

			var requestData = CreateRequestData(HttpMethod.HEAD, "/", null, requestOverrides);
			return requestData;
		}

		public void Ping()
		{
			if (this._settings.DisablePings || !this._connectionPool.SupportsPinging || !this.CurrentNode.IsResurrected) return;

			using (var audit = this.Audit(AuditEvent.PingSuccess))
			{
				try
				{
					var requestData = PingRequestData(audit);
					this._connection.Request<VoidResponse>(requestData);
				}
				catch
				{
					audit.Event = AuditEvent.PingFailure;
					throw;
				}
			}
		}

		public async Task PingAsync()
		{
			if (this._settings.DisablePings || !this._connectionPool.SupportsPinging || !this.CurrentNode.IsResurrected) return;

			using (var audit = this.Audit(AuditEvent.PingSuccess))
			{
				try
				{
					var requestData = PingRequestData(audit);
					await this._connection.RequestAsync<VoidResponse>(requestData);
				}
				catch
				{
					audit.Event = AuditEvent.PingFailure;
					throw;
				}
			}
		}

		public static void VoidCallHandler(ElasticsearchResponse<Stream> response) { }

		private string SniffPath => "_nodes/_all/settings?flat_settings&timeout=" + this.PingTimeout;

		public IEnumerable<Node> SniffNodes => this._connectionPool.Nodes.OrderByDescending(n => n.MasterEligable ? 3 : 0);

		public void Sniff()
		{
			var path = this.SniffPath;
			var exceptions = new List<ElasticsearchException>();
			foreach (var node in this.SniffNodes)
			{
				using (var audit = this.Audit(AuditEvent.SniffSuccess))
				{
					audit.Node = node;
					try
					{
						var requestData = new RequestData(HttpMethod.GET, path, null, this._settings, this._memoryStreamFactory);
						requestData.Uri = node.CreatePath(requestData.Path);

						var response = this._connection.Request<SniffResponse>(requestData);
						var nodes = response.Body.ToNodes(this._connectionPool.UsingSsl);
						this._connectionPool.Reseed(nodes);
						return;
					}
					catch (ElasticsearchException e) when (e.Cause == PipelineFailure.BadAuthentication) //unrecoverable
					{
						audit.Event = AuditEvent.SniffFailure;
						e.RethrowKeepingStackTrace();
						continue;
					}
					catch (ElasticsearchException e)
					{
						audit.Event = AuditEvent.SniffFailure;
						exceptions.Add(e);
						continue;
					}
				}
			}
			throw new ElasticsearchException(PipelineFailure.BadSniff, new AggregateException(exceptions));
		}

		public async Task SniffAsync()
		{
			var path = this.SniffPath;
			var exceptions = new List<ElasticsearchException>();
			foreach (var node in this.SniffNodes)
			{
				using (var audit = this.Audit(AuditEvent.SniffSuccess))
				{
					audit.Node = node;
					try
					{
						var requestData = new RequestData(HttpMethod.GET, path, null, this._settings, this._memoryStreamFactory);
						requestData.Uri = node.CreatePath(requestData.Path);

						var response = await this._connection.RequestAsync<SniffResponse>(requestData);
						this._connectionPool.Reseed(response.Body.ToNodes(this._connectionPool.UsingSsl));
						return;
					}
					catch (ElasticsearchException e) when (e.Cause == PipelineFailure.BadAuthentication) //unrecoverable
					{
						audit.Event = AuditEvent.SniffFailure;
						e.RethrowKeepingStackTrace();
						continue;
					}
					catch (ElasticsearchException e)
					{
						audit.Event = AuditEvent.SniffFailure;
						exceptions.Add(e);
						continue;
					}
				}
			}
			throw new ElasticsearchException(PipelineFailure.BadSniff, new AggregateException(exceptions));
		}

		public ElasticsearchResponse<TReturn> CallElasticsearch<TReturn>(RequestData requestData) where TReturn : class
		{
			using (var audit = this.Audit(AuditEvent.HealhyResponse))
			{
				audit.Node = this.CurrentNode;
				audit.Path = requestData.Path;
				var uri = this.CurrentNode.CreatePath(requestData.Path);
				requestData.Uri = uri;
				ElasticsearchResponse<TReturn> response = null;
				try
				{
					response = this._connection.Request<TReturn>(requestData);
					response.AuditTrail = this.AuditTrail;
					if (!response.SuccessOrKnownError)
						audit.Event = AuditEvent.BadResponse;
					return response;
				}
				catch (Exception e)
				{
					(response as ElasticsearchResponse<Stream>)?.Body?.Dispose();
					audit.Event = AuditEvent.BadResponse;
					throw new ElasticsearchException(PipelineFailure.BadResponse, e);
				}
			}
		}

		public async Task<ElasticsearchResponse<TReturn>> CallElasticsearchAsync<TReturn>(RequestData requestData) where TReturn : class
		{
			using (var audit = this.Audit(AuditEvent.HealhyResponse))
			{
				audit.Node = this.CurrentNode;
				audit.Path = requestData.Path;
				var uri = this.CurrentNode.CreatePath(requestData.Path);
				requestData.Uri = uri;
				ElasticsearchResponse<TReturn> response = null;
				try
				{
					response = await this._connection.RequestAsync<TReturn>(requestData);
					response.AuditTrail = this.AuditTrail;
					if (!response.SuccessOrKnownError)
						audit.Event = AuditEvent.BadResponse;
					return response;
				}
				catch (Exception e)
				{
					(response as ElasticsearchResponse<Stream>)?.Body?.Dispose();
					audit.Event = AuditEvent.BadResponse;
					throw new ElasticsearchException(PipelineFailure.BadResponse, e);
				}
			}
		}

		private Auditable Audit(AuditEvent type) => new Auditable(type, this.AuditTrail, this._dateTimeProvider);
	}
}