using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.Tests.Unit.Stubs
{
	public static class FakeResponse
	{
		public static ElasticsearchResponse<DynamicDictionary> Ok(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/")
		{
			return ElasticsearchResponse<DynamicDictionary>.Create(config, 200, method, path, null);
		}
		
		public static ElasticsearchResponse<DynamicDictionary> Bad(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/")
		{
			return ElasticsearchResponse<DynamicDictionary>.Create(config, 503, method, path, null);
		}
		public static ElasticsearchResponse<DynamicDictionary> Any(
			IConnectionConfigurationValues config, 
			int statusCode,
			string method = "GET",
			string path = "/")
		{
			return ElasticsearchResponse<DynamicDictionary>.Create(config, statusCode, method, path, null);
		}
	}
}