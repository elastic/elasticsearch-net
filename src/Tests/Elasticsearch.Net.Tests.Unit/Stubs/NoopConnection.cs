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
		ElasticsearchResponse<T> Create<T>();
	}

	public class ResponseGenerator : IResponseGenerator
	{
		public virtual ElasticsearchResponse<T> Create<T>()
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


		public virtual Task<ElasticsearchResponse<T>> Get<T>(Uri uri, object deserializationState = null)
		{
			return DoAsyncRequest<T>(uri);
		}

		public virtual ElasticsearchResponse<T> GetSync<T>(Uri uri, object deserializationState = null) 
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create<T>();
		}

		public virtual Task<ElasticsearchResponse<T>> Head<T>(Uri uri, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create<T>());
		}

		public virtual ElasticsearchResponse<T> HeadSync<T>(Uri uri, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create<T>();
		}

		public virtual Task<ElasticsearchResponse<T>> Post<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create<T>());
		}

		public virtual ElasticsearchResponse<T> PostSync<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create<T>();
		}

		public virtual Task<ElasticsearchResponse<T>> Put<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create<T>());
		}

		public virtual ElasticsearchResponse<T> PutSync<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create<T>();
		}

		public virtual Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create<T>());
		}

		public virtual ElasticsearchResponse<T> DeleteSync<T>(Uri uri, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create<T>();
		}

		public virtual Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create<T>());
		}

		public virtual ElasticsearchResponse<T> DeleteSync<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			_uriObserver.Observe(uri);
			return _responseGenerator.Create<T>();
		}

		public bool Ping(Uri uri)
		{
			return true;
		}

		public IList<Uri> Sniff(Uri uri)
		{
			throw new NotImplementedException();
		}

		private Task<ElasticsearchResponse<T>> DoAsyncRequest<T>(Uri uri)
		{
			_uriObserver.Observe(uri);
			return Task.FromResult(_responseGenerator.Create<T>());
		}
	}
}
