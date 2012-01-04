namespace ElasticSearch.Client
{
	public class ElasticSearchException : System.Exception
	{
		public ElasticSearchException(string msg) : base(msg)
		{
		}

		public ElasticSearchException(string msg, System.Exception exp) : base(msg, exp)
		{
		}
	}
}