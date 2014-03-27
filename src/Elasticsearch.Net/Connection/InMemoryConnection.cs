using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public class InMemoryConnection : HttpConnection
	{
		private byte[] _fixedResultBytes = Encoding.UTF8.GetBytes("{ \"USING NEST IN MEMORY CONNECTION\"  : null }");

		public InMemoryConnection(IConnectionConfigurationValues settings)
			: base(settings)
		{

		}

		public InMemoryConnection(IConnectionConfigurationValues settings, string fixedResult)
			: this (settings)
		{
			_fixedResultBytes = Encoding.UTF8.GetBytes(fixedResult);
		}

		protected override ElasticsearchResponse<T> DoSynchronousRequest<T>(HttpWebRequest request, byte[] data = null, object deserializationState = null)
		{
			return this.ReturnConnectionStatus<T>(request, data, deserializationState);
		}

		private ElasticsearchResponse<T> ReturnConnectionStatus<T>(HttpWebRequest request, byte[] data, object deserializationState = null)
		{
			var method = request.Method;
			var path = request.RequestUri.ToString();

			using (var ms = new MemoryStream(_fixedResultBytes))
			{
				var cs = ElasticsearchResponse<T>.Create(this._ConnectionSettings, 200, method, path, data, ms, deserializationState);
				_ConnectionSettings.ConnectionStatusHandler(cs);
				return cs;
			}
		}

		protected override Task<ElasticsearchResponse<T>> DoAsyncRequest<T>(HttpWebRequest request, byte[] data = null, object deserializationState = null)
		{
			return Task.Factory.StartNew<ElasticsearchResponse<T>>(() =>
			{
				var cs = this.ReturnConnectionStatus<T>(request, data, deserializationState);
				return cs;
			});
		}

	}
}
