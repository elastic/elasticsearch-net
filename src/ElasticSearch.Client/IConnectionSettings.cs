using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public interface IConnectionSettings
	{
		string Host { get; }
		int Port { get; }
		int TimeOut { get; }
		string ProxyAddress { get; }
		string Username { get;  }
		string Password { get; }
		string DefaultIndex { get; }
		
	}
}
