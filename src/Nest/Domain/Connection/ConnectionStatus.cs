using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace Nest
{
	public class ConnectionStatus
	{
		public bool Success { get; private set; }
		public ConnectionError Error { get; private set; }
		public string Result { get; internal set; }
		public string Request { get; internal set; }

		public ConnectionStatus(Exception e)
		{
			this.Success = false;
			this.Error = new ConnectionError(e);
			this.Result = this.Error.Response;
		} 

		public ConnectionStatus(ConnectionError error)
		{
			this.Success = false;
			if (error != null)
			{ 
				this.Error = error;
				this.Result = this.Error.Response;
			}
		}
		public ConnectionStatus(string result)
		{
			this.Success = true;
			this.Result = result;
		}

	}
}
