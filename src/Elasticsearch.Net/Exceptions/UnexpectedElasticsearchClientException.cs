using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class UnexpectedElasticsearchClientException : ElasticsearchClientException
	{
		public UnexpectedElasticsearchClientException(Exception killerException, List<PipelineException> seenExceptions)
			: base(PipelineFailure.Unexpected, killerException?.Message ?? "An unexpected exception occurred.", killerException) =>
			SeenExceptions = seenExceptions;

		public List<PipelineException> SeenExceptions { get; set; }
	}
}
