using System;
using Elasticsearch.Net;

namespace Nest
{
	public class ReindexException: Exception
	{
		public ElasticsearchResponse Status { get; private set; }

		public ReindexException(ElasticsearchResponse status, string message = null) : base(message)
		{
			this.Status = status;
		}
	}
}