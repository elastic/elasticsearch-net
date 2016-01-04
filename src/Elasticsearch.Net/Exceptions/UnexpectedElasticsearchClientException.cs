using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class UnexpectedElasticsearchClientException : ElasticsearchClientException
	{
		public List<PipelineException> SeenExceptions { get; set; }

		public UnexpectedElasticsearchClientException(Exception killerException, List<PipelineException> seenExceptions)
			: base(PipelineFailure.Unexpected, killerException?.Message ?? "An unexpected exception occured.", killerException)
		{
			this.SeenExceptions = seenExceptions;
		}
	}
}
