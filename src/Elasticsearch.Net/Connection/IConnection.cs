using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection
{
	public interface IConnection
	{
		/// <summary>
		/// Signals to sniff which type of adresses are valid for this connection implementation
		/// </summary>
		string[] PreferedTransportOrder { get; }

		Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConfiguration requestConfiguration = null);
		ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConfiguration requestConfiguration = null);

		Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConfiguration requestConfiguration = null);
		ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConfiguration requestConfiguration = null);

		Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null);
		ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null);

		Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null);
		ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null);

		Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConfiguration requestConfiguration = null);
		ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConfiguration requestConfiguration = null);

		Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null);
		ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null);
		
		
	}
}
