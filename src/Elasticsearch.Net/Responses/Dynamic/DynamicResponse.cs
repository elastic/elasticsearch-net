namespace Elasticsearch.Net
{
	public class DynamicResponse : ElasticsearchResponse<dynamic>
	{
		public DynamicResponse() { }

		public DynamicResponse(DynamicDictionary dictionary) => Body = dictionary;
	}
}
