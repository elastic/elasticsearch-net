using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Nest
{
	[Serializable]
	public class DslException : Exception
	{
		public DslException(string msg) : base(msg) { }

		public DslException(string msg, System.Exception exp) : base(msg, exp) { }

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected DslException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}