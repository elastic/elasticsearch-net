using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	//TODO make sure we attach as much information from this pipeline to unrecoverable exceptions
	public class ElasticsearchConnectionException : Exception
	{
		public PipelineFailure Cause { get; }
		public IApiCallDetails Response { get; }
		public bool Recoverable => Cause == PipelineFailure.BadResponse || Cause == PipelineFailure.Unexpected || Cause == PipelineFailure.BadPing;

		public ElasticsearchConnectionException(PipelineFailure cause, Exception innerException) 
			: base(cause.Explanation(), innerException)
		{
			this.Cause = cause;
		}

		public ElasticsearchConnectionException(PipelineFailure cause, IApiCallDetails response, Exception innerException) : base("", innerException)
		{
			this.Cause = cause;
			this.Response = response;
		}
	}

}
