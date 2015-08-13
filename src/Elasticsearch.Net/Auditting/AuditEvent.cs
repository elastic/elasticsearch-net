namespace Elasticsearch.Net.Connection
{
	public enum AuditEvent
	{
		SniffOnStartup,
		SniffOnFail,
		SniffOnStaleCluster,
		SniffFail,
		SniffSuccess,
		Ping,
		BadResponse,
		HealhyResponse
	}
}