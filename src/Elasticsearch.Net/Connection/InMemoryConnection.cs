using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection
{
	public class InMemoryConnection : HttpConnection
	{
		private byte[] _fixedResultBytes = Encoding.UTF8.GetBytes("{ \"USING NEST IN MEMORY CONNECTION\"  : null }");
		private int _statusCode;

		public List<Tuple<string, Uri, byte[]>> Requests = new List<Tuple<string, Uri, byte[]>>(); 
		
		public bool RecordRequests { get; set;}

		public InMemoryConnection() : base(new ConnectionConfiguration())
		{
			
		}
		public InMemoryConnection(IConnectionConfigurationValues settings) : base(settings)
		{
			_statusCode = 200;
		}

		public InMemoryConnection(IConnectionConfigurationValues settings, string fixedResult, int statusCode = 200) : this(settings)
		{
			_fixedResultBytes = Encoding.UTF8.GetBytes(fixedResult);
			_statusCode = statusCode;
		}

		public override Task<ElasticsearchResponse<Stream>> DoRequest(HttpMethod method, Uri uri, byte[] data = null, IRequestConfiguration requestSpecificConfig = null)
		{

			return Task.Factory.StartNew(() =>
			{
				var cs = this.ReturnConnectionStatus(method, uri, data, requestSpecificConfig);
				return cs;
			});
		}

		public override ElasticsearchResponse<Stream> DoRequestSync(HttpMethod method, Uri uri, byte[] data = null,
			IRequestConfiguration requestSpecificConfig = null)
		{
			return this.ReturnConnectionStatus(method, uri, data, requestSpecificConfig);
		}

		private ElasticsearchResponse<Stream> ReturnConnectionStatus(HttpMethod method, Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			var path = uri.ToString();

			var cs = ElasticsearchResponse<Stream>.Create(this._settings, _statusCode, method.ToString(), path, data);
			cs.Response = new MemoryStream(_fixedResultBytes);
			if (this._settings.ConnectionStatusHandler != null)
				this._settings.ConnectionStatusHandler(cs);

			if (this.RecordRequests)
			{
				this.Requests.Add(Tuple.Create(method.ToString(), uri, data));
			}

			return cs;
		}

	}
}
