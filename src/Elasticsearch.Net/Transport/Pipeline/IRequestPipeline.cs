using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IRequestPipeline : IDisposable
	{
		bool FirstPoolUsageNeedsSniffing { get; }
		bool SniffsOnConnectionFailure { get; }
		bool SniffsOnStaleCluster { get; }
		bool StaleClusterState { get; }

		List<Audit> AuditTrail { get; }

		DateTime StartedOn { get; }
		bool IsTakingTooLong { get; }

		int Retried { get; }
		int MaxRetries { get; }

		TResponse CallElasticsearch<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse;

		Task<TResponse> CallElasticsearchAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse;

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

		void BadResponse<TResponse>(ref TResponse response, RequestData requestData, List<PipelineException> seenExceptions)
			where TResponse : class, IElasticsearchResponse;

		void ThrowNoNodesAttempted(RequestData requestData, List<PipelineException> seenExceptions);

		void AuditCancellationRequested();
	}
}
