using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public static class ResponseBuilder

	{
		private const int BufferSize = 81920;

		internal static readonly IDisposable EmptyDisposable = new MemoryStream();

		public static TResponse ToResponse<TResponse>(RequestData requestData, Exception ex, int? statusCode, IEnumerable<string> warnings, Stream responseStream)
			where TResponse : class, IElasticsearchResponse
		{
			var details = Initialize(requestData, ex, statusCode, warnings);
			var response = SetBody<TResponse>(details, requestData, responseStream);
			response.ApiCall = details;
			return response;
		}

		public static async Task<TResponse> ToResponseAsync<TResponse>(
			RequestData requestData,
			Exception ex,
			int? statusCode,
			IEnumerable<string> warnings,
			Stream responseStream,
			CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse
		{
			var details = Initialize(requestData, ex, statusCode, warnings);
			var response = await SetBodyAsync<TResponse>(details, requestData, responseStream, cancellationToken).ConfigureAwait(false);
			response.ApiCall = details;
			return response;
		}

		private static HttpDetails Initialize(RequestData requestData, Exception exception, int? statusCode, IEnumerable<string> warnings)
		{
			var success = false;
			var allowedStatusCodes = requestData.AllowedStatusCodes.ToList();
			if (statusCode.HasValue)
			{
				success = statusCode >= 200 && statusCode < 300
				          || (requestData.Method == HttpMethod.HEAD && statusCode == 404)
				          || allowedStatusCodes.Contains(statusCode.Value)
				          || allowedStatusCodes.Contains(-1);
			}
			var httpCallDetails = new HttpDetails
			{
				Success = success,
				OriginalException = exception,
				HttpStatusCode = statusCode,
				RequestBodyInBytes = requestData.PostData?.WrittenBytes,
				Uri = requestData.Uri,
				HttpMethod = requestData.Method,
				DeprecationWarnings = warnings ?? Enumerable.Empty<string>()
			};
			return httpCallDetails;
		}

		private static TResponse SetBody<TResponse>(HttpDetails details, RequestData requestData, Stream stream)
			where TResponse : class, IElasticsearchResponse
		{
			byte[] bytes = null;
			var disableDirectStreaming = requestData.PostData?.DisableDirectStreaming ?? requestData.ConnectionSettings.DisableDirectStreaming;
			if (disableDirectStreaming || NeedsToEagerReadStream<TResponse>())
			{
				var inMemoryStream = requestData.MemoryStreamFactory.Create();
				stream.CopyTo(inMemoryStream, BufferSize);
				bytes = SwapStreams(ref stream, ref inMemoryStream);
				details.ResponseBodyInBytes = bytes;
			}

			var needsDispose = typeof(TResponse) != typeof(ElasticsearchResponse<Stream>);
			using (needsDispose ? stream : EmptyDisposable)
			{
				if (SetSpecialTypes<TResponse>(stream, bytes, out var r))
					return r;

				if (requestData.CustomConverter != null) return requestData.CustomConverter(details, stream) as TResponse;
				return requestData.ConnectionSettings.RequestResponseSerializer.Deserialize<TResponse>(stream);
			}
		}

		private static async Task<TResponse> SetBodyAsync<TResponse>(HttpDetails details, RequestData requestData, Stream stream, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse
		{
			byte[] bytes = null;
			var disableDirectStreaming = requestData.PostData?.DisableDirectStreaming ?? requestData.ConnectionSettings.DisableDirectStreaming;
			if (disableDirectStreaming || NeedsToEagerReadStream<TResponse>())
			{
				var inMemoryStream = requestData.MemoryStreamFactory.Create();
				await stream.CopyToAsync(inMemoryStream, BufferSize, cancellationToken).ConfigureAwait(false);
				bytes = SwapStreams(ref stream, ref inMemoryStream);
				details.ResponseBodyInBytes = bytes;
			}

			var needsDispose = typeof(TResponse) != typeof(ElasticsearchResponse<Stream>);
			using (needsDispose ? stream : EmptyDisposable)
			{
				if (SetSpecialTypes<TResponse>(stream, bytes, out var r)) return r;
				if (requestData.CustomConverter != null) return requestData.CustomConverter(details, stream) as TResponse;
				return await requestData.ConnectionSettings.RequestResponseSerializer.DeserializeAsync<TResponse>(stream, cancellationToken)
					.ConfigureAwait(false);
			}
		}

		private static readonly VoidResponse StaticVoid = new VoidResponse { Body = new VoidResponse.VoidBody() };
		private static readonly Type[] SpecialTypes = {typeof(StringResponse), typeof(BytesResponse), typeof(VoidResponse), typeof(StreamResponse)};

		private static bool SetSpecialTypes<TResponse>(Stream responseStream, byte[] bytes, out TResponse cs)
			where TResponse : class, IElasticsearchResponse
		{
			cs = null;
			var responseType = typeof(TResponse);
			if (!SpecialTypes.Contains(responseType)) return false;

			if (responseType == typeof(StringResponse))
				cs = new StringResponse(bytes.Utf8String()) as TResponse;
			else if (responseType == typeof(byte[]))
				cs = new BytesResponse(bytes) as TResponse;
			else if (responseType == typeof(VoidResponse))
				cs = StaticVoid as TResponse;
			else if (responseType == typeof(StreamResponse))
				cs = new StreamResponse(responseStream) as TResponse;

			return cs != null;
		}

		private static bool NeedsToEagerReadStream<TResponse>()
			where TResponse : class, IElasticsearchResponse =>
			typeof(TResponse) == typeof(StringResponse) || typeof(TResponse) == typeof(BytesResponse);

		private static byte[] SwapStreams(ref Stream responseStream, ref MemoryStream ms)
		{
			var bytes = ms.ToArray();
			responseStream.Dispose();
			responseStream = ms;
			responseStream.Position = 0;
			return bytes;
		}
	}
}
