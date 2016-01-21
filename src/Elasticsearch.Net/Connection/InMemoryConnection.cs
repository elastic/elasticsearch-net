using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class InMemoryConnection : HttpConnection
	{
		private readonly byte[] _responseBody;
		private readonly int _statusCode;

		public InMemoryConnection() 
		{
			_statusCode = 200;
		}

		public InMemoryConnection(byte[] responseBody, int statusCode = 200) 
		{
			_responseBody = responseBody;
			_statusCode = statusCode;
		}

		public override Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData) =>
			Task.FromResult(this.ReturnConnectionStatus<TReturn>(requestData));

		public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData) => 
			this.ReturnConnectionStatus<TReturn>(requestData);

		protected ElasticsearchResponse<TReturn> ReturnConnectionStatus<TReturn>(RequestData requestData, byte[] responseBody = null, int? statusCode = null)
			where TReturn : class
		{
			var body = responseBody ?? _responseBody;
            var data = requestData.PostData;
            if (data != null)
            {
                using (var stream = new MemoryStream())
                {
                    if (requestData.HttpCompression)
                        using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
                            data.Write(zipStream, requestData.ConnectionSettings);
                    else
                        data.Write(stream, requestData.ConnectionSettings);
                }
            }

            var builder = new ResponseBuilder<TReturn>(requestData)
			{
				StatusCode = statusCode ?? this._statusCode,
				Stream = (body != null) ? new MemoryStream(body) : null,
			};
			var cs = builder.ToResponse();
			return cs;
		}
	}
}
