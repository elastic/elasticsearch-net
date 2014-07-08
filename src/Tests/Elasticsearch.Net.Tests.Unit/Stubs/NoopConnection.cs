using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Tests.Unit.Stubs
{
	public interface IUriObserver
	{
		void Observe(Uri uri);
	}

	public class UriObserver : IUriObserver
	{
		public virtual void Observe(Uri uri) { }
	}

	public interface IResponseGenerator
	{
		ElasticsearchResponse<Stream> Create();
	}

	public class ResponseGenerator : IResponseGenerator
	{
		public virtual ElasticsearchResponse<Stream> Create()
		{
			return null;
		}
	}

	public class NoopConnection : IConnection
	{
		private readonly IConnectionConfigurationValues _configValues;
		private readonly IUriObserver _uriObserver;
		private readonly IResponseGenerator _responseGenerator;

		public NoopConnection(
			IConnectionConfigurationValues configValues
			, IUriObserver uriObserver
			, IResponseGenerator responseGenerator)
		{
			_uriObserver = uriObserver;
			_responseGenerator = responseGenerator;
			_configValues = configValues;
		}


		public virtual Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			return DoAsyncRequest(uri);
		}

		public virtual ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConfiguration requestSpecificConfig = null) 
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		private Task<ElasticsearchResponse<Stream>> DoAsyncRequest(Uri uri)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}
	}
}
