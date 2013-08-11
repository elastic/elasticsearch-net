using System;

namespace Nest
{
	public class ReindexException: Exception
	{
		public ConnectionStatus Status { get; private set; }

		public ReindexException(ConnectionStatus status, string message = null) : base(message)
		{
			this.Status = status;
		}
	}
}