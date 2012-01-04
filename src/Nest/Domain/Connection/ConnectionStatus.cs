using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Nest
{
	public class ConnectionStatus
	{
		public bool Success { get; private set; }
		public ConnectionError Error { get; private set; }
		public string Result { get; internal set; }


		public ConnectionStatus(Exception e)
		{
			this.Success = false;
			this.Error = new ConnectionError(e);
		} 

		public ConnectionStatus(ConnectionError error)
		{
			this.Success = false;
			this.Error = error;
		}
		public ConnectionStatus(string result)
		{
			this.Success = true;
			this.Result = result;
		}
	}
}
