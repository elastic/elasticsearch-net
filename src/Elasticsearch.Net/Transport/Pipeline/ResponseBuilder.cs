// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public static class ResponseBuilder
	{
		public const int BufferSize = 81920;

		private static readonly Type[] SpecialTypes =
		{
			typeof(StringResponse), typeof(BytesResponse), typeof(VoidResponse), typeof(DynamicResponse)
		};

		public static TResponse ToResponse<TResponse>(
			RequestData requestData,
			Exception ex,
			int? statusCode,
			IEnumerable<string> warnings,
			Stream responseStream,
			string mimeType = null
		) where TResponse : class, IElasticsearchResponse, new() =>
			ToResponse<TResponse>(requestData, ex, statusCode, warnings, responseStream, null, mimeType);

		public static TResponse ToResponse<TResponse>(
			RequestData requestData,
			Exception ex,
			int? statusCode,
			IEnumerable<string> warnings,
			Stream responseStream,
			string productName,
			string mimeType
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			responseStream.ThrowIfNull(nameof(responseStream));

			var details = Initialize(requestData, ex, statusCode, warnings, productName, mimeType);

			//TODO take ex and (responseStream == Stream.Null) into account might not need to flow to SetBody in that case

			TResponse response = null;

			// Only attempt to set the body if the response may have content
			if (MayHaveBody(statusCode, requestData.Method))
				response = SetBody<TResponse>(details, requestData, responseStream, mimeType);
			else
				responseStream.Dispose();

			response ??= new TResponse();

			response.ApiCall = details;
			return response;
		}

		public static Task<TResponse> ToResponseAsync<TResponse>(
			RequestData requestData,
			Exception ex,
			int? statusCode,
			IEnumerable<string> warnings,
			Stream responseStream,
			string mimeType = null,
			CancellationToken cancellationToken = default
		)
			where TResponse : class, IElasticsearchResponse, new() => ToResponseAsync<TResponse>(requestData, ex, statusCode, warnings,
			responseStream, null, mimeType, cancellationToken);

		public static async Task<TResponse> ToResponseAsync<TResponse>(
			RequestData requestData,
			Exception ex,
			int? statusCode,
			IEnumerable<string> warnings,
			Stream responseStream,
			string productName,
			string mimeType,
			CancellationToken cancellationToken = default
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			responseStream.ThrowIfNull(nameof(responseStream));

			//TODO take ex and (responseStream == Stream.Null) into account might not need to flow to SetBody in that case

			var details = Initialize(requestData, ex, statusCode, warnings, productName, mimeType);

			TResponse response = null;

			// Only attempt to set the body if the response may have content
			if (MayHaveBody(statusCode, requestData.Method))
				response = await SetBodyAsync<TResponse>(details, requestData, responseStream, mimeType, cancellationToken).ConfigureAwait(false);
			else
				responseStream.Dispose();

			response ??= new TResponse();

			response.ApiCall = details;
			return response;
		}

		/// <summary>
		/// A helper which returns true if the response could potentially have a body.
		/// </summary>
		private static bool MayHaveBody(int? statusCode, HttpMethod httpMethod) =>
			!statusCode.HasValue || statusCode.Value != 204 && httpMethod != HttpMethod.HEAD;

		private static ApiCallDetails Initialize(
			RequestData requestData,
			Exception exception,
			int? statusCode,
			IEnumerable<string> warnings,
			string productName,
			string mimeType
		)
		{
			var success = false;
			var allowedStatusCodes = requestData.AllowedStatusCodes;
			if (statusCode.HasValue)
			{
				if (allowedStatusCodes.Contains(-1) || allowedStatusCodes.Contains(statusCode.Value))
					success = true;
				else
					success = requestData.ConnectionSettings
						.StatusCodeToResponseSuccess(requestData.Method, statusCode.Value);
			}

			// We don't validate the content-type (MIME type) for HEAD requests or responses that have no content (204 status code).
			// Elastic Cloud responses to HEAD requests strip the content-type header so we want to avoid validation in that case.
			if (MayHaveBody(statusCode, requestData.Method) && !RequestData.ValidResponseContentType(requestData.Accept, mimeType))
				success = false;

			var details = new ApiCallDetails
			{
				Success = success,
				OriginalException = exception,
				HttpStatusCode = statusCode,
				RequestBodyInBytes = requestData.PostData?.WrittenBytes,
				Uri = requestData.Uri,
				HttpMethod = requestData.Method,
				DeprecationWarnings = warnings ?? Enumerable.Empty<string>(),
				ResponseMimeType = mimeType,
				ConnectionConfiguration = requestData.ConnectionSettings,
				ProductName = productName
			};
			return details;
		}

		private static TResponse SetBody<TResponse>(ApiCallDetails details, RequestData requestData, Stream responseStream, string mimeType)
			where TResponse : class, IElasticsearchResponse, new()
		{
			byte[] bytes = null;
			var disableDirectStreaming = requestData.PostData?.DisableDirectStreaming ?? requestData.ConnectionSettings.DisableDirectStreaming;
			if (disableDirectStreaming || NeedsToEagerReadStream<TResponse>())
			{
				var inMemoryStream = requestData.MemoryStreamFactory.Create();
				responseStream.CopyTo(inMemoryStream, BufferSize);
				bytes = SwapStreams(ref responseStream, ref inMemoryStream);
				details.ResponseBodyInBytes = bytes;
			}

			using (responseStream)
			{
				if (SetSpecialTypes<TResponse>(mimeType, bytes, requestData.MemoryStreamFactory, out var r))
					return r;

				if (details.HttpStatusCode.HasValue && requestData.SkipDeserializationForStatusCodes.Contains(details.HttpStatusCode.Value))
					return null;

				var serializer = requestData.ConnectionSettings.RequestResponseSerializer;
				if (requestData.CustomResponseBuilder != null)
					return requestData.CustomResponseBuilder.DeserializeResponse(serializer, details, responseStream) as TResponse;

				return !RequestData.ValidResponseContentType(requestData.Accept, mimeType)
					? null
					: serializer.Deserialize<TResponse>(responseStream);
			}
		}

		private static async Task<TResponse> SetBodyAsync<TResponse>(
			ApiCallDetails details, RequestData requestData, Stream responseStream, string mimeType, CancellationToken cancellationToken
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			byte[] bytes = null;
			var disableDirectStreaming = requestData.PostData?.DisableDirectStreaming ?? requestData.ConnectionSettings.DisableDirectStreaming;
			if (disableDirectStreaming || NeedsToEagerReadStream<TResponse>())
			{
				var inMemoryStream = requestData.MemoryStreamFactory.Create();
				await responseStream.CopyToAsync(inMemoryStream, BufferSize, cancellationToken).ConfigureAwait(false);
				bytes = SwapStreams(ref responseStream, ref inMemoryStream);
				details.ResponseBodyInBytes = bytes;
			}

			using (responseStream)
			{
				if (SetSpecialTypes<TResponse>(mimeType, bytes, requestData.MemoryStreamFactory, out var r)) return r;

				if (details.HttpStatusCode.HasValue && requestData.SkipDeserializationForStatusCodes.Contains(details.HttpStatusCode.Value))
					return null;

				var serializer = requestData.ConnectionSettings.RequestResponseSerializer;
				if (requestData.CustomResponseBuilder != null)
					return await requestData.CustomResponseBuilder.DeserializeResponseAsync(serializer, details, responseStream, cancellationToken)
						.ConfigureAwait(false) as TResponse;

				return !RequestData.ValidResponseContentType(requestData.Accept, mimeType)
					? null
					: await serializer
						.DeserializeAsync<TResponse>(responseStream, cancellationToken)
						.ConfigureAwait(false);
			}
		}

		private static bool SetSpecialTypes<TResponse>(string mimeType, byte[] bytes, IMemoryStreamFactory memoryStreamFactory, out TResponse cs)
			where TResponse : class, IElasticsearchResponse, new()
		{
			cs = null;
			var responseType = typeof(TResponse);
			if (!SpecialTypes.Contains(responseType)) return false;

			if (responseType == typeof(StringResponse))
				cs = new StringResponse(bytes.Utf8String()) as TResponse;
			else if (responseType == typeof(BytesResponse))
				cs = new BytesResponse(bytes) as TResponse;
			else if (responseType == typeof(VoidResponse))
				cs = new VoidResponse() as TResponse;
			else if (responseType == typeof(DynamicResponse))
			{
				//if not json store the result under "body"
				if (!RequestData.IsJsonMimeType(mimeType))
				{
					var dictionary = new DynamicDictionary { ["body"] = new(bytes.Utf8String()) };

					cs = new DynamicResponse(dictionary) as TResponse;
				}
				else
				{
					using var ms = memoryStreamFactory.Create(bytes);
					var body = LowLevelRequestResponseSerializer.Instance.Deserialize<DynamicDictionary>(ms);
					cs = new DynamicResponse(body) as TResponse;
				}
			}
			return cs != null;
		}

		private static bool NeedsToEagerReadStream<TResponse>()
			where TResponse : class, IElasticsearchResponse, new() =>
			typeof(TResponse) == typeof(StringResponse)
			|| typeof(TResponse) == typeof(BytesResponse)
			|| typeof(TResponse) == typeof(DynamicResponse);

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
