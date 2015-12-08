using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ElasticsearchNetException : Exception
	{
		public ElasticsearchNetException(string message, Exception inner) 
			: base(message, inner)
		{

		}

		public ElasticsearchNetException(Exception inner) : base(inner.Message, inner)
		{
		}

		public bool IsElasticsearchServerException() => true;
	}
}
