namespace Elasticsearch.Net
{
	public class StringResponse : ElasticsearchResponse<string>
	{
		public StringResponse() { }
		public StringResponse(string body) => this.Body = body;
	}
}
