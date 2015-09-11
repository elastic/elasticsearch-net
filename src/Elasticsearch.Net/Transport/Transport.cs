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
using System.Collections.Specialized;
using System.Threading;

namespace Elasticsearch.Net.Connection
{
	public class Transport<TConnectionSettings> : ITransport<TConnectionSettings>
		where TConnectionSettings : IConnectionConfigurationValues
	{
		private SemaphoreSlim _semaphore;

		//TODO discuss which should be public
		public TConnectionSettings Settings { get; }
		public IDateTimeProvider DateTimeProvider { get; }
		public IMemoryStreamFactory MemoryStreamFactory { get; }
		public IRequestPipelineFactory PipelineProvider { get; private set; }

		/// <summary>
		/// Transport coordinates the client requests over the connection pool nodes and is in charge of falling over on different nodes 
		/// </summary>
		/// <param name="configurationValues">The connectionsettings to use for this transport</param>
		public Transport(TConnectionSettings configurationValues)
			: this(configurationValues, null, null, null)
		{ }

		/// <summary>
		/// Transport coordinates the client requests over the connection pool nodes and is in charge of falling over on different nodes 
		/// </summary>
		/// <param name="configurationValues">The connectionsettings to use for this transport</param>
		/// <param name="requestPipelineProvider">In charge of create a new pipeline, safe to pass null to use the default</param>
		/// <param name="dateTimeProvider">The date time proved to use, safe to pass null to use the default</param>
		/// <param name="memoryStreamFactory">The memory stream provider to use, safe to pass null to use the default</param>
		public Transport(
			TConnectionSettings configurationValues,
			IRequestPipelineFactory pipelineProvider,
			IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory
			)
		{
			configurationValues.ThrowIfNull(nameof(configurationValues));
			configurationValues.ConnectionPool.ThrowIfNull(nameof(configurationValues.ConnectionPool));
			configurationValues.Connection.ThrowIfNull(nameof(configurationValues.Connection));
			configurationValues.Serializer.ThrowIfNull(nameof(configurationValues.Serializer));

			this.Settings = configurationValues;
			this.PipelineProvider = pipelineProvider ?? new RequestPipelineFactory();
			this.DateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
			this.MemoryStreamFactory = memoryStreamFactory ?? new MemoryStreamFactory();
			this._semaphore = new SemaphoreSlim(1, 1);
		}

		public ElasticsearchResponse<TReturn> Request<TReturn>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where TReturn : class
		{
			using (var pipeline = this.PipelineProvider.Create(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, requestParameters))
			{
				pipeline.FirstPoolUsage(this._semaphore);

				var requestData = new RequestData(method, path, data, this.Settings, requestParameters, this.MemoryStreamFactory);
				ElasticsearchResponse<TReturn> response = null;

				var exceptions = new List<ElasticsearchException>();
				foreach (var node in pipeline.NextNode())
				{
					requestData.Node = node;
					try
					{
						pipeline.SniffOnStaleCluster();
						pipeline.Ping(node);
						response = pipeline.CallElasticsearch<TReturn>(requestData);
					}
					catch (ElasticsearchException exception) when (!exception.Recoverable)
					{
						pipeline.MarkDead(node);
						exception.RethrowKeepingStackTrace();
					}
					catch (ElasticsearchException exception)
					{
						pipeline.MarkDead(node);
						exceptions.Add(exception);
					}
					if (response != null && response.SuccessOrKnownError)
					{
						pipeline.MarkAlive(node);
						return response;
					}
				}
				pipeline.BadResponse(ref response, requestData, exceptions);
				return response;
			}
		}

		public async Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where TReturn : class
		{
			using (var pipeline = this.PipelineProvider.Create(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, requestParameters))
			{
				await pipeline.FirstPoolUsageAsync(this._semaphore);

				var requestData = new RequestData(method, path, data, this.Settings, requestParameters, this.MemoryStreamFactory);
				ElasticsearchResponse<TReturn> response = null;

				var exceptions = new List<ElasticsearchException>();
				foreach (var node in pipeline.NextNode())
				{
					requestData.Node = node;
					try
					{
						await pipeline.SniffOnStaleClusterAsync();
						await pipeline.PingAsync(node);
						response = await pipeline.CallElasticsearchAsync<TReturn>(requestData);
					}
					catch (ElasticsearchException exception) when (!exception.Recoverable)
					{
						pipeline.MarkDead(node);
						exception.RethrowKeepingStackTrace();
					}
					catch (ElasticsearchException exception)
					{
						pipeline.MarkDead(node);
						exceptions.Add(exception);
					}
					if (response != null && response.SuccessOrKnownError)
					{
						pipeline.MarkAlive(node);
						return response;
					}
				}
				pipeline.BadResponse(ref response, requestData, exceptions);
				return response;
			}
		}


	}
}
