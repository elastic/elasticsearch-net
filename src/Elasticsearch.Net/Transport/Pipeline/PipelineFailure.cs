namespace Elasticsearch.Net
{
	public enum PipelineFailure
	{
		BadAuthentication,
		BadResponse,
		BadPing,
		BadSniff,
		CouldNotStartSniffOnStartup,
		Unexpected
	}	
}
