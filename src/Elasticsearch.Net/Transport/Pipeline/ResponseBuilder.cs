using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ResponseBuilder<TReturn>
		where TReturn : class

	{
		private const int BufferSize = 81920;
		private static readonly IDisposable EmptyDisposable = new MemoryStream();
		private static readonly VoidResponse Void = new VoidResponse();
		private readonly CancellationToken _cancellationToken;
		private readonly bool _disableDirectStreaming;

		private readonly RequestData _requestData;

		public ResponseBuilder(RequestData requestData, CancellationToken cancellationToken = default(CancellationToken))
		{
			_requestData = requestData;
			_cancellationToken = cancellationToken;
			_disableDirectStreaming =
				_requestData.PostData?.DisableDirectStreaming ?? _requestData.ConnectionSettings.DisableDirectStreaming;
		}

		public IEnumerable<string> DeprecationWarnings { get; set; }

		public Exception Exception { get; set; }
		public string ResponseMimeType { get; set; } = RequestData.MimeType;
		public int? StatusCode { get; set; }
		public Stream Stream { get; set; }

		public ElasticsearchResponse<TReturn> ToResponse()
		{
			var response = Initialize(StatusCode, Exception);
			if (Stream != null)
				SetBody(response, Stream);
			Finalize(response);
			return response;
		}

		public async Task<ElasticsearchResponse<TReturn>> ToResponseAsync()
		{
			var response = Initialize(StatusCode, Exception);
			if (Stream != null)
				await SetBodyAsync(response, Stream).ConfigureAwait(false);
			Finalize(response);
			return response;
		}

		private ElasticsearchResponse<TReturn> Initialize(int? statusCode, Exception exception)
		{
			var response = statusCode.HasValue
				? new ElasticsearchResponse<TReturn>(statusCode.Value, _requestData.AllowedStatusCodes)
				: new ElasticsearchResponse<TReturn>(exception);
			response.RequestBodyInBytes = _requestData.PostData?.WrittenBytes;
			response.Uri = _requestData.Uri;
			response.HttpMethod = _requestData.Method;
			response.OriginalException = exception;
			response.DeprecationWarnings = DeprecationWarnings ?? Enumerable.Empty<string>();
			response.ResponseMimeType = ResponseMimeType;
			return response;
		}

		private void SetBody(ElasticsearchResponse<TReturn> response, Stream stream)
		{
			byte[] bytes = null;
			if (NeedsToEagerReadStream(response))
			{
				var inMemoryStream = _requestData.MemoryStreamFactory.Create();
				stream.CopyTo(inMemoryStream, BufferSize);
				bytes = SwapStreams(ref stream, ref inMemoryStream);
			}

			var needsDispose = typeof(TReturn) != typeof(Stream);
			using (needsDispose ? stream : EmptyDisposable)
			{
				if (response.Success || response.AllowAllStatusCodes)
				{
					if (!SetSpecialTypes(stream, response, bytes))
					{
						if (_requestData.CustomConverter != null) response.Body = _requestData.CustomConverter(response, stream) as TReturn;
						else response.Body = _requestData.ConnectionSettings.Serializer.Deserialize<TReturn>(stream);
					}
					if (response.AllowAllStatusCodes)
						ReadServerError(response, new MemoryStream(bytes), bytes);
				}
				else if (response.HttpStatusCode != null)
					ReadServerError(response, stream, bytes);
			}
		}

		private async Task SetBodyAsync(ElasticsearchResponse<TReturn> response, Stream stream)
		{
			byte[] bytes = null;
			if (NeedsToEagerReadStream(response))
			{
				var inMemoryStream = _requestData.MemoryStreamFactory.Create();
				await stream.CopyToAsync(inMemoryStream, BufferSize, _cancellationToken).ConfigureAwait(false);
				bytes = SwapStreams(ref stream, ref inMemoryStream);
			}

			var needsDispose = typeof(TReturn) != typeof(Stream);
			using (needsDispose ? stream : EmptyDisposable)
			{
				if (response.Success || response.AllowAllStatusCodes)
				{
					if (!SetSpecialTypes(stream, response, bytes))
					{
						if (_requestData.CustomConverter != null) response.Body = _requestData.CustomConverter(response, stream) as TReturn;
						else
							response.Body = await _requestData.ConnectionSettings.Serializer.DeserializeAsync<TReturn>(stream, _cancellationToken)
								.ConfigureAwait(false);
					}
					if (response.AllowAllStatusCodes)
						await ReadServerErrorAsync(response, new MemoryStream(bytes), bytes).ConfigureAwait(false);
				}
				else if (response.HttpStatusCode != null)
					await ReadServerErrorAsync(response, stream, bytes).ConfigureAwait(false);
			}
		}

		private void ReadServerError(ElasticsearchResponse<TReturn> response, Stream stream, byte[] bytes)
		{
			ServerError serverError;
			if (ServerError.TryCreate(stream, out serverError))
				response.ServerError = serverError;
			if (_disableDirectStreaming)
				response.ResponseBodyInBytes = bytes;
		}

		private async Task ReadServerErrorAsync(ElasticsearchResponse<TReturn> response, Stream stream, byte[] bytes)
		{
			response.ServerError = await ServerError.TryCreateAsync(stream, _cancellationToken).ConfigureAwait(false);
			if (_disableDirectStreaming)
				response.ResponseBodyInBytes = bytes;
		}

		private void Finalize(ElasticsearchResponse<TReturn> response)
		{
			var passAlongConnectionStatus = response.Body as IBodyWithApiCallDetails;
			if (passAlongConnectionStatus != null) passAlongConnectionStatus.ApiCall = response;
		}

		private bool NeedsToEagerReadStream(ElasticsearchResponse<TReturn> response) =>
			response.AllowAllStatusCodes //need to double read for error and TReturn
			|| _disableDirectStreaming || typeof(TReturn) == typeof(string) || typeof(TReturn) == typeof(byte[]);

		private byte[] SwapStreams(ref Stream responseStream, ref MemoryStream ms)
		{
			var bytes = ms.ToArray();
			responseStream.Dispose();
			responseStream = ms;
			responseStream.Position = 0;
			return bytes;
		}

		private bool SetSpecialTypes(Stream responseStream, ElasticsearchResponse<TReturn> cs, byte[] bytes)
		{
			var setSpecial = true;
			if (_disableDirectStreaming)
				cs.ResponseBodyInBytes = bytes;
			var returnType = typeof(TReturn);
			if (returnType == typeof(string))
				SetStringResult(cs as ElasticsearchResponse<string>, bytes);
			else if (returnType == typeof(byte[]))
				SetByteResult(cs as ElasticsearchResponse<byte[]>, bytes);
			else if (returnType == typeof(VoidResponse))
				SetVoidResult(cs as ElasticsearchResponse<VoidResponse>, responseStream);
			else if (returnType == typeof(Stream))
				SetStreamResult(cs as ElasticsearchResponse<Stream>, responseStream);
			else
				setSpecial = false;
			return setSpecial;
		}

		private void SetStringResult(ElasticsearchResponse<string> result, byte[] bytes) => result.Body = bytes.Utf8String();

		private void SetByteResult(ElasticsearchResponse<byte[]> result, byte[] bytes) => result.Body = bytes;

		private void SetStreamResult(ElasticsearchResponse<Stream> result, Stream response) => result.Body = response;

		private void SetVoidResult(ElasticsearchResponse<VoidResponse> result, Stream response)
		{
			response.Dispose();
			result.Body = Void;
		}
	}
}
