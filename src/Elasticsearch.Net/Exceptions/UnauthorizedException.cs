using System;
using System.IO;
using System.Net;

namespace Elasticsearch.Net.Connection
{
	public class UnauthorizedException : Exception
	{
		public ElasticsearchResponse<Stream> Response { get; private set; }
		
		public UnauthorizedException(ElasticsearchResponse<Stream> response)
		{
			this.Response = response;
		}
	}
}