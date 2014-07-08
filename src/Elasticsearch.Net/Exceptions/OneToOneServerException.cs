namespace Elasticsearch.Net.Exceptions
{
	internal class OneToOneServerException
	{
		public int status { get; set; }
		public string error { get; set; }
	}
}