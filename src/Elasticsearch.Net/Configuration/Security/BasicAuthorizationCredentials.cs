using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Connection.Security
{
	// TODO: Rename to BasicAuthenticationCredentials in 2.0
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
