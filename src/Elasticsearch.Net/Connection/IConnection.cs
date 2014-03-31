using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface IRequestConnectionConfiguration
	{
		int RequestTimeout { get; }
		int ConnectTimeout { get; }
	}

	public interface IRequestConfiguration : IRequestConnectionConfiguration
	{
		/// <summary>
		/// This will override whatever is set on the connection configuration or whatever default the connectionpool has.
		/// </summary>
		int? MaxRetries { get; }

		/// <summary>
		/// This will force the operation on the specified node, this will bypass any configured connection pool and will no retry.
		/// </summary>
		Uri ForceNode { get; set; }
	}

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

		bool Ping(Uri uri);
		IList<Uri> Sniff(Uri uri);

	}
}
