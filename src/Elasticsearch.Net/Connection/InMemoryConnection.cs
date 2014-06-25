using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public class InMemoryConnection : IConnection
	{
		protected readonly IConnectionConfigurationValues _settings;
		public IConnectionConfigurationValues ConnectionSettings { get { return _settings;}}
		private byte[] _fixedResultBytes = Encoding.UTF8.GetBytes("{ \"USING NEST IN MEMORY CONNECTION\"  : null }");
		
		public InMemoryConnection() : this(new ConnectionConfiguration()) { }

		public InMemoryConnection(IConnectionConfigurationValues settings)
		{
			_settings = settings;
		}

		public InMemoryConnection(IConnectionConfigurationValues settings, string fixedResult) : this(settings)
		{
			_fixedResultBytes = Encoding.UTF8.GetBytes(fixedResult);
		}

		private ElasticsearchResponse<Stream> Result(string method, string path,  byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{

			var cs = ElasticsearchResponse<Stream>.Create(this._settings, 200, method, path, data);
			cs.Response = new MemoryStream(_fixedResultBytes);
			return cs;
		}

		protected Task<ElasticsearchResponse<Stream>> ResultAsync(string method, string path, byte[] data = null, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return Task.Factory.StartNew(() =>
			{
				var cs = this.Result(method, path, data, requestSpecificConfig);
				return cs;
			});
		}

		public virtual Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.ResultAsync("GET", uri.ToString(), null, requestSpecificConfig);
		}

		public virtual ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.Result("GET", uri.ToString(), null, requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.ResultAsync("HEAD", uri.ToString(), null, requestSpecificConfig);
		}

		public virtual ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.Result("HEAD", uri.ToString(), null, requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.ResultAsync("POST", uri.ToString(), data, requestSpecificConfig);
		}

		public virtual ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.Result("POST", uri.ToString(), data, requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.ResultAsync("PUT", uri.ToString(), data, requestSpecificConfig);
		}

		public virtual ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.Result("PUT", uri.ToString(), data, requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.ResultAsync("DELETE", uri.ToString(), null, requestSpecificConfig);
		}

		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.Result("DELETE", uri.ToString(), null, requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.ResultAsync("DELETE", uri.ToString(), data, requestSpecificConfig);
		}

		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConnectionConfiguration requestSpecificConfig = null)
		{
			return this.Result("DELETE", uri.ToString(), data, requestSpecificConfig);
		}
	}
}
