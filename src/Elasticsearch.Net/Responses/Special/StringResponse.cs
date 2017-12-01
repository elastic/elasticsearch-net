namespace Elasticsearch.Net
{
	public class StringResponse : ElasticsearchResponse<string>
	{
		public StringResponse(string body) => this.Body = body;
	}
}
