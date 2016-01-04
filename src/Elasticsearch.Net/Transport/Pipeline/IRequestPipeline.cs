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

		ElasticsearchResponse<TReturn> CallElasticsearch<TReturn>(RequestData requestData) where TReturn : class;
		Task<ElasticsearchResponse<TReturn>> CallElasticsearchAsync<TReturn>(RequestData requestData) where TReturn : class;

		void MarkAlive(Node node);
		void MarkDead(Node node);

		IEnumerable<Node> NextNode();

		void Ping(Node node);
		Task PingAsync(Node node);

		void FirstPoolUsage(SemaphoreSlim semaphore);
		Task FirstPoolUsageAsync(SemaphoreSlim semaphore);

		void Sniff();
		Task SniffAsync();

		void SniffOnStaleCluster();
		Task SniffOnStaleClusterAsync();

		void SniffOnConnectionFailure();
		Task SniffOnConnectionFailureAsync();

		void BadResponse<TReturn>(ref ElasticsearchResponse<TReturn> response, RequestData requestData, List<PipelineException> seenExceptions)
			where TReturn : class;
	}
}