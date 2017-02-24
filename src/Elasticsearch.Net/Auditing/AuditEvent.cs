namespace Elasticsearch.Net_5_2_0
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
		BadRequest
	}
}
