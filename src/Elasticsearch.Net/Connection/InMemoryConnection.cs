using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection
{
	public class InMemoryConnection : HttpConnection
	{
		private byte[] _fixedResultBytes = Encoding.UTF8.GetBytes("{ \"USING NEST IN MEMORY CONNECTION\"  : null }");
		public InMemoryConnection()
			: base(new ConnectionConfiguration())
		{
			
		}
		public InMemoryConnection(IConnectionConfigurationValues settings)
			: base(settings)
		{

		}

		public InMemoryConnection(IConnectionConfigurationValues settings, string fixedResult)
			: this(settings)
		{
			_fixedResultBytes = Encoding.UTF8.GetBytes(fixedResult);
		}

		protected override ElasticsearchResponse<Stream> DoSynchronousRequest(HttpWebRequest request, byte[] data = null, IRequestConfiguration requestSpecificConfig = null)
		{
			return this.ReturnConnectionStatus(request, data, requestSpecificConfig);
		}

		private ElasticsearchResponse<Stream> ReturnConnectionStatus(HttpWebRequest request, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			var method = request.Method;
			var path = request.RequestUri.ToString();

			var cs = ElasticsearchResponse<Stream>.Create(this.ConnectionSettings, 200, method, path, data);
			cs.Response = new MemoryStream(_fixedResultBytes);
			if (this.ConnectionSettings.ConnectionStatusHandler != null)
				this.ConnectionSettings.ConnectionStatusHandler(cs);
			return cs;
		}

		protected override Task<ElasticsearchResponse<Stream>> DoAsyncRequest(HttpWebRequest request, byte[] data = null, IRequestConfiguration requestSpecificConfig = null)
		{
			return Task.Factory.StartNew(() =>
			{
				var cs = this.ReturnConnectionStatus(request, data, requestSpecificConfig);
				return cs;
			});
		}

	}
}
