using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
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

		void AuditCancellationRequested();

		void BadResponse<TResponse>(ref TResponse response, IApiCallDetails callDetails, RequestData data, ElasticsearchClientException exception)
			where TResponse : class, IElasticsearchResponse, new();

		TResponse CallElasticsearch<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new();

		Task<TResponse> CallElasticsearchAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new();

		ElasticsearchClientException CreateClientException<TResponse>(TResponse response, IApiCallDetails callDetails, RequestData data,
			List<PipelineException> seenExceptions
		)
			where TResponse : class, IElasticsearchResponse, new();

		void FirstPoolUsage(SemaphoreSlim semaphore);

		Task FirstPoolUsageAsync(SemaphoreSlim semaphore, CancellationToken cancellationToken);

		void MarkAlive(Node node);

		void MarkDead(Node node);

		IEnumerable<Node> NextNode();

		void Ping(Node node);

		Task PingAsync(Node node, CancellationToken cancellationToken);

		void Sniff();

		Task SniffAsync(CancellationToken cancellationToken);

		void SniffOnConnectionFailure();

		Task SniffOnConnectionFailureAsync(CancellationToken cancellationToken);

		void SniffOnStaleCluster();

		Task SniffOnStaleClusterAsync(CancellationToken cancellationToken);

		void ThrowNoNodesAttempted(RequestData requestData, List<PipelineException> seenExceptions);
	}
}
