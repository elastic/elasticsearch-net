using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.Tests.Unit.Stubs
{
	public static class FakeResponse
	{
		public static ElasticsearchResponse<Stream> Ok(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/",
			Stream response = null)
		{
			return ElasticsearchResponse<Stream>.Create(config, 200, method, path, null, response);
		}

		public static Task<ElasticsearchResponse<Stream>> OkAsync(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/",
			Stream response = null)
		{
			response = response ?? new MemoryStream(Encoding.UTF8.GetBytes("{}"));
			return Task.FromResult(ElasticsearchResponse<Stream>.Create(config, 200, method, path, null, response));
		}

		public static ElasticsearchResponse<Stream> Bad(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/",
			Stream response = null)
		{
			return ElasticsearchResponse<Stream>.Create(config, 503, method, path, null, response);
		}
		
		public static Task<ElasticsearchResponse<Stream>> BadAsync(
			IConnectionConfigurationValues config, 
			string method = "GET",
			string path = "/",
			Stream response = null)
		{
			return Task.FromResult(ElasticsearchResponse<Stream>.Create(config, 503, method, path, null, response));
		}

		public static ElasticsearchResponse<Stream> Any(
			IConnectionConfigurationValues config, 
			int statusCode,
			string method = "GET",
			string path = "/",
			Stream response = null)
		{
			return ElasticsearchResponse<Stream>.Create(config, statusCode, method, path, null, response);
		}
		
		public static Task<ElasticsearchResponse<Stream>> AnyAsync(
			IConnectionConfigurationValues config, 
			int statusCode,
			string method = "GET",
			string path = "/",
			Stream response = null)
		{
			return Task.FromResult(ElasticsearchResponse<Stream>.Create(config, statusCode, method, path, null, response));
		}
	}
}