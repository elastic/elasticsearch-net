using System;
using System.Collections.Generic;
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
		ElasticsearchResponse Create();
	}

	public class ResponseGenerator : IResponseGenerator
	{
		public virtual ElasticsearchResponse Create()
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


		public virtual Task<ElasticsearchResponse> Get(Uri uri)
		{
			return DoAsyncRequest(uri);
		}

		public virtual ElasticsearchResponse GetSync(Uri uri)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Head(Uri uri)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse HeadSync(Uri uri)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Post(Uri uri, byte[] data)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse PostSync(Uri uri, byte[] data)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Put(Uri uri, byte[] data)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse PutSync(Uri uri, byte[] data)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Delete(Uri uri)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse DeleteSync(Uri uri)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Delete(Uri uri, byte[] data)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse DeleteSync(Uri uri, byte[] data)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public bool Ping(Uri uri, int connectTimeout)
		{
			throw new NotImplementedException();
		}

		public IList<Uri> Sniff(Uri uri, int connectTimeout)
		{
			throw new NotImplementedException();
		}

		private Task<ElasticsearchResponse> DoAsyncRequest(Uri uri)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}
	}
}
