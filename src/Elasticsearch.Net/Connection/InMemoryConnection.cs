// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class InMemoryHttpResponse
	{
		public int StatusCode { get; set; } = 200;
		public byte[] ResponseBytes { get; set; } = Array.Empty<byte>();
		public Dictionary<string, List<string>> Headers { get; set; } = new();
		public string ContentType { get; set; }
	}

	public class InMemoryConnection : IConnection
	{
		private readonly string _basePath = "/";
		private const string DefaultProductName = "Elasticsearch";
		private static readonly byte[] EmptyBody = Encoding.UTF8.GetBytes("");
		private readonly string _contentType;
		private readonly Exception _exception;
		private readonly byte[] _responseBody;
		private readonly int _statusCode;
		private readonly InMemoryHttpResponse _productCheckResponse;
		private readonly string _productHeader;

		public static InMemoryHttpResponse ValidProductCheckResponse(string productName = null)
		{
			var responseJson = new
			{
				name = "es01",
				cluster_name = "elasticsearch-test-cluster",
				version = new
				{
					number = "7.14.0",
					build_flavor = "default",
					build_hash = "af1dc6d8099487755c3143c931665b709de3c764",
					build_timestamp = "2020-08-11T21:36:48.204330Z",
					build_snapshot = false,
					lucene_version = "8.6.0"
				},
				tagline = "You Know, for Search"
			};

			using var ms = RecyclableMemoryStreamFactory.Default.Create();
			LowLevelRequestResponseSerializer.Instance.Serialize(responseJson, ms);

			var response = new InMemoryHttpResponse
			{
				ResponseBytes = ms.ToArray()
			};

			response.Headers.Add("X-elastic-product", new List<string>{ productName ?? DefaultProductName });

			return response;
		}

		/// <summary>
		/// Every request will succeed with this overload, note that it won't actually return mocked responses
		/// so using this overload might fail if you are using it to test high level bits that need to deserialize the response.
		/// </summary>
		public InMemoryConnection()
		{
			_statusCode = 200;
			_productCheckResponse = ValidProductCheckResponse();
		}

		public InMemoryConnection(string basePath) : this() => _basePath = $"/{basePath.Trim('/')}/";

		public InMemoryConnection(InMemoryHttpResponse productCheckResponse = null, int statusCode = 200, string productHeader = null)
		{
			_statusCode = statusCode;
			_productCheckResponse = productCheckResponse ?? ValidProductCheckResponse();
			_productHeader = productHeader;
		}

		public InMemoryConnection(
			byte[] responseBody,
			int statusCode = 200,
			Exception exception = null,
			string contentType = null,
			InMemoryHttpResponse productCheckResponse = null,
			string productNameFromHeader = null) : this(productCheckResponse, statusCode, productNameFromHeader)
		{
			_responseBody = responseBody;
			_exception = exception;
			_contentType = contentType ?? RequestData.DefaultJsonMimeType;
		}

		public virtual TResponse Request<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new() =>
			ReturnConnectionStatus<TResponse>(requestData);

		public virtual Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new() =>
			ReturnConnectionStatusAsync<TResponse>(requestData, cancellationToken);

		void IDisposable.Dispose() => DisposeManagedResources();
		
		protected TResponse ReturnConnectionStatus<TResponse>(
			RequestData requestData,
			byte[] responseBody = null,
			int? statusCode = null,
			string contentType = null,
			InMemoryHttpResponse productCheckResponse = null
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (_basePath.Equals(requestData.Uri.AbsolutePath, StringComparison.Ordinal) && requestData.Method == HttpMethod.GET)
				return ReturnProductCheckResponse<TResponse>(requestData, statusCode, productCheckResponse);

			var body = responseBody ?? _responseBody;
			var data = requestData.PostData;

			if (data is not null)
			{
				using var stream = requestData.MemoryStreamFactory.Create();

				if (requestData.HttpCompression)
					using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
						data.Write(zipStream, requestData.ConnectionSettings);
				else
					data.Write(stream, requestData.ConnectionSettings);
			}

			requestData.MadeItToResponse = true;

			var sc = statusCode ?? _statusCode;
			Stream s = body != null ? requestData.MemoryStreamFactory.Create(body) : requestData.MemoryStreamFactory.Create(EmptyBody);
			return ResponseBuilder.ToResponse<TResponse>(requestData, _exception, sc, null, s, contentType ?? _contentType ?? RequestData.DefaultJsonMimeType);
		}

		protected async Task<TResponse> ReturnConnectionStatusAsync<TResponse>(
			RequestData requestData,
			CancellationToken cancellationToken,
			byte[] responseBody = null,
			int? statusCode = null,
			string contentType = null,
			InMemoryHttpResponse productCheckResponse = null
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			if (_basePath.Equals(requestData.Uri.AbsolutePath, StringComparison.Ordinal) && requestData.Method == HttpMethod.GET)
				return ReturnProductCheckResponse<TResponse>(requestData, statusCode, productCheckResponse);

			var body = responseBody ?? _responseBody;
			var data = requestData.PostData;

			if (data != null)
			{
				using var stream = requestData.MemoryStreamFactory.Create();
				if (requestData.HttpCompression)
					using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
						await data.WriteAsync(zipStream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);
				else
					await data.WriteAsync(stream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);
			}
			requestData.MadeItToResponse = true;

			var sc = statusCode ?? _statusCode;
			Stream s = body != null ? requestData.MemoryStreamFactory.Create(body) : requestData.MemoryStreamFactory.Create(EmptyBody);
			return await ResponseBuilder
				.ToResponseAsync<TResponse>(requestData, _exception, sc, null, s, contentType ?? _contentType, _productHeader, cancellationToken)
				.ConfigureAwait(false);
		}

		private TResponse ReturnProductCheckResponse<TResponse>(
			RequestData requestData,
			int? statusCode = null,
			InMemoryHttpResponse productCheckResponse = null
		) where TResponse : class, IElasticsearchResponse, new()
		{
			productCheckResponse ??= _productCheckResponse;
			productCheckResponse.Headers.TryGetValue("X-elastic-product", out var productNames);

			requestData.MadeItToResponse = true;

			using var ms = requestData.MemoryStreamFactory.Create(productCheckResponse.ResponseBytes);

			return ResponseBuilder.ToResponse<TResponse>(
				requestData, _exception, statusCode ?? productCheckResponse.StatusCode, null, ms,
				RequestData.DefaultJsonMimeType, productNames?.FirstOrDefault());
		}

		protected virtual void DisposeManagedResources() { }
	}
}
