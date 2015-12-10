using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	//TODO make sure we attach as much information from this pipeline to unrecoverable exceptions
	public class ConnectionException : Exception
	{
		public PipelineFailure FailureReason { get; }
		public IApiCallDetails Response { get; }
		public bool Recoverable => FailureReason == PipelineFailure.BadResponse || FailureReason == PipelineFailure.Unexpected || FailureReason == PipelineFailure.BadPing;

		public ConnectionException(PipelineFailure failure, Exception innerException) 
			: base(failure.Explanation(), innerException)
		{
			this.FailureReason = failure;
		}

		public ConnectionException(PipelineFailure failure, IApiCallDetails response, Exception innerException) : base("", innerException)
		{
			this.FailureReason = failure;
			this.Response = response;
		}
	}

	public static class PipelineFailureExtensions
	{
		public static string Explanation(this PipelineFailure failure)
		{
			switch(failure)
			{
				case PipelineFailure.BadResponse:
					return "An error occurred trying to establish a connection with the specified node.";
				case PipelineFailure.BadAuthentication:
					return "Could not authenticate with the specified node. Try verifying your credentials or check your Shield configuration.";
				case PipelineFailure.BadPing:
					return "Failed to ping the specified node.";
				case PipelineFailure.BadSniff:
					return "Failed sniffing cluster state.";
				case PipelineFailure.CouldNotStartSniffOnStartup:
					return "Failed sniffing cluster state upon client startup.";
				case PipelineFailure.RetryTimeout:
					return "Maximum timeout reached while retrying request.";
				case PipelineFailure.RetryMaximum:
					return "Maximum number of retries reached.";
				default:
					return "An unexpected error occurred. Try checking the original exception for more information.";
			}
		}
	}
}
