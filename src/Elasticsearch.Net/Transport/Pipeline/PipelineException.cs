using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class PipelineException : Exception
	{
		public PipelineFailure FailureReason { get; }
		
		public bool Recoverable => FailureReason == PipelineFailure.BadResponse || FailureReason == PipelineFailure.Unexpected || FailureReason == PipelineFailure.BadPing;

		public PipelineException(PipelineFailure failure, Exception innerException) 
			: base(failure.Explanation(), innerException)
		{
			this.FailureReason = failure;
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
				default:
					return "An unexpected error occurred. Try checking the original exception for more information.";
			}
		}
	}
}
