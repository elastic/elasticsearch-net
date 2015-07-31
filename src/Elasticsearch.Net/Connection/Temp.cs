using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Connection.Configuration;
using PurifyNet;
using System.IO;

namespace Elasticsearch.Net.Connection
{
	public class NewTransport : ITransport
	{
		//TODO discuss which should be public
		public IElasticsearchSerializer Serializer { get; }
		public IConnectionConfigurationValues Settings { get; }
		public IConnection Connection { get; }
		public IConnectionPool ConnectionPool { get; }
		public IDateTimeProvider DateTimeProvider { get; }
		public IMemoryStreamProvider MemoryStreamProvider { get; }

		public NewTransport(
			IConnectionConfigurationValues configurationValues,
			IConnection connection = null,
			IElasticsearchSerializer serializer = null,
			IDateTimeProvider dateTimeProvider = null,
			IMemoryStreamProvider memoryStreamProvider = null
			)
		{
			configurationValues.ThrowIfNull(nameof(configurationValues));
			configurationValues.ConnectionPool.ThrowIfNull(nameof(configurationValues.ConnectionPool));

			this.Settings = configurationValues;
			this.Connection = connection ?? new HttpConnection(configurationValues);
			this.Serializer = serializer ?? new ElasticsearchDefaultSerializer();
			this.ConnectionPool = this.Settings.ConnectionPool;

			this.DateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
			this.MemoryStreamProvider = memoryStreamProvider ?? new MemoryStreamProvider();

		}

		public ElasticsearchResponse<T> DoRequest<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			using (var pipeline = new RequestPipeline<T>(this.Connection, this.Settings, this.DateTimeProvider, requestParameters))
			{
				if (!pipeline.FirstPoolUsage())
					pipeline.OutOfDateClusterInformation();

				while (pipeline.NextNode())
				{
					var success = false;
					try
					{
						pipeline.Ping();
						pipeline.CallElasticsearch(method, path, data, requestParameters);
						success = true;
					}
					catch (ElasticsearchException exception) when (!exception.Recoverable)
					{
						pipeline.CurrentNode.IsAlive = false;
						exception.RethrowKeepingStackTrace();
					}
					if (success && (pipeline.Result?.Success).GetValueOrDefault(false))
					{
						pipeline.CurrentNode.IsAlive = true;
						return pipeline.TypedResult;
					}

					pipeline.BadResponse();
				}
				return pipeline.ReturnInvalidResponseOrThrow();
			}
		}

