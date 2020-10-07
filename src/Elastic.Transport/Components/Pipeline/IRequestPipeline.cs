// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport.Diagnostics.Auditing;

namespace Elastic.Transport
{
	public interface IRequestPipeline : IDisposable
	{
		List<Audit> AuditTrail { get; }
		bool FirstPoolUsageNeedsSniffing { get; }
		bool IsTakingTooLong { get; }
		int MaxRetries { get; }

		int Retried { get; }
		bool SniffsOnConnectionFailure { get; }
		bool SniffsOnStaleCluster { get; }
		bool StaleClusterState { get; }

		DateTime StartedOn { get; }

		TResponse CallProductEndpoint<TResponse>(RequestData requestData)
			where TResponse : class, ITransportResponse, new();

		Task<TResponse> CallProductEndpointAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, ITransportResponse, new();

		void MarkAlive(Node node);

		void MarkDead(Node node);

		IEnumerable<Node> NextNode();

		void Ping(Node node);

		Task PingAsync(Node node, CancellationToken cancellationToken);

		void FirstPoolUsage(SemaphoreSlim semaphore);

		Task FirstPoolUsageAsync(SemaphoreSlim semaphore, CancellationToken cancellationToken);

		void Sniff();

		Task SniffAsync(CancellationToken cancellationToken);

		void SniffOnStaleCluster();

		Task SniffOnStaleClusterAsync(CancellationToken cancellationToken);

		void SniffOnConnectionFailure();

		Task SniffOnConnectionFailureAsync(CancellationToken cancellationToken);

		void BadResponse<TResponse>(ref TResponse response, IApiCallDetails callDetails, RequestData data, TransportException exception)
			where TResponse : class, ITransportResponse, new();

		void ThrowNoNodesAttempted(RequestData requestData, List<PipelineException> seenExceptions);

		void AuditCancellationRequested();

		TransportException CreateClientException<TResponse>(TResponse response, IApiCallDetails callDetails, RequestData data,
			List<PipelineException> seenExceptions
		)
			where TResponse : class, ITransportResponse, new();
	}
}
