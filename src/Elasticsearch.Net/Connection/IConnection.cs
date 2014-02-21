using System;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface IConnection
	{
		Task<ElasticsearchResponse> Get(string path);
		ElasticsearchResponse GetSync(string path);

		Task<ElasticsearchResponse> Head(string path);
		ElasticsearchResponse HeadSync(string path);

		Task<ElasticsearchResponse> Post(string path, byte[] data);
		ElasticsearchResponse PostSync(string path, byte[] data);

		Task<ElasticsearchResponse> Put(string path, byte[] data);
		ElasticsearchResponse PutSync(string path, byte[] data);

		Task<ElasticsearchResponse> Delete(string path);
		ElasticsearchResponse DeleteSync(string path);

		Task<ElasticsearchResponse> Delete(string path, byte[] data);
		ElasticsearchResponse DeleteSync(string path, byte[] data);
	}
}
