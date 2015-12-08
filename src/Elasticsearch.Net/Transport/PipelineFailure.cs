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

	public static class PipelineFailureExtensions
	{
		public static string Explanation(this PipelineFailure failure)
		{
			switch(failure)
			{
				case PipelineFailure.BadAuthentication:
					return "Could not authenticate with the Elasticsearch node.  Verify your credentials.";
				case PipelineFailure.BadResponse:
					return "An Elasticsearch server error occurred.";
				case PipelineFailure.BadPing:
					return "Unable to ping the Elasticsearch node.";
				case PipelineFailure.BadSniff:
					return "Unable to sniff for Elasticsearch nodes.";
				case PipelineFailure.CouldNotStartSniffOnStartup:
					return "Failed sniffing for Elasticsearch nodes on startup.";
				case PipelineFailure.RetryTimeout:
					return "Maximum timeout reached while retrying.";
				case PipelineFailure.RetryMaximum:
					return "Maximum number of retries reached.";
				default:
					return "An unexpected error occurred. Try checking the inner exception.";
			}
		}
	}
}
