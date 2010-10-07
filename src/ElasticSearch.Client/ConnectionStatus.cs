using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace ElasticSearch.Client
{
	class ConnectionStatus
	{
		public bool Success { get; private set; }
		public ConnectionError Error { get; private set; }
		public string Result { get; private set; }

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
