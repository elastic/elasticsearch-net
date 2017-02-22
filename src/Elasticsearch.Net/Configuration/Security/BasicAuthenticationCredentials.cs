namespace Elasticsearch.Net_5_2_0
{
	public class BasicAuthenticationCredentials
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public override string ToString() => $"{this.Username}:{this.Password}";
	}
}
