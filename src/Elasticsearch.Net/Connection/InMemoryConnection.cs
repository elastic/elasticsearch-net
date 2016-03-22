using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class InMemoryConnection : IConnection
	{
		private readonly byte[] _responseBody;
		private readonly int _statusCode;
		private readonly Exception _exception;

		public InMemoryConnection()
		{
			_statusCode = 200;
		}

		public InMemoryConnection(byte[] responseBody, int statusCode = 200, Exception exception = null)
		{
			_responseBody = responseBody;
			_statusCode = statusCode;
			_exception = exception;
		}

		public virtual async Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData) where TReturn : class =>
			await this.ReturnConnectionStatusAsync<TReturn>(requestData).ConfigureAwait(false);

		public virtual ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData) where TReturn : class =>
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
				Exception = _exception
			};
			var cs = builder.ToResponse();
			return cs;
		}

		protected async Task<ElasticsearchResponse<TReturn>> ReturnConnectionStatusAsync<TReturn>(RequestData requestData, byte[] responseBody = null, int? statusCode = null)
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
							await data.WriteAsync(zipStream, requestData.ConnectionSettings).ConfigureAwait(false);
					else
						await data.WriteAsync(stream, requestData.ConnectionSettings).ConfigureAwait(false);
				}
			}

			var builder = new ResponseBuilder<TReturn>(requestData)
			{
				StatusCode = statusCode ?? this._statusCode,
				Stream = (body != null) ? new MemoryStream(body) : null,
				Exception = _exception
			};
			var cs = await builder.ToResponseAsync().ConfigureAwait(false);
			return cs;
		}

		void IDisposable.Dispose() => DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
