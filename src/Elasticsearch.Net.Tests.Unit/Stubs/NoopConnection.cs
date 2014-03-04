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


		public virtual Task<ElasticsearchResponse> Get(string path)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse GetSync(string path)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Head(string path)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse HeadSync(string path)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Post(string path, byte[] data)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse PostSync(string path, byte[] data)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Put(string path, byte[] data)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse PutSync(string path, byte[] data)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Delete(string path)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse DeleteSync(string path)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}

		public virtual Task<ElasticsearchResponse> Delete(string path, byte[] data)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create());
		}

		public virtual ElasticsearchResponse DeleteSync(string path, byte[] data)
		{
			var uri = _configValues.ConnectionPool.GetNext();
			_uriObserver.Observe(uri);
			return _responseGenerator.Create();
		}
	}
}
