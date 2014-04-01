using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface IConnection
	{

		Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null);
		ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null);
		ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null);
		ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null);
		ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null);
		ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null);
		ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null);

	}
}
