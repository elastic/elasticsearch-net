namespace Elasticsearch.Net
{
	public class BasicAuthenticationCredentials
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public override string ToString() => $"{this.Username}:{this.Password}";
	}
}
