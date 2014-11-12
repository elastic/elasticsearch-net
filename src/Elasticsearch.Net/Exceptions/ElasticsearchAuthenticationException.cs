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

		internal ElasticsearchServerException ToElasticsearchServerException()
		{
			if (this.Response == null)
				return null;
			return new ElasticsearchServerException(this.Response.HttpStatusCode.Value, "AuthenticationException");
		}
	}
}