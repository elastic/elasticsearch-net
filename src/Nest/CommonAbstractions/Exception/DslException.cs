using System;

namespace Nest
{
	[Serializable]
	public class DslException : System.Exception
	{
		public object Offender { get; set; }

		public DslException(string msg) : base(msg)
		{
		}

		public DslException(string msg, System.Exception exp) : base(msg, exp)
		{
		}
	}
}