using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch.Net.Connection.Security
{
	public class BasicAuthenticationCredentials
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		public override string ToString()
		{
			return this.UserName + ":" + this.Password;
		}
	}
}
