namespace Elasticsearch.Net
{
	//TODO SNIFFONFAIL AND SKIPNODE ARE NEVER USED
	//TODO add MaxRetry, RetryTimeout?
	public enum AuditEvent
	{
		SniffOnStartup,
		SniffOnFail,
		SniffOnStaleCluster,

		SniffSuccess,
		SniffFailure,
		PingSuccess,
		PingFailure,

		SkipNode,
		BadResponse,
		HealthyResponse
	}
}