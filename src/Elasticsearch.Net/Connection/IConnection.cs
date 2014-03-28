using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface IConnectionConfigurationOverrides
	{
		int RequestTimeout { get; }
		int ConnectTimeout { get; }
	}

	public interface IConnection
	{

		Task<ElasticsearchResponse<Stream>> Get(Uri uri, IConnectionConfigurationOverrides requestSpecificConfig = null);
		ElasticsearchResponse<Stream> GetSync(Uri uri, IConnectionConfigurationOverrides requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Head(Uri uri, IConnectionConfigurationOverrides requestSpecificConfig = null);
		ElasticsearchResponse<Stream> HeadSync(Uri uri, IConnectionConfigurationOverrides requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IConnectionConfigurationOverrides requestSpecificConfig = null);
		ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IConnectionConfigurationOverrides requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IConnectionConfigurationOverrides requestSpecificConfig = null);
		ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IConnectionConfigurationOverrides requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IConnectionConfigurationOverrides requestSpecificConfig = null);
		ElasticsearchResponse<Stream> DeleteSync(Uri uri, IConnectionConfigurationOverrides requestSpecificConfig = null);

		Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IConnectionConfigurationOverrides requestSpecificConfig = null);
		ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IConnectionConfigurationOverrides requestSpecificConfig = null);

		bool Ping(Uri uri);
		IList<Uri> Sniff(Uri uri);

	}
}
