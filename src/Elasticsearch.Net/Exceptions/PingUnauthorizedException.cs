using System;
using System.IO;
using System.Net;

namespace Elasticsearch.Net.Connection
{
	public class PingUnauthorizedException : Exception
	{
		public ElasticsearchResponse<Stream> Response { get; private set; }
		
		public PingUnauthorizedException(ElasticsearchResponse<Stream> response)
		{
			this.Response = response;
		}
	}
}