using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public class RequestPipeline: IRequestPipeline
	{
		private readonly IConnectionConfigurationValues _settings;
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IMemoryStreamFactory _memoryStreamFactory;

		//todo these two terms are too similar come up with a better name
		public IRequestParameters RequestParameters { get; }
		public IRequestConfiguration RequestConfiguration { get; }

		public Node CurrentNode { get; private set; }
		public DateTime StartedOn { get; private set; }

		public int MaxRetries { get; }
		private int _retried = 0;
		public int Retried => _retried;

		private int? _cursor = null;

		//TODO static property on ConnectionConfiguration?
		readonly TimeSpan DefaultPingTimeout = TimeSpan.FromSeconds(1);
		readonly TimeSpan SslDefaultPingTimeout = TimeSpan.FromSeconds(2);

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
			this.StartedOn = dateTimeProvider.Now();
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

		public bool FirstPoolUsage()
		{
			if (!this._settings.SniffsOnStartup || !this._connectionPool.AcceptsUpdates || this._connectionPool.SniffedOnStartup)
				return false;

			this.Sniff();

			return true;
		}

		public async Task<bool> FirstPoolUsageAsync()
		{
			if (!this._settings.SniffsOnStartup || !this._connectionPool.AcceptsUpdates || this._connectionPool.SniffedOnStartup)
				return false;

			await this.SniffAsync();

			return true;
		}

		public void OutOfDateClusterInformation()
		{
			var sniffLifeSpan = this._settings.SniffInformationLifeSpan;
			if (!sniffLifeSpan.HasValue) return;

			var now = this._dateTimeProvider.Now();
			var lastSniff = this._connectionPool.LastUpdate;

			if (!lastSniff.HasValue || (lastSniff.HasValue && sniffLifeSpan.Value < (now - lastSniff.Value)))
				this.Sniff();
		}

		public async Task OutOfDateClusterInformationAsync()
		{
			var sniffLifeSpan = this._settings.SniffInformationLifeSpan;
			if (!sniffLifeSpan.HasValue) return;

			var now = this._dateTimeProvider.Now();
			var lastSniff = this._connectionPool.LastUpdate;

			if (!lastSniff.HasValue || (lastSniff.HasValue && sniffLifeSpan.Value < (now - lastSniff.Value)))
				await this.SniffAsync();
		}
		private bool TookToLong()
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

		public bool NextNode()
		{
			if (this.Retried >= this.MaxRetries + 1) return false;

			//TODO move this out of GetNext;
			int newCursor;
			var node = this._connectionPool.GetNext(_cursor, out newCursor);
			this._cursor = newCursor;
			//todo make connectionpool return Node
			this.CurrentNode = node;
			return true;
		}

		public void BadResponse(IElasticsearchResponse response)
		{
			this.MarkDead();
			var currentRetryCount = this._retried;
			this._retried++;
			
			var tookToLong = this.TookToLong();
			if (!tookToLong || currentRetryCount < this.MaxRetries)
				return;
			
			if (tookToLong) throw new ElasticsearchException(PipelineFailure.RetryMaximum, response);
			throw new ElasticsearchException(PipelineFailure.RetryMaximum, response);
		}

		TimeSpan PingTimeout =>
			 this.RequestConfiguration?.ConnectTimeout ?? this._settings.PingTimeout ?? (this._connectionPool.UsingSsl ? SslDefaultPingTimeout : DefaultPingTimeout);

		public void Ping()
		{
			if (this._settings.DisablePings || !this._connectionPool.SupportsPinging || !this.CurrentNode.IsResurrected) return;

			//TODO merge with this.RequestConfiguration
			var requestOverrides = new RequestConfiguration { ConnectTimeout = PingTimeout, RequestTimeout = PingTimeout };

			var requestData = new RequestData(HttpMethod.HEAD, "/", null, this._settings, requestOverrides, this._memoryStreamFactory);
			requestData.Uri = this.CurrentNode.CreatePath(requestData.Path);
			this.Call(PipelineFailure.BadPing, () => this._connection.Request<VoidResponse>(requestData));
		}

		public async Task PingAsync()
		{
			if (this._settings.DisablePings || !this._connectionPool.SupportsPinging || !this.CurrentNode.IsResurrected) return;

			//TODO merge with this.RequestConfiguration
			var requestOverrides = new RequestConfiguration { ConnectTimeout = PingTimeout, RequestTimeout = PingTimeout };
			var requestData = new RequestData(HttpMethod.HEAD, "/", null, this._settings, requestOverrides, this._memoryStreamFactory);
			requestData.Uri = this.CurrentNode.CreatePath(requestData.Path);
			await this.CallAsync(PipelineFailure.BadPing, () => this._connection.RequestAsync<VoidResponse>(requestData));

		}
		public static void VoidCallHandler(ElasticsearchResponse<Stream> response) { }

		ElasticsearchResponse<TReturn> Call<TReturn>(PipelineFailure failure,  Func<ElasticsearchResponse<TReturn>> call)
		{
			ElasticsearchResponse<TReturn> response = null;
			try
			{
				response = call();
			}
			catch (Exception e)
			{
				(response as ElasticsearchResponse<Stream>)?.Response?.Dispose();
				throw new ElasticsearchException(failure, e);
			}
			return response;
		}

		async Task<ElasticsearchResponse<TReturn>> CallAsync<TReturn>(PipelineFailure failure,  Func<Task<ElasticsearchResponse<TReturn>>> call)
		{
			ElasticsearchResponse<TReturn> response = null;
			try
			{
				response = await call();
			}
			catch (Exception e)
			{
				(response as ElasticsearchResponse<Stream>)?.Response?.Dispose();
				throw new ElasticsearchException(failure, e);
			}
			return response;
		}

		void Sniff()
		{
			var path = "_nodes/_all/clear?timeout=" + this.PingTimeout;
			var exceptions = new List<ElasticsearchException>();
			foreach (var node in this._connectionPool.Nodes)
			{
				try
				{
					var requestData = new RequestData(HttpMethod.GET, path, null, this._settings, this._memoryStreamFactory);
					requestData.Uri = this.CurrentNode.CreatePath(requestData.Path);

					var response = this.Call(PipelineFailure.BadResponse, () => this._connection.Request<Stream>(requestData));
					using (response.Response)
					{
						var listOfNodes = Sniffer.FromStream(response, response.Response, this._settings.Serializer);
						if (!listOfNodes.HasAny())
							throw new ElasticsearchException(PipelineFailure.BadResponse, response);

						this._connectionPool.Update(listOfNodes.Select(n=>new Node(n)));
					}
				}
				catch (ElasticsearchException e) when (e.Cause == PipelineFailure.BadAuthentication) //unrecoverable
				{
					e.RethrowKeepingStackTrace();
					continue;
				}
				catch (ElasticsearchException e)
				{
					exceptions.Add(e);
					continue;
				}
			}
			throw new ElasticsearchException(PipelineFailure.BadSniff, new AggregateException(exceptions));
		}

		async Task SniffAsync()
		{
			var path = "_nodes/_all/clear?timeout=" + this.PingTimeout;
			var exceptions = new List<ElasticsearchException>();
			foreach (var node in this._connectionPool.Nodes)
			{
				try
				{
					var requestData = new RequestData(HttpMethod.GET, path, null, this._settings, this._memoryStreamFactory);
					requestData.Uri = this.CurrentNode.CreatePath(requestData.Path);

					var response = await this.CallAsync(PipelineFailure.BadResponse, () => this._connection.RequestAsync<Stream>(requestData));
					using (response.Response)
					{
						var listOfNodes = Sniffer.FromStream(response, response.Response, this._settings.Serializer);
						if (!listOfNodes.HasAny())
							throw new ElasticsearchException(PipelineFailure.BadResponse, response);

						this._connectionPool.Update(listOfNodes.Select(n=>new Node(n)));
					}
				}
				catch (ElasticsearchException e) when (e.Cause == PipelineFailure.BadAuthentication) //unrecoverable
				{
					e.RethrowKeepingStackTrace();
					continue;
				}
				catch (ElasticsearchException e)
				{
					exceptions.Add(e);
					continue;
				}
			}
			throw new ElasticsearchException(PipelineFailure.BadSniff, new AggregateException(exceptions));
		}

		public ElasticsearchResponse<TReturn> CallElasticsearch<TReturn>(RequestData requestData)
		{
			var uri = this.CurrentNode.CreatePath(requestData.Path);
			requestData.Uri = uri;
			return this.Call(PipelineFailure.BadResponse, () => this._connection.Request<TReturn>(requestData));
		}

		public async Task<ElasticsearchResponse<TReturn>> CallElasticsearchAsync<TReturn>(RequestData requestData)
		{
			var uri = this.CurrentNode.CreatePath(requestData.Path);
			requestData.Uri = uri;
			return await this.CallAsync(PipelineFailure.BadResponse, () => this._connection.RequestAsync<TReturn>(requestData));
		}
	}
}