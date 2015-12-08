using System;

namespace Elasticsearch.Net.Connection
{
	public enum PipelineFailure
	{
		BadAuthentication,
		BadResponse,
		BadPing,
		BadSniff,
		CouldNotStartSniffOnStartup,
		RetryTimeout,
		RetryMaximum,
		Unexpected
	}	
}
