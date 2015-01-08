using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Nest.Tests.Integration.Exceptions
{
	public class ThrowAlwaysConnection : IConnection
	{
		public string[] PreferedTransportOrder { get { return null; }}

		public Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}

		public ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			throw new NotImplementedException();
		}
	}
}