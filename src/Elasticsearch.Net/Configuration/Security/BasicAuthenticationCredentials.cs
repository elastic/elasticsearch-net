namespace Elasticsearch.Net
{
	public class BasicAuthenticationCredentials
	{
		public BasicAuthenticationCredentials() { }

		public BasicAuthenticationCredentials(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public string Password { get; set; }
		public string Username { get; set; }

		public override string ToString() => $"{Username}:{Password}";
	}
}
