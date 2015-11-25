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
using Elasticsearch.Net.Extensions;

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
			this._cancellationToken = this.RequestConfiguration?.CancellationToken ?? CancellationToken.None;
			this.StartedOn = dateTimeProvider.Now();
		}

		public int MaxRetries => Math.Min(this._settings.MaxRetries.GetValueOrDefault(int.MaxValue), this._connectionPool.MaxRetries);

		public bool FirstPoolUsageNeedsSniffing =>
			this._connectionPool.SupportsReseeding && this._settings.SniffsOnStartup && !this._connectionPool.SniffedOnStartup;

		public bool SniffsOnConnectionFailure => this._connectionPool.SupportsReseeding && this._settings.SniffsOnConnectionFault;

		public bool SniffsOnStaleCluster => this._connectionPool.SupportsReseeding && this._settings.SniffInformationLifeSpan.HasValue;

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

		TimeSpan PingTimeout =>
			 this.RequestConfiguration?.ConnectTimeout
			?? this._settings.PingTimeout
			?? (this._connectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

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
		
		public bool Refresh { get; private set; }

		public bool DepleededRetries => this.Retried >= this.MaxRetries + 1 || this.IsTakingTooLong;

		private Auditable Audit(AuditEvent type) => new Auditable(type, this.AuditTrail, this._dateTimeProvider);

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
			if (!StaleClusterState) return;
			using (this.Audit(AuditEvent.SniffOnStaleCluster))
			{
				this.Sniff();
				this._connectionPool.SniffedOnStartup = true;
			}
		}

		public async Task SniffOnStaleClusterAsync()
		{
			if (!StaleClusterState) return;
			using (this.Audit(AuditEvent.SniffOnStaleCluster))
			{
				await this.SniffAsync();
				this._connectionPool.SniffedOnStartup = true;
			}
		}

		public IEnumerable<Node> NextNode()
		{
			//This for loop allows to break out of the view state machine if we need to 
			//force a refresh (after reseeding connectionpool). We have a hardcoded limit of only
			//allowing 100 of these refreshes per call
			var refreshed = false;
			for(var i = 0; i < 100; i++)
			{
				if (this.DepleededRetries) yield break;
				foreach (var node in this._connectionPool.CreateView().TakeWhile(node => !this.DepleededRetries))
				{
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

			var requestOverrides = this.RequestConfiguration ?? new RequestConfiguration { };
			requestOverrides.ConnectTimeout = requestOverrides.RequestTimeout = PingTimeout;

			return new RequestData(HttpMethod.HEAD, "/", null, this._settings, requestOverrides, this._memoryStreamFactory) { Node = node };
		}

		public void Ping(Node node)
		{
			if (this._settings.DisablePings || !this._connectionPool.SupportsPinging || !node.IsResurrected) return;

			using (var audit = this.Audit(AuditEvent.PingSuccess))
			{
				try
				{
					var pingData = CreatePingRequestData(node, audit);
					this._connection.Request<VoidResponse>(pingData);
				}
				catch
				{
					audit.Event = AuditEvent.PingFailure;
					if (this.SniffsOnConnectionFailure) this.Sniff();
					throw;
				}
			}
		}

		public async Task PingAsync(Node node)
		{
			if (this._settings.DisablePings || !this._connectionPool.SupportsPinging || !node.IsResurrected) return;

			using (var audit = this.Audit(AuditEvent.PingSuccess))
			{
				try
				{
					var pingData = CreatePingRequestData(node, audit);
					await this._connection.RequestAsync<VoidResponse>(pingData);
				}
				catch
				{
					audit.Event = AuditEvent.PingFailure;
					if (this.SniffsOnConnectionFailure) await this.SniffAsync();
					throw;
				}
			}
		}

		private string SniffPath => "_nodes/_all/settings?flat_settings&timeout=" + this.PingTimeout;

		public IEnumerable<Node> SniffNodes => this._connectionPool.CreateView().ToList().OrderBy(n =>  n.MasterEligable ? n.Uri.Port : int.MaxValue);

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
						var requestData = new RequestData(HttpMethod.GET, path, null, this._settings, this._memoryStreamFactory) { Node = node };
						var response = this._connection.Request<SniffResponse>(requestData);
						var nodes = response.Body.ToNodes(this._connectionPool.UsingSsl);
						this._connectionPool.Reseed(nodes);
						this.Refresh = true;
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
						var requestData = new RequestData(HttpMethod.GET, path, null, this._settings, this._memoryStreamFactory) { Node = node };
						var response = await this._connection.RequestAsync<SniffResponse>(requestData);
						this._connectionPool.Reseed(response.Body.ToNodes(this._connectionPool.UsingSsl));
						this.Refresh = true;
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
			using (var audit = this.Audit(AuditEvent.HealthyResponse))
			{
				audit.Node = requestData.Node;
				audit.Path = requestData.Path;

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
					if (this.SniffsOnConnectionFailure) this.Sniff();
					throw new ElasticsearchException(PipelineFailure.BadResponse, e);
				}
			}
		}

		public async Task<ElasticsearchResponse<TReturn>> CallElasticsearchAsync<TReturn>(RequestData requestData) where TReturn : class
		{
			using (var audit = this.Audit(AuditEvent.HealthyResponse))
			{
				audit.Node = requestData.Node;
				audit.Path = requestData.Path;

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
					if (this.SniffsOnConnectionFailure) await this.SniffAsync();
					throw new ElasticsearchException(PipelineFailure.BadResponse, e);
				}
			}
		}

		public void BadResponse<TReturn>(ref ElasticsearchResponse<TReturn> response, RequestData data, List<ElasticsearchException> seenExceptions)
			where TReturn : class
		{
			var pipelineFailure = PipelineFailure.BadResponse;
			if (this.IsTakingTooLong) pipelineFailure = PipelineFailure.RetryTimeout;
			if (this.Retried >= this.MaxRetries) pipelineFailure = PipelineFailure.RetryMaximum;

			Exception seenAggregate = seenExceptions.HasAny() ? new AggregateException(seenExceptions) : null;
			if (response == null)
				response = data.CreateResponse<TReturn>(new ElasticsearchException(pipelineFailure, seenAggregate));
			response.AuditTrail = this.AuditTrail;
		}


		public void Dispose()
		{
		}
	}
}