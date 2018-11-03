using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class InMemoryConnection : IConnection
	{
		internal static readonly byte[] EmptyBody = Encoding.UTF8.GetBytes("");
		private readonly string _contentType;
		private readonly Exception _exception;
		private readonly byte[] _responseBody;
		private readonly int _statusCode;

		/// <summary>
		/// Every request will succeed with this overload, note that it won't actually return mocked responses
		/// so using this overload might fail if you are using it to test high level bits that need to deserialize the response.
		/// </summary>
		public InMemoryConnection() => _statusCode = 200;

		public InMemoryConnection(byte[] responseBody, int statusCode = 200, Exception exception = null, string contentType = RequestData.MimeType)
		{
			_responseBody = responseBody;
			_statusCode = statusCode;
			_exception = exception;
			_contentType = contentType;
		}

		public virtual TResponse Request<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new() =>
			ReturnConnectionStatus<TResponse>(requestData);

		public virtual Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new() =>
			ReturnConnectionStatusAsync<TResponse>(requestData, cancellationToken);

		void IDisposable.Dispose() => DisposeManagedResources();

		protected TResponse ReturnConnectionStatus<TResponse>(RequestData requestData, byte[] responseBody = null, int? statusCode = null,
			string contentType = null
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			var body = responseBody ?? _responseBody;
			var data = requestData.PostData;
			if (data != null)
			{
				using (var stream = requestData.MemoryStreamFactory.Create())
				{
					if (requestData.HttpCompression)
						using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
							data.Write(zipStream, requestData.ConnectionSettings);
					else
						data.Write(stream, requestData.ConnectionSettings);
				}
			}
			requestData.MadeItToResponse = true;

			var sc = statusCode ?? _statusCode;
			Stream s = body != null ? requestData.MemoryStreamFactory.Create(body) : requestData.MemoryStreamFactory.Create(EmptyBody);
			return ResponseBuilder.ToResponse<TResponse>(requestData, _exception, sc, null, s, contentType ?? _contentType ?? RequestData.MimeType);
		}

		protected async Task<TResponse> ReturnConnectionStatusAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken,
			byte[] responseBody = null, int? statusCode = null, string contentType = null
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			var body = responseBody ?? _responseBody;
			var data = requestData.PostData;
			if (data != null)
			{
				using (var stream = requestData.MemoryStreamFactory.Create())
				{
					if (requestData.HttpCompression)
						using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
							await data.WriteAsync(zipStream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);
					else
						await data.WriteAsync(stream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);
				}
			}
			requestData.MadeItToResponse = true;

			var sc = statusCode ?? _statusCode;
			Stream s = body != null ? requestData.MemoryStreamFactory.Create(body) : requestData.MemoryStreamFactory.Create(EmptyBody);
			return await ResponseBuilder
				.ToResponseAsync<TResponse>(requestData, _exception, sc, null, s, contentType ?? _contentType, cancellationToken)
				.ConfigureAwait(false);
		}

		protected virtual void DisposeManagedResources() { }
	}
}
