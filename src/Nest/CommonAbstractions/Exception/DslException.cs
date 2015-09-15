using System;

namespace Nest
{
#if !DOTNETCORE
	[Serializable]
#endif
	public class DslException : System.Exception
	{
		public DslException(string msg) : base(msg)
		{
		}

		public DslException(string msg, System.Exception exp) : base(msg, exp)
		{
		}
	}
}