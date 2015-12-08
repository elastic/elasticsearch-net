using Elasticsearch.Net.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ElasticsearchClientException : Exception
	{
		public ElasticsearchClientException(string message, Exception inner)
			: base(message, inner) { }

		public ElasticsearchClientException(Exception inner) : this(inner.Message, inner) { }
	}
}
