using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class InMemoryConnection : HttpConnection
	{
		private static readonly byte[] FixedResultBytes = Encoding.UTF8.GetBytes("{ \"USING NEST IN MEMORY CONNECTION\"  : null }");
		private readonly byte[] _fixedBytes;
		private readonly int _statusCode;

		public List<Tuple<string, Uri, PostData<object>>> Requests = new List<Tuple<string, Uri, PostData<object>>>(); 
		
		public InMemoryConnection() 
		{
			_statusCode = 200;
		}

		public InMemoryConnection(string fixedResult, int statusCode = 200) 
		{
			_fixedBytes = Encoding.UTF8.GetBytes(fixedResult);
			_statusCode = statusCode;
		}

		public override Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData) =>
			Task.FromResult(this.ReturnConnectionStatus<TReturn>(requestData));

		public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData) => 
			this.ReturnConnectionStatus<TReturn>(requestData);

		protected ElasticsearchResponse<TReturn> ReturnConnectionStatus<TReturn>(RequestData requestData, byte[] fixedResult = null, int? statusCode = null)
			where TReturn : class
		{
			var cs = requestData.CreateResponse<TReturn>(statusCode ?? this._statusCode, new MemoryStream(fixedResult ?? _fixedBytes ?? FixedResultBytes));
			return cs;
		}

	}
}
