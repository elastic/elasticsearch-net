using System;

namespace Elasticsearch.Net
{
	public class BasicAuthenticationCredentials
	{
		public string Username { get; set; }
		[Obsolete("Scheduled to be removed use Username instead, note the lowercase n")]
		public string UserName { get { return Username; } set { Username = value; } }
		public string Password { get; set; }

		public override string ToString()
		{
			return this.Username + ":" + this.Password;
		}
	}
}