		public async Task<ElasticsearchResponse<T>> DoRequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			using (var pipeline = new RequestPipeline<T>(this.Connection, this.Settings, this.DateTimeProvider, requestParameters))
			{
				if (await pipeline.FirstPoolUsageAsync())
					await pipeline.OutOfDateClusterInformationAsync();

				while (pipeline.NextNode())
				{
					var success = false;
					try
					{
						await pipeline.PingAsync();
						await pipeline.CallElasticsearchAsync(method, path, data, requestParameters);
						success = true;
					}
					catch (ElasticsearchException exception) when (!exception.Recoverable)
					{
						pipeline.CurrentNode.IsAlive = false;
						exception.RethrowKeepingStackTrace();
					}
					if (success && (pipeline.Result?.Success).GetValueOrDefault(false))
					{
						pipeline.CurrentNode.IsAlive = true;
						return pipeline.TypedResult;
					}
					pipeline.BadResponse();

				}
				return pipeline.ReturnInvalidResponseOrThrow();
			}
		}
	}

	public class RequestPipeline<T> : IDisposable
	{
		public ElasticsearchResponse<T> TypedResult { get; private set; }
		public ElasticsearchResponse<Stream> Result { get; private set; }
		public IConnectionConfigurationValues Settings { get; }
		public IConnection Connection { get; }
		public IConnectionPool ConnectionPool { get; }
		public IDateTimeProvider DateTimeProvider { get; }

		//todo these two terms are too similar come up with a better name
		public IRequestParameters RequestParameters { get; }
		public IRequestConfiguration RequestConfiguration { get; }

		public Node CurrentNode { get; private set; }
		public DateTime StartedOn { get; private set; }
		public int MaxRetries { get; }
		private int _retried = 0;
		public int Retried => _retried;

		private int _nodeSeed = 0;

		const int DefaultPingTimeout = 1000;
		readonly int SslDefaultPingTimeout = 2000;

		public RequestPipeline(IConnection connection, IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider, IRequestParameters requestParameters)
		{
			this.Settings = configurationValues;
			this.ConnectionPool = this.Settings.ConnectionPool;
			this.Connection = connection;
			this.MaxRetries = this.Settings.MaxRetries ?? this.ConnectionPool.MaxRetries;
			this.DateTimeProvider = dateTimeProvider;
			this.RequestParameters = requestParameters;
			this.RequestConfiguration = requestParameters?.RequestConfiguration;
			this.StartedOn = dateTimeProvider.Now();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public bool FirstPoolUsage()
		{
			if (!this.Settings.SniffsOnStartup || !this.ConnectionPool.AcceptsUpdates || this.ConnectionPool.SniffedOnStartup)
				return false;

			this.Sniff();

			return true;
		}

		public async Task<bool> FirstPoolUsageAsync()
		{
			if (!this.Settings.SniffsOnStartup || !this.ConnectionPool.AcceptsUpdates || this.ConnectionPool.SniffedOnStartup)
				return false;

			await this.SniffAsync();

			return true;
		}

		public void OutOfDateClusterInformation()
		{
			var sniffLifeSpan = this.Settings.SniffInformationLifeSpan;
			if (!sniffLifeSpan.HasValue) return;

			var now = this.DateTimeProvider.Now();
			var lastSniff = this.ConnectionPool.LastUpdate;

			if (!lastSniff.HasValue || (lastSniff.HasValue && sniffLifeSpan.Value < (now - lastSniff.Value)))
				this.Sniff();
		}

		public async Task OutOfDateClusterInformationAsync()
		{
			var sniffLifeSpan = this.Settings.SniffInformationLifeSpan;
			if (!sniffLifeSpan.HasValue) return;

			var now = this.DateTimeProvider.Now();
			var lastSniff = this.ConnectionPool.LastUpdate;

			if (!lastSniff.HasValue || (lastSniff.HasValue && sniffLifeSpan.Value < (now - lastSniff.Value)))
				await this.SniffAsync();
		}
		private bool TookToLong()
		{
			var timeout = this.Settings.MaxRetryTimeout.GetValueOrDefault(TimeSpan.FromMilliseconds(this.Settings.Timeout));
			var now = this.DateTimeProvider.Now();

			//we apply a soft margin so that if a request timesout at 59 seconds when the maximum is 60 we also abort.
			var margin = (timeout.TotalMilliseconds / 100.0) * 98;
			var marginTimeSpan = TimeSpan.FromMilliseconds(margin);
			var timespanCall = (now - this.StartedOn);
			var tookToLong = timespanCall >= marginTimeSpan;
			return tookToLong;
		}

		public bool NextNode() {
			if (this.Retried >= this.MaxRetries) return false;

			//TODO move this out of GetNext;
			var node = this.ConnectionPool.GetNext(_nodeSeed, out _nodeSeed);
			//todo make connectionpool return Node
			this.CurrentNode = node;
			return true;
		}

		public void BadResponse()
		{
			var currentRetryCount = this._retried;
			this._retried++;
			
			this.CurrentNode.IsAlive = false;
			var tookToLong = this.TookToLong();
			if (!tookToLong || currentRetryCount < this.MaxRetries)
				return;
			
			if (tookToLong) throw new ElasticsearchException(PipelineFailure.RetryMaximum, this.Result);
			throw new ElasticsearchException(PipelineFailure.RetryMaximum, this.Result);
		}

		int PingTimeout =>
			 this.RequestConfiguration.ConnectTimeout ?? this.Settings.PingTimeout ?? (this.ConnectionPool.UsingSsl ? SslDefaultPingTimeout : DefaultPingTimeout);

		public void Ping()
		{
			if (this.Settings.DisablePings) return;

			//TODO merge with this.RequestConfiguration
			var requestOverrides = new RequestConfiguration { ConnectTimeout = PingTimeout, RequestTimeout = PingTimeout };

			this.Call(PipelineFailure.BadPing, () => this.Connection.HeadSync(this.CurrentNode.CreatePath(""), requestOverrides));

		}

		public async Task PingAsync()
		{
			if (this.Settings.DisablePings) return;

			//TODO merge with this.RequestConfiguration
			var requestOverrides = new RequestConfiguration { ConnectTimeout = PingTimeout, RequestTimeout = PingTimeout };

			await this.CallAsync(PipelineFailure.BadPing, () => this.Connection.Head(this.CurrentNode.CreatePath(""), requestOverrides));

		}
		public static void VoidCallHandler(ElasticsearchResponse<Stream> response) { }

		ElasticsearchResponse<Stream> Call(PipelineFailure failure,  Func<ElasticsearchResponse<Stream>> call)
		{
			ElasticsearchResponse<Stream> response = null;
			try
			{
				response = call();
			}
			catch (ElasticsearchException e)
			{
				e.RethrowKeepingStackTrace();
				return null; //not hit;
			}
			catch (Exception e)
			{
				response?.Response?.Dispose();
				throw new ElasticsearchException(failure, e);
			}
			return HandleResponseStream(failure, response);
		}

		async Task<ElasticsearchResponse<Stream>> CallAsync(PipelineFailure failure,  Func<Task<ElasticsearchResponse<Stream>>> call)
		{
			ElasticsearchResponse<Stream> response = null;
			try
			{
				response = await call();
			}
			catch (ElasticsearchException e)
			{
				e.RethrowKeepingStackTrace();
				return null; //not hit;
			}
			catch (Exception e)
			{
				response?.Response?.Dispose();
				throw new ElasticsearchException(failure, e);
			}
			return HandleResponseStream(failure, response);
		}

		static ElasticsearchResponse<Stream> HandleResponseStream(PipelineFailure failure, ElasticsearchResponse<Stream> response)
		{
			if (response == null || response.Response == null || !response.Success)
				throw new ElasticsearchException(failure, response);
			return response;
		}

		void Sniff()
		{
			var path = "_nodes/_all/clear?timeout=" + this.PingTimeout;
			var exceptions = new List<ElasticsearchException>();
			foreach (var node in this.ConnectionPool.Nodes)
			{
				try
				{
					var response = this.Call(PipelineFailure.BadResponse, () => this.Connection.GetSync(node.CreatePath(path), this.RequestConfiguration));
					using (response.Response)
					{
						var listOfNodes = Sniffer.FromStream(response, response.Response, this.Settings.Serializer, this.Connection.AddressScheme);
						if (!listOfNodes.HasAny())
							throw new ElasticsearchException(PipelineFailure.BadResponse, response);

						this.ConnectionPool.UpdateNodeList(listOfNodes);
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
			foreach (var node in this.ConnectionPool.Nodes)
			{
				try
				{
					var response = await this.CallAsync(PipelineFailure.BadResponse, () => this.Connection.Get(node.CreatePath(path), this.RequestConfiguration));
					using (response.Response)
					{
						var listOfNodes = Sniffer.FromStream(response, response.Response, this.Settings.Serializer, this.Connection.AddressScheme);
						if (!listOfNodes.HasAny())
							throw new ElasticsearchException(PipelineFailure.BadResponse, response);

						this.ConnectionPool.UpdateNodeList(listOfNodes);
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

		public void CallElasticsearch(HttpMethod method, string path, PostData<object> post = null, IRequestParameters requestParameters = null)
		{
			var config = requestParameters?.RequestConfiguration;
			var uri = this.CurrentNode.CreatePath(path);
			var data = post.ToByteArray(this.Settings);
			this.Result = this.Call(PipelineFailure.BadResponse, () =>
			{
				switch (method)
				{
					case HttpMethod.POST: return this.Connection.PostSync(uri, data, config);
					case HttpMethod.PUT: return this.Connection.PutSync(uri, data, config);
					case HttpMethod.HEAD: return this.Connection.HeadSync(uri, config);
					case HttpMethod.GET: return this.Connection.GetSync(uri, config);
					case HttpMethod.DELETE:
						return data == null || data.Length == 0
							? this.Connection.DeleteSync(uri, config)
							: this.Connection.DeleteSync(uri, data, config);
				}
				throw new ElasticsearchException(PipelineFailure.Unexpected, new ArgumentException("unknown http method", nameof(method)));
			});
		}

		public async Task CallElasticsearchAsync(HttpMethod method, string path, PostData<object> post = null, IRequestParameters requestParameters = null)
		{
			var config = requestParameters?.RequestConfiguration;
			var uri = this.CurrentNode.CreatePath(path);
			var data = post.ToByteArray(this.Settings);
			this.Result = await this.CallAsync(PipelineFailure.BadResponse, () =>
			{
				switch (method)
				{
					case HttpMethod.POST: return this.Connection.Post(uri, data, config);
					case HttpMethod.PUT: return this.Connection.Put(uri, data, config);
					case HttpMethod.HEAD: return this.Connection.Head(uri, config);
					case HttpMethod.GET: return this.Connection.Get(uri, config);
					case HttpMethod.DELETE:
						return data == null || data.Length == 0
							? this.Connection.Delete(uri, config)
							: this.Connection.Delete(uri, data, config);
				}
				throw new ElasticsearchException(PipelineFailure.Unexpected, new ArgumentException("unknown http method", nameof(method)));
			});
		}

		internal ElasticsearchResponse<T> ReturnInvalidResponseOrThrow()
		{
			throw new NotImplementedException();
		}
	}
	
	public interface IPostData
	{
		byte[] ToByteArray(IConnectionConfigurationValues settings);
	}

	public class PostData<T> : IPostData
	{
		protected readonly byte[] ByteArray;
		protected readonly string LiteralString;
		protected readonly IEnumerable<string> EnumurabeOfStrings;
		protected readonly IEnumerable<T> EnumerableOfObject;
		protected readonly T Item5;

		readonly int _tag;

		public PostData(byte[] item) { ByteArray = item; _tag = 0; }
		public PostData(string item) { LiteralString = item; _tag = 1; }
		public PostData(IEnumerable<string> item) { EnumurabeOfStrings = item; _tag = 2; }
		public PostData(IEnumerable<T> item) { EnumerableOfObject = item; _tag = 3; }
		public PostData(T item) { Item5 = item; _tag = 4; }

		public byte[] ToByteArray(IConnectionConfigurationValues settings)
		{
			var indent = settings.UsesPrettyRequests ? SerializationFormatting.Indented : SerializationFormatting.None;
			switch (_tag)
			{
				case 0: return ByteArray;
				case 1: return LiteralString?.Utf8Bytes();
				case 2: return EnumurabeOfStrings?.HasAny() ?? false ? (string.Join("\n", EnumurabeOfStrings) + "\n").Utf8Bytes() : null;
				case 3: return EnumurabeOfStrings?.HasAny() ?? false ? (string.Join("\n", EnumurabeOfStrings) + "\n").Utf8Bytes() : null;
				case 4: return EnumerableOfObject?.HasAny() ?? false
						? (string.Join("\n", EnumerableOfObject.Select(soo => settings.Serializer.Serialize(soo, SerializationFormatting.None).Utf8String())) + "\n").Utf8Bytes()
						: null;
				case 5: return settings.Serializer.Serialize(Item5, indent);
			}
			return null;
		}

		public static implicit operator PostData<T>(byte[] byteArray) => new PostData<T>(byteArray);
		public static implicit operator PostData<T>(string literalString) => new PostData<T>(literalString);
		public static implicit operator PostData<T>(List<string> listOfStrings) => new PostData<T>(listOfStrings);
		public static implicit operator PostData<T>(List<T> listOfObjects) => new PostData<T>(listOfObjects);
		public static implicit operator PostData<T>(T @object) => new PostData<T>(@object);
	}
}
