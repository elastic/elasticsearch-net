using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Occurs when an IElasticClient call does not have 
	/// enough information to dispatch into the raw client.
	/// </summary>
	[Serializable]
	public class DispatchException : Exception
	{
		public DispatchException(string msg) : base(msg) { }

		public DispatchException(string msg, System.Exception exp) : base(msg, exp) { }

        protected DispatchException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

	}
}