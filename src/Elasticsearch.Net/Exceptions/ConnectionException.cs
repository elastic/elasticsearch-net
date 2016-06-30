using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Elasticsearch.Net.Connection
{
	[Serializable]
	public class ConnectionException : Exception
	{
		public int HttpStatusCode { get; private set; }
		public ConnectionException(int statusCode = -1, string response = null) : base(Enum.GetName(typeof(HttpStatusCode), statusCode))
		{
			this.HttpStatusCode = statusCode;
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected ConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.HttpStatusCode = info.GetInt32("HttpStatusCode");
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException(nameof(info));

			info.AddValue("HttpStatusCode", this.HttpStatusCode);
			base.GetObjectData(info, context);
		}
	}
}