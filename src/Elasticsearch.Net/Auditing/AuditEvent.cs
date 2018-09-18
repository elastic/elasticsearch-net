namespace Elasticsearch.Net
{
	public enum AuditEvent
	{
		SniffOnStartup,
		SniffOnFail,
		SniffOnStaleCluster,

		SniffSuccess,
		SniffFailure,
		PingSuccess,
		PingFailure,

		Resurrection,
		AllNodesDead,
		BadResponse,
		HealthyResponse,

		MaxTimeoutReached,
		MaxRetriesReached,
		BadRequest,
		NoNodesAttempted,
		CancellationRequested,
		FailedOverAllNodes,
	}
}
