using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	// TODO come up with a better name for this?
	public class UnexpectedElasticsearchClientException : ElasticsearchClientException
	{
		public List<PipelineException> SeenExceptions { get; set; }

		public UnexpectedElasticsearchClientException(Exception killerException, List<PipelineException> seenExceptions)
			: base("An unexpected exception occured.", killerException)
		{
			this.SeenExceptions = seenExceptions;
		}
	}
}
