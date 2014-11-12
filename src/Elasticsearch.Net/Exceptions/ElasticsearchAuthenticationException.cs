using System;
using System.IO;
using System.Net;

namespace Elasticsearch.Net.Connection
{
	public class ElasticsearchAuthenticationException : Exception
	{
		public ElasticsearchResponse<Stream> Response { get; private set; }
		
		public ElasticsearchAuthenticationException(ElasticsearchResponse<Stream> response)
		{
			this.Response = response;
		}
	}
}