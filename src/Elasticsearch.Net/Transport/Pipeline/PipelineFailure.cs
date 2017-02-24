namespace Elasticsearch.Net_5_2_0
{
	public enum PipelineFailure
	{
		BadAuthentication,
		BadResponse,
		PingFailure,
		SniffFailure,
		CouldNotStartSniffOnStartup,
		MaxTimeoutReached,
		MaxRetriesReached,
		Unexpected,
		BadRequest
	}
}
