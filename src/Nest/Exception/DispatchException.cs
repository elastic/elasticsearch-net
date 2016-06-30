using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

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

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected DispatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }

	}
}