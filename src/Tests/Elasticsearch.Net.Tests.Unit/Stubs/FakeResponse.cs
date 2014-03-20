using System.Collections.Generic;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.Tests.Unit.Stubs
{
	public static class FakeResponse
	{
		public static ElasticsearchResponse<Dictionary<string, object>> Ok(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/")
		{
			return ElasticsearchResponse<Dictionary<string, object>>.Create(config, 200, method, path, null);
		}
		
		public static ElasticsearchResponse<Dictionary<string, object>> Bad(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/")
		{
			return ElasticsearchResponse<Dictionary<string, object>>.Create(config, 503, method, path, null);
		}
		public static ElasticsearchResponse<Dictionary<string, object>> Any(
			IConnectionConfigurationValues config, 
			int statusCode,
			string method = "GET",
			string path = "/")
		{
			return ElasticsearchResponse<Dictionary<string, object>>.Create(config, statusCode, method, path, null);
		}
	}
}