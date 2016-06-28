using System;
using System.Runtime.Serialization;

namespace Nest
{
	[Serializable]
	public class DslException : Exception
	{
		public DslException(string msg) : base(msg) { }

		public DslException(string msg, System.Exception exp) : base(msg, exp) { }

		protected DslException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}