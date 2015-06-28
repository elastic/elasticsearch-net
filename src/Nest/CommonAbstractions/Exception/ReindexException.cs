using System;
using Elasticsearch.Net;

namespace Nest
{
	public class ReindexException: Exception
	{
		public IElasticsearchResponse Status { get; private set; }

		public ReindexException(IElasticsearchResponse status, string message = null) : base(message)
		{
			this.Status = status;
		}
	}
}