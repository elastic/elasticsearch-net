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

		ElasticsearchResponse<TReturn> CallElasticsearch<TReturn>(RequestData requestData) where TReturn : class;

		Task<ElasticsearchResponse<TReturn>> CallElasticsearchAsync<TReturn>(RequestData requestData, CancellationToken cancellationToken)
			where TReturn : class;

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

		void BadResponse<TReturn>(ref ElasticsearchResponse<TReturn> response, RequestData requestData, List<PipelineException> seenExceptions)
			where TReturn : class;

		void ThrowNoNodesAttempted(RequestData requestData, List<PipelineException> seenExceptions);

		void AuditCancellationRequested();
	}
}
