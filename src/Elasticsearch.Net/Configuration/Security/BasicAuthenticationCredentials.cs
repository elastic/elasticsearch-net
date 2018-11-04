namespace Elasticsearch.Net
{
	public class BasicAuthenticationCredentials
	{
		public string Password { get; set; }
		public string Username { get; set; }

		public override string ToString() => $"{Username}:{Password}";
	}
}
