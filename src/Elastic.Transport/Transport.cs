// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport.Extensions;
using Elastic.Transport.Products;

#if !DOTNETCORE
using System.Net;
#endif

namespace Elastic.Transport
{
	public class Transport<TConnectionSettings> : ITransport<TConnectionSettings>
		where TConnectionSettings : class, ITransportConfigurationValues
	{
		private readonly IProductRegistration _productRegistration;

		/// <summary>
		/// Transport coordinates the client requests over the connection pool nodes and is in charge of falling over on different nodes
		/// </summary>
		/// <param name="configurationValues">The connection settings to use for this transport</param>
		public Transport(TConnectionSettings configurationValues)
			: this(configurationValues, null, null, null) { }

		/// <summary>
		/// Transport coordinates the client requests over the connection pool nodes and is in charge of falling over on different nodes
		/// </summary>
		/// <param name="configurationValues">The connection settings to use for this transport</param>
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

			_productRegistration = configurationValues.ProductRegistration;
			Settings = configurationValues;
			PipelineProvider = pipelineProvider ?? new RequestPipelineFactory();
			DateTimeProvider = dateTimeProvider ?? Elastic.Transport.DateTimeProvider.Default;
			MemoryStreamFactory = memoryStreamFactory ?? configurationValues.MemoryStreamFactory;
		}

		public TConnectionSettings Settings { get; }

		private IDateTimeProvider DateTimeProvider { get; }
		private IMemoryStreamFactory MemoryStreamFactory { get; }
		private IRequestPipelineFactory PipelineProvider { get; }

		public TResponse Request<TResponse>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new()
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
						if (_productRegistration.SupportsSniff) pipeline.SniffOnStaleCluster();
						if (_productRegistration.SupportsPing) Ping(pipeline, node);

						response = pipeline.CallProductEndpoint<TResponse>(requestData);
						if (!response.ApiCall.SuccessOrKnownError)
						{
							pipeline.MarkDead(node);
							if (_productRegistration.SupportsSniff) pipeline.SniffOnConnectionFailure();
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
						throw new UnexpectedTransportException(killerException, seenExceptions)
						{
							Request = requestData,
							Response = response?.ApiCall,
							AuditTrail = pipeline.AuditTrail
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
			where TResponse : class, ITransportResponse, new()
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
						if (_productRegistration.SupportsSniff)
							await pipeline.SniffOnStaleClusterAsync(cancellationToken).ConfigureAwait(false);
						if (_productRegistration.SupportsPing)
							await PingAsync(pipeline, node, cancellationToken).ConfigureAwait(false);

						response = await pipeline.CallProductEndpointAsync<TResponse>(requestData, cancellationToken).ConfigureAwait(false);
						if (!response.ApiCall.SuccessOrKnownError)
						{
							pipeline.MarkDead(node);
							if (_productRegistration.SupportsSniff)
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
						if (killerException is OperationCanceledException && cancellationToken.IsCancellationRequested)
							pipeline.AuditCancellationRequested();

						throw new UnexpectedTransportException(killerException, seenExceptions)
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
			ref TResponse response, PipelineException ex, IRequestPipeline pipeline, Node node, ICollection<PipelineException> seenExceptions
		)
			where TResponse : class, ITransportResponse, new()
		{
			if (response == null) response = ex.Response as TResponse;
			pipeline.MarkDead(node);
			seenExceptions.Add(ex);
		}

		private TResponse FinalizeResponse<TResponse>(RequestData requestData, IRequestPipeline pipeline, List<PipelineException> seenExceptions,
			TResponse response
		) where TResponse : class, ITransportResponse, new()
		{
			if (requestData.Node == null) //foreach never ran
				pipeline.ThrowNoNodesAttempted(requestData, seenExceptions);

			var callDetails = GetMostRecentCallDetails(response, seenExceptions);
			var clientException = pipeline.CreateClientException(response, callDetails, requestData, seenExceptions);

			if (response?.ApiCall == null)
				pipeline.BadResponse(ref response, callDetails, requestData, clientException);

			HandleTransportException(requestData, clientException, response);
			return response;
		}

		private static IApiCallDetails GetMostRecentCallDetails<TResponse>(TResponse response, IEnumerable<PipelineException> seenExceptions)
			where TResponse : class, ITransportResponse, new()
		{
			var callDetails = response?.ApiCall ?? seenExceptions.LastOrDefault(e => e.ApiCall != null)?.ApiCall;
			return callDetails;
		}


		// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
		private void HandleTransportException(RequestData data, Exception clientException, ITransportResponse response)
		{
			if (response.ApiCall is ApiCallDetails a)
			{
				//if original exception was not explicitly set during the pipeline
				//set it to the TransportException we created for the bad response
				if (clientException != null && a.OriginalException == null)
					a.OriginalException = clientException;
				//On .NET Core the IConnection implementation throws exceptions on bad responses
				//This causes it to behave differently to .NET FULL. We already wrapped the WebException
				//under TransportException and it exposes way more information as part of it's
				//exception message e.g the the root cause of the server error body.
#if !DOTNETCORE
				if (a.OriginalException is WebException)
					a.OriginalException = clientException;
#endif
			}

			Settings.OnRequestCompleted?.Invoke(response.ApiCall);
			if (data != null && (clientException != null && data.ThrowExceptions)) throw clientException;
		}

		private void Ping(IRequestPipeline pipeline, Node node)
		{
			try
			{
				pipeline.Ping(node);
			}
			catch (PipelineException e) when (e.Recoverable)
			{
				if (_productRegistration.SupportsSniff)
					pipeline.SniffOnConnectionFailure();
				throw;
			}
		}

		private async Task PingAsync(IRequestPipeline pipeline, Node node, CancellationToken cancellationToken)
		{
			try
			{
				await pipeline.PingAsync(node, cancellationToken).ConfigureAwait(false);
			}
			catch (PipelineException e) when (e.Recoverable)
			{
				if (_productRegistration.SupportsSniff)
					await pipeline.SniffOnConnectionFailureAsync(cancellationToken).ConfigureAwait(false);
				throw;
			}
		}
	}
}
