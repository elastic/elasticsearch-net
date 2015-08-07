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
		private UrlFormatProvider _formatter;

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
			: this(configurationValues, null, null, null) { }

		/// <summary>
		/// Transport coordinates the client requests over the connection pool nodes and is in charge of falling over on different nodes 
		/// </summary>
		/// <param name="configurationValues">The connectionsettings to use for this transport</param>
		/// <param name="requestPipelineProvider">In charge of create a new pipeline, safe to pass null to use the default</param>
		/// <param name="dateTimeProvider">The date time proved to use, safe to pass null to use the default</param>
		/// <param name="memoryStreamProvider">The memory stream provider to use, safe to pass null to use the default</param>
		public Transport(
			TConnectionSettings configurationValues,
			IRequestPipelineFactory pipelineProvider,
			IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamProvider
			)
		{
			configurationValues.ThrowIfNull(nameof(configurationValues));
			configurationValues.ConnectionPool.ThrowIfNull(nameof(configurationValues.ConnectionPool));
			configurationValues.Connection.ThrowIfNull(nameof(configurationValues.Connection));
			configurationValues.Serializer.ThrowIfNull(nameof(configurationValues.Serializer));

			this.Settings = configurationValues;
			this.PipelineProvider = pipelineProvider ?? new RequestPipelineFactory();
			this.DateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
			this.MemoryStreamFactory = memoryStreamProvider ?? new MemoryStreamFactory();
			this._formatter = new UrlFormatProvider(this.Settings);
		}

		public ElasticsearchResponse<TReturn> Request<TReturn>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			path = this.CreatePathWithQueryStrings(path, this.Settings, requestParameters);
			using (var pipeline = this.PipelineProvider.Create(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, requestParameters))
			{
				if (!pipeline.FirstPoolUsage())
					pipeline.OutOfDateClusterInformation();

				var requestData = new RequestData(method, path, data, this.Settings, requestParameters?.RequestConfiguration, this.MemoryStreamFactory);
				ElasticsearchResponse<TReturn> response = null;

				while (pipeline.NextNode())
				{
					try
					{
						pipeline.Ping();
						response = pipeline.CallElasticsearch<TReturn>(requestData);
					}
					catch (ElasticsearchException exception) when (!exception.Recoverable)
					{
						pipeline.MarkDead();
						exception.RethrowKeepingStackTrace();
					}
					if (response != null && response.SuccessOrKnownError)
					{
						pipeline.MarkAlive();
						return response;
					}
					pipeline.BadResponse(response);
				}
				return response;
			}
		}

		public async Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			path = this.CreatePathWithQueryStrings(path, this.Settings, requestParameters);
			using (var pipeline = this.PipelineProvider.Create(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, requestParameters))
			{
				if (await pipeline.FirstPoolUsageAsync())
					await pipeline.OutOfDateClusterInformationAsync();

				var requestData = new RequestData(method, path, data, this.Settings, requestParameters?.RequestConfiguration, this.MemoryStreamFactory);
				ElasticsearchResponse<TReturn> response = null;

				while (pipeline.NextNode())
				{
					try
					{
						await pipeline.PingAsync();
						response = await pipeline.CallElasticsearchAsync<TReturn>(requestData);
					}
					catch (ElasticsearchException exception) when (!exception.Recoverable)
					{
						pipeline.MarkDead();
						exception.RethrowKeepingStackTrace();
					}
					if (response != null && response.SuccessOrKnownError)
					{
						pipeline.MarkAlive();
						return response;
					}
					pipeline.BadResponse(response);
				}
				return response;
			}
		}

		private string CreatePathWithQueryStrings(string path, IConnectionConfigurationValues global, IRequestParameters request = null)
		{
			//Make sure we append global query string as well the request specific query string parameters
			var copy = new NameValueCollection(global.QueryStringParameters);
			copy.Add(request.QueryString.ToNameValueCollection(this._formatter));
			if (!copy.HasKeys()) return path;

			var queryString = copy.ToQueryString();
			path += queryString;
			return path;
		}

	}
}
