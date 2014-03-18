using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	/// <summary>
	/// 
	/// </summary>
	public class InMemoryConnection : HttpConnection
	{
		private readonly byte[] _fixedResultBytes = Encoding.UTF8.GetBytes("{ \"USING NEST IN MEMORY CONNECTION\"  : null }");

		public InMemoryConnection(IConnectionConfigurationValues settings)
			: base(settings)
		{

		}

		protected override ElasticsearchResponse<T> DoSynchronousRequest<T>(HttpWebRequest request, byte[] data = null, object deserializationState = null)
		{
			return this.ReturnConnectionStatus<T>(request, data);
		}

		private ElasticsearchResponse<T> ReturnConnectionStatus<T>(HttpWebRequest request, byte[] data)
		{
			var method = request.Method;
			var path = request.RequestUri.ToString();

			var cs = ElasticsearchResponse.Create<T>(this._ConnectionSettings, 200, method, path, data);
			_ConnectionSettings.ConnectionStatusHandler(cs);
			return cs;
		}

		protected override Task<ElasticsearchResponse<T>> DoAsyncRequest<T>(HttpWebRequest request, byte[] data = null, object deserializationState = null)
		{
			return Task.Factory.StartNew<ElasticsearchResponse<T>>(() =>
			{
				var cs = this.ReturnConnectionStatus<T>(request, data);
				_ConnectionSettings.ConnectionStatusHandler(cs);
				return cs;
			});
		}

	}
}
