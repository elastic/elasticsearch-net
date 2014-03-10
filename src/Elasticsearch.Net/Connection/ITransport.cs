using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface ITransport
	{
		ElasticsearchResponse DoRequest(string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0);

		Task<ElasticsearchResponse> DoRequestAsync(
			string method, 
			string path, 
			object data = null, NameValueCollection queryString = null, int retried = 0);
	}
}