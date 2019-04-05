using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
#if !DOTNETCORE
using System.Net;

#endif

namespace Elasticsearch.Net
{
	public class Transport<TConnectionSettings> : ITransport<TConnectionSettings>
		where TConnectionSettings : IConnectionConfigurationValues
	{
		/// <summary>
		/// Transport coordinates the client requests over the connection pool nodes and is in charge of falling over on different nodes
		/// </summary>
		/// <param name="configurationValues">The connectionsettings to use for this transport</param>
		public Transport(TConnectionSettings configurationValues) : this(configurationValues, null, null, null) { }

		/// <summary>
		/// Transport coordinates the client requests over the connection pool nodes and is in charge of falling over on different nodes
		/// </summary>
		/// <param name="configurationValues">The connectionsettings to use for this transport</param>
		/// <param name="pipelineProvider">In charge of create a new pipeline, safe to pass null to use the default</param>
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
			configurationValues.RequestResponseSerializer.ThrowIfNull(nameof(configurationValues.RequestResponseSerializer));

			Settings = configurationValues;
			PipelineProvider = pipelineProvider ?? new RequestPipelineFactory();
			DateTimeProvider = dateTimeProvider ?? Net.DateTimeProvider.Default;
			MemoryStreamFactory = memoryStreamFactory ?? configurationValues.MemoryStreamFactory;
		}

		public TConnectionSettings Settings { get; }

		private IDateTimeProvider DateTimeProvider { get; }
		private IMemoryStreamFactory MemoryStreamFactory { get; }
		private IRequestPipelineFactory PipelineProvider { get; }

		public TResponse Request<TResponse>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new()
		{
			using (var pipeline = PipelineProvider.Create(Settings, DateTimeProvider, MemoryStreamFactory, requestParameters))
			{
				pipeline.FirstPoolUsage(Settings.BootstrapLock);

				var requestData = new RequestData(method, path, data, Settings, requestParameters, MemoryStreamFactory);
				Settings.OnRequestDataCreated?.Invoke(requestData);
				TResponse response = null;

				var seenExceptions = new List<PipelineException>();
				foreach (var node in pipeline.NextNode())
				{
					requestData.Node = node;
					try
					{
						pipeline.SniffOnStaleCluster();
						Ping(pipeline, node);
						response = pipeline.CallElasticsearch<TResponse>(requestData);
						if (!response.ApiCall.SuccessOrKnownError)
						{
							pipeline.MarkDead(node);
							pipeline.SniffOnConnectionFailure();
						}
					}
					catch (PipelineException pipelineException) when (!pipelineException.Recoverable)
					{
						HandlePipelineException(ref response, pipelineException, pipeline, node, seenExceptions);
						break;
					}
					catch (PipelineException pipelineException)
					{
						HandlePipelineException(ref response, pipelineException, pipeline, node, seenExceptions);
					}
					catch (Exception killerException)
					{
						throw new UnexpectedElasticsearchClientException(killerException, seenExceptions)
						{
							Request = requestData,
							Response = response?.ApiCall,
							AuditTrail = pipeline?.AuditTrail
						};
					}
					if (response == null || !response.ApiCall.SuccessOrKnownError) continue;

					pipeline.MarkAlive(node);
					break;
				}
				return FinalizeResponse(requestData, pipeline, seenExceptions, response);
			}
		}

		public async Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken,
			PostData data = null, IRequestParameters requestParameters = null
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			using (var pipeline = PipelineProvider.Create(Settings, DateTimeProvider, MemoryStreamFactory, requestParameters))
			{
				await pipeline.FirstPoolUsageAsync(Settings.BootstrapLock, cancellationToken).ConfigureAwait(false);

				var requestData = new RequestData(method, path, data, Settings, requestParameters, MemoryStreamFactory);
				Settings.OnRequestDataCreated?.Invoke(requestData);
				TResponse response = null;

				var seenExceptions = new List<PipelineException>();
				foreach (var node in pipeline.NextNode())
				{
					requestData.Node = node;
					try
					{
						await pipeline.SniffOnStaleClusterAsync(cancellationToken).ConfigureAwait(false);
						await PingAsync(pipeline, node, cancellationToken).ConfigureAwait(false);
						response = await pipeline.CallElasticsearchAsync<TResponse>(requestData, cancellationToken).ConfigureAwait(false);
						if (!response.ApiCall.SuccessOrKnownError)
						{
							pipeline.MarkDead(node);
							await pipeline.SniffOnConnectionFailureAsync(cancellationToken).ConfigureAwait(false);
						}
					}
					catch (PipelineException pipelineException) when (!pipelineException.Recoverable)
					{
						HandlePipelineException(ref response, pipelineException, pipeline, node, seenExceptions);
						break;
					}
					catch (PipelineException pipelineException)
					{
						HandlePipelineException(ref response, pipelineException, pipeline, node, seenExceptions);
					}
					catch (Exception killerException)
					{
						throw new UnexpectedElasticsearchClientException(killerException, seenExceptions)
						{
							Request = requestData,
							Response = response?.ApiCall,
							AuditTrail = pipeline.AuditTrail
						};
					}
					if (cancellationToken.IsCancellationRequested)
					{
						pipeline.AuditCancellationRequested();
						break;
					}
					if (response == null || !response.ApiCall.SuccessOrKnownError) continue;

					pipeline.MarkAlive(node);
					break;
				}
				return FinalizeResponse(requestData, pipeline, seenExceptions, response);
			}
		}

		private static void HandlePipelineException<TResponse>(
			ref TResponse response, PipelineException ex, IRequestPipeline pipeline, Node node, List<PipelineException> seenExceptions
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (response == null) response = ex.Response as TResponse;
			pipeline.MarkDead(node);
			seenExceptions.Add(ex);
		}

		private TResponse FinalizeResponse<TResponse>(RequestData requestData, IRequestPipeline pipeline, List<PipelineException> seenExceptions,
			TResponse response
		) where TResponse : class, IElasticsearchResponse, new()
		{
			if (requestData.Node == null) //foreach never ran
				pipeline.ThrowNoNodesAttempted(requestData, seenExceptions);

			var callDetails = GetMostRecentCallDetails(response, seenExceptions);
			var clientException = pipeline.CreateClientException(response, callDetails, requestData, seenExceptions);

			if (response?.ApiCall == null)
				pipeline.BadResponse(ref response, callDetails, requestData, clientException);

			HandleElasticsearchClientException(requestData, clientException, response);
			return response;
		}

		private static IApiCallDetails GetMostRecentCallDetails<TResponse>(TResponse response, IEnumerable<PipelineException> seenExceptions)
			where TResponse : class, IElasticsearchResponse, new()
		{
			var callDetails = response?.ApiCall ?? seenExceptions.LastOrDefault(e => e.ApiCall != null)?.ApiCall;
			return callDetails;
		}


		private void HandleElasticsearchClientException(RequestData data, Exception clientException, IElasticsearchResponse response)
		{
			if (response.ApiCall is ApiCallDetails a)
			{
				//if original exception was not explicitly set during the pipeline
				//set it to the ElasticsearchClientException we created for the bad response
				if (clientException != null && a.OriginalException == null)
					a.OriginalException = clientException;
				//On .NET Core the IConnection implementation throws exceptions on bad responses
				//This causes it to behave differently to .NET FULL. We already wrapped the WebException
				//under ElasticsearchServerException and it exposes way more information as part of it's
				//exception message e.g the the root cause of the server error body.
#if !DOTNETCORE
				if (a.OriginalException is WebException)
					a.OriginalException = clientException;
#endif
			}

			Settings.OnRequestCompleted?.Invoke(response.ApiCall);
			if (clientException != null && data.ThrowExceptions) throw clientException;
		}

		private static void Ping(IRequestPipeline pipeline, Node node)
		{
			try
			{
				pipeline.Ping(node);
			}
			catch (PipelineException e) when (e.Recoverable)
			{
				pipeline.SniffOnConnectionFailure();
				throw;
			}
		}

		private static async Task PingAsync(IRequestPipeline pipeline, Node node, CancellationToken cancellationToken)
		{
			try
			{
				await pipeline.PingAsync(node, cancellationToken).ConfigureAwait(false);
			}
			catch (PipelineException e) when (e.Recoverable)
			{
				await pipeline.SniffOnConnectionFailureAsync(cancellationToken).ConfigureAwait(false);
				throw;
			}
		}
	}
}
