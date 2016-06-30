using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Elasticsearch.Net.Connection
{
	[Serializable]
	public abstract class ElasticsearchAuthException : Exception
	{
		protected abstract string ExceptionType { get; }
		protected abstract int StatusCode { get; }

		public ElasticsearchResponse<Stream> Response { get; private set; }

		protected ElasticsearchAuthException(ElasticsearchResponse<Stream> response)
		{
			this.Response = response;
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected ElasticsearchAuthException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		internal ElasticsearchServerException ToElasticsearchServerException() =>
			this.Response == null ? null : new ElasticsearchServerException(this.StatusCode, this.ExceptionType);


	}

	[Serializable]
	public class ElasticsearchAuthorizationException : ElasticsearchAuthException
	{
		public ElasticsearchAuthorizationException(ElasticsearchResponse<Stream> response) : base(response) { }

		protected override string ExceptionType => "AuthorizationException";

		protected override int StatusCode => 403;

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected ElasticsearchAuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException(nameof(info));

			info.AddValue("ExceptionType", this.ExceptionType);
			info.AddValue("StatusCode", this.StatusCode);
			base.GetObjectData(info, context);
		}
	}


	[Serializable]
	public class ElasticsearchAuthenticationException : ElasticsearchAuthException
	{
		protected override string ExceptionType => "AuthenticationException";

		protected override int StatusCode => 401;

		public ElasticsearchAuthenticationException(ElasticsearchResponse<Stream> response) : base(response) { }

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected ElasticsearchAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException(nameof(info));

			info.AddValue("ExceptionType", this.ExceptionType);
			info.AddValue("StatusCode", this.StatusCode);
			base.GetObjectData(info, context);
		}

	}
}