namespace Elasticsearch.Net
{
	public class DynamicResponse : ElasticsearchResponse<dynamic>
	{
		public DynamicResponse() { }
		public DynamicResponse(DynamicBody body) => this.Body = body;
	}
}
