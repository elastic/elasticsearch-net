using System;
using System.IO;
using System.Net;

namespace Elasticsearch.Net.Connection
{
	public abstract class ElasticsearchAuthException : Exception
	{
		protected abstract string ExceptionType { get; }
		protected abstract int StatusCode { get; }

		protected ElasticsearchAuthException(ElasticsearchResponse<Stream> response)
		{
			this.Response = response;
		}

		internal ElasticsearchServerException ToElasticsearchServerException()
		{
			if (this.Response == null)
				return null;
			return new ElasticsearchServerException(this.StatusCode, this.ExceptionType);
		}
		public ElasticsearchResponse<Stream> Response { get; private set; }
	}

	public class ElasticsearchAuthorizationException : ElasticsearchAuthException
	{
		public ElasticsearchAuthorizationException(ElasticsearchResponse<Stream> response) : base(response) { }

		protected override string ExceptionType { get { return "AuthorizationException"; } }

		protected override int StatusCode { get { return 403; } }
	}


	public class ElasticsearchAuthenticationException : ElasticsearchAuthException
	{
		protected override string ExceptionType { get { return  "AuthenticationException"; } }

		protected override int StatusCode { get { return 401; } }

		public ElasticsearchAuthenticationException(ElasticsearchResponse<Stream> response) : base(response) { }

	}
}