namespace Nest
{
	public class ConnectionException : System.Exception
	{
		public int HttpStatusCode { get; private set; }
		public string Response { get; private set; }
		public ConnectionException(string msg,  int statusCode = 500, string response = null) : base(msg)
		{
			this.HttpStatusCode = statusCode;
			this.Response = response;
		}

		public ConnectionException(string msg, System.Exception exp, int statusCode = 500, string response = null)
			: base(msg, exp)
		{
			this.HttpStatusCode = statusCode;
			this.Response = response;
		}
	}
}