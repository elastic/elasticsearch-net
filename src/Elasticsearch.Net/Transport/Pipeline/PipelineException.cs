using System;

namespace Elasticsearch.Net
{
	public class PipelineException : Exception
	{
		public PipelineException(PipelineFailure failure)
			: base(GetMessage(failure)) => FailureReason = failure;

		public PipelineException(string message)
			: base(message) => FailureReason = PipelineFailure.BadResponse;

		public PipelineException(PipelineFailure failure, Exception innerException)
			: base(GetMessage(failure), innerException) => FailureReason = failure;

		public IApiCallDetails ApiCall { get; internal set; }
		public PipelineFailure FailureReason { get; }

		public bool Recoverable =>
			FailureReason == PipelineFailure.BadRequest
			|| FailureReason == PipelineFailure.BadResponse
			|| FailureReason == PipelineFailure.PingFailure;

		public IElasticsearchResponse Response { get; internal set; }

		private static string GetMessage(PipelineFailure failure)
		{
			switch (failure)
			{
				case PipelineFailure.BadRequest: return "An error occurred trying to write the request data to the specified node.";
				case PipelineFailure.BadResponse: return "An error occurred trying to read the response from the specified node.";
				case PipelineFailure.BadAuthentication:
					return "Could not authenticate with the specified node. Try verifying your credentials or check your Shield configuration.";
				case PipelineFailure.PingFailure: return "Failed to ping the specified node.";
				case PipelineFailure.SniffFailure: return "Failed sniffing cluster state.";
				case PipelineFailure.CouldNotStartSniffOnStartup: return "Failed sniffing cluster state upon client startup.";
				case PipelineFailure.MaxTimeoutReached: return "Maximum timeout was reached.";
				case PipelineFailure.MaxRetriesReached: return "The call was retried the configured maximum amount of times";
				case PipelineFailure.NoNodesAttempted:
					return "No nodes were attempted, this can happen when a node predicate does not match any nodes";
				case PipelineFailure.Unexpected:
				default:
					return "An unexpected error occurred. Try checking the original exception for more information.";
			}
		}
	}
}
