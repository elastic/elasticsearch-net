using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private int _statusCode;

		public List<Tuple<string, Uri, PostData<object>>> Requests = new List<Tuple<string, Uri, PostData<object>>>(); 
		
		public bool RecordRequests { get; set;}

		public InMemoryConnection() 
		{
			_statusCode = 200;
		}

		public InMemoryConnection(string fixedResult, int statusCode = 200) 
		{
			_fixedResultBytes = Encoding.UTF8.GetBytes(fixedResult);
			_statusCode = statusCode;
		}

		public override Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData) =>
			Task.FromResult(this.ReturnConnectionStatus<TReturn>(requestData));

		public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData) => 
			this.ReturnConnectionStatus<TReturn>(requestData);

		protected ElasticsearchResponse<TReturn> ReturnConnectionStatus<TReturn>(RequestData requestData, byte[] fixedResult = null)
			where TReturn : class
		{
			var request = this.CreateHttpWebRequest(requestData);
			var method = request.Method;
			var path = request.RequestUri.ToString();

			var cs = requestData.CreateResponse<TReturn>(this._statusCode, new MemoryStream(fixedResult ?? _fixedResultBytes));
			if (this.RecordRequests)
			{
				this.Requests.Add(Tuple.Create(method, request.RequestUri, requestData.Data));
			}

			return cs;
		}

	}
}
