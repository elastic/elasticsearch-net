using System;

namespace Elasticsearch.Net
{
	public class PipelineException : Exception
	{
		//|| FailureReason == FailureReason.Unexpected;

		public PipelineException(PipelineFailure failure)
			: base(GetMessage(failure)) => FailureReason = failure;

		public PipelineException(string message)
			: base(message) => FailureReason = PipelineFailure.BadResponse;

		public PipelineException(PipelineFailure failure, Exception innerException)
			: base(GetMessage(failure), innerException) => FailureReason = failure;

		public PipelineFailure FailureReason { get; }

		public bool Recoverable =>
			FailureReason == PipelineFailure.BadRequest
			|| FailureReason == PipelineFailure.BadResponse
			|| FailureReason == PipelineFailure.PingFailure;

		public IApiCallDetails Response { get; internal set; }

		private static string GetMessage(PipelineFailure failure)
		{
			switch (failure)
			{
				case PipelineFailure.BadRequest:
					return "An error occurred trying to write the request data to the specified node.";
				case PipelineFailure.BadResponse:
					return "An error occurred trying to read the response from the specified node.";
				case PipelineFailure.BadAuthentication:
					return "Could not authenticate with the specified node. Try verifying your credentials or check your Shield configuration.";
				case PipelineFailure.PingFailure:
					return "Failed to ping the specified node.";
				case PipelineFailure.SniffFailure:
					return "Failed sniffing cluster state.";
				case PipelineFailure.CouldNotStartSniffOnStartup:
					return "Failed sniffing cluster state upon client startup.";
				default:
					return "An unexpected error occurred. Try checking the original exception for more information.";
			}
		}
	}
}
