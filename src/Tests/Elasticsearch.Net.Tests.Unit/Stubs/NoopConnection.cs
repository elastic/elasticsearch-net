using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;

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


		public virtual Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return DoAsyncRequest(uri);
		}

		public virtual ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null) 
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public bool Ping(Uri uri)
		{
			return true;
		}

		public IList<Uri> Sniff(Uri uri)
		{
			throw new NotImplementedException();
		}

		private Task<ElasticsearchResponse<Stream>> DoAsyncRequest(Uri uri)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}
	}
}
