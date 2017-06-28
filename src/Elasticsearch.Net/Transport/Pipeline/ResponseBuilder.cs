using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	internal class ResponseBuilderStatics
	{
		internal const int BufferSize = 81920;
		internal static readonly VoidResponse Void = new VoidResponse();
		//TODO this empty disposable is used in more places move to its own static class
		internal static readonly IDisposable EmptyDisposable = new MemoryStream();
		internal static byte[] SwapStreams(ref Stream responseStream, ref MemoryStream ms)
		{
			var bytes = ms.ToArray();
			responseStream.Dispose();
			responseStream = ms;
			responseStream.Position = 0;
			return bytes;
		}
		internal static void SetStringResult(ElasticsearchResponse<string> result, byte[] bytes) =>
			result.Body = bytes.Utf8String();

		internal static void SetByteResult(ElasticsearchResponse<byte[]> result, byte[] bytes) =>
			result.Body = bytes;

		internal static void SetStreamResult(ElasticsearchResponse<Stream> result, Stream response) =>
			result.Body = response;

		internal static void SetVoidResult(ElasticsearchResponse<VoidResponse> result, Stream response)
		{
			response.Dispose();
			result.Body = ResponseBuilderStatics.Void;
		}
	}
	public class ResponseBuilder<TReturn> where TReturn : class
	{
		private readonly RequestData _requestData;
		private readonly CancellationToken _cancellationToken;
		private readonly bool _disableDirectStreaming;

		public Exception Exception { get; set; }
		public int? StatusCode { get; set; }
		public Stream Stream { get; set; }

		public IEnumerable<string> DeprecationWarnings { get; set; }

		public ResponseBuilder(RequestData requestData, CancellationToken cancellationToken = default(CancellationToken))
		{
			_requestData = requestData;
			_cancellationToken = cancellationToken;
			_disableDirectStreaming =
				this._requestData.PostData?.DisableDirectStreaming ?? this._requestData.ConnectionSettings.DisableDirectStreaming;
		}

		private IDisposable Trace(string section) => this._requestData.Trace(section);

		public ElasticsearchResponse<TReturn> ToResponse()
		{
			var response = Initialize(this.StatusCode, this.Exception);
			if (this.Stream != null)
				SetBody(response, this.Stream);
			Finalize(response);
			return response;
		}

		public async Task<ElasticsearchResponse<TReturn>> ToResponseAsync()
		{
			var response = Initialize(this.StatusCode, this.Exception);
			if (this.Stream != null)
				await SetBodyAsync(response, this.Stream).ConfigureAwait(false);
			Finalize(response);
			return response;
		}

		private ElasticsearchResponse<TReturn> Initialize(int? statusCode, Exception exception)
		{
			var response = statusCode.HasValue
				? new ElasticsearchResponse<TReturn>(statusCode.Value, this._requestData.AllowedStatusCodes)
				: new ElasticsearchResponse<TReturn>(exception);
			response.RequestBodyInBytes = this._requestData.PostData?.WrittenBytes;
			response.Uri = this._requestData.Uri;
			response.HttpMethod = this._requestData.Method;
			response.OriginalException = exception;
			response.DeprecationWarnings = this.DeprecationWarnings ?? Enumerable.Empty<string>();
			return response;
		}

		private void SetBody(ElasticsearchResponse<TReturn> response, Stream stream)
		{
			byte[] bytes = null;
			if (NeedsToEagerReadStream(response))
				using (Trace($"{nameof(ResponseBuilder<TReturn>)}.{nameof(NeedsToEagerReadStream)}"))
				{
					var inMemoryStream = this._requestData.MemoryStreamFactory.Create();
					stream.CopyTo(inMemoryStream, ResponseBuilderStatics.BufferSize);
					bytes = ResponseBuilderStatics.SwapStreams(ref stream, ref inMemoryStream);
				}

			var needsDispose = typeof(TReturn) != typeof(Stream);
			using (needsDispose ? stream : ResponseBuilderStatics.EmptyDisposable)
			{
				if (response.Success || response.AllowAllStatusCodes)
				{
					if (!SetSpecialTypes(stream, response, bytes))
					{
						if (this._requestData.CustomConverter != null)
							using (Trace(nameof(RequestData.CustomConverter)))
								response.Body = this._requestData.CustomConverter(response, stream) as TReturn;

						else using (Trace(nameof(IElasticsearchSerializer.Deserialize)))
							response.Body = this._requestData.ConnectionSettings.Serializer.Deserialize<TReturn>(stream);
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
				using (Trace($"{nameof(ResponseBuilder<TReturn>)}.{nameof(NeedsToEagerReadStream)} Async"))
				{
					var inMemoryStream = this._requestData.MemoryStreamFactory.Create();
					await stream.CopyToAsync(inMemoryStream, ResponseBuilderStatics.BufferSize, this._cancellationToken).ConfigureAwait(false);
					bytes = ResponseBuilderStatics.SwapStreams(ref stream, ref inMemoryStream);
				}

			var needsDispose = typeof(TReturn) != typeof(Stream);
			using (needsDispose ? stream : ResponseBuilderStatics.EmptyDisposable)
			{
				if (response.Success || response.AllowAllStatusCodes)
				{
					if (!SetSpecialTypes(stream, response, bytes))
					{
						//TODO CustomConverter smells on its own but it also has no async variant
						if (this._requestData.CustomConverter != null)
							using(Trace(nameof(RequestData.CustomConverter)))
								response.Body = this._requestData.CustomConverter(response, stream) as TReturn;

						else using(Trace(nameof(IElasticsearchSerializer.DeserializeAsync)))
							response.Body = await this._requestData.ConnectionSettings.Serializer.DeserializeAsync<TReturn>(stream, this._cancellationToken).ConfigureAwait(false);
					}
					if (response.AllowAllStatusCodes)
						await ReadServerErrorAsync(response, new MemoryStream(bytes), bytes);
				}
				else if (response.HttpStatusCode != null)
					await ReadServerErrorAsync(response, stream, bytes);
			}
		}

		private void ReadServerError(ElasticsearchResponse<TReturn> response, Stream stream, byte[] bytes)
		{
			using (Trace($"{nameof(ResponseBuilder<TReturn>)}.{nameof(ReadServerError)}"))
			{
				if (ServerError.TryCreate(stream, out ServerError serverError))
					response.ServerError = serverError;
				if (_disableDirectStreaming)
					response.ResponseBodyInBytes = bytes;
			}
		}

		private async Task ReadServerErrorAsync(ElasticsearchResponse<TReturn> response, Stream stream, byte[] bytes)
		{

			using(Trace($"{nameof(ResponseBuilder<TReturn>)}.{nameof(ReadServerErrorAsync)}"))
			{
				response.ServerError = await ServerError.TryCreateAsync(stream, this._cancellationToken).ConfigureAwait(false);
				if (_disableDirectStreaming)
					response.ResponseBodyInBytes = bytes;
			}
		}

		private void Finalize(ElasticsearchResponse<TReturn> response)
		{
			var passAlongConnectionStatus = response.Body as IBodyWithApiCallDetails;
			if (passAlongConnectionStatus != null)
				passAlongConnectionStatus.ApiCall = response;
		}

		private bool NeedsToEagerReadStream(ElasticsearchResponse<TReturn> response) =>
			response.AllowAllStatusCodes //need to double read for error and TReturn
			|| _disableDirectStreaming || typeof(TReturn) == typeof(string) || typeof(TReturn) == typeof(byte[]);

		private bool SetSpecialTypes(Stream responseStream, ElasticsearchResponse<TReturn> cs, byte[] bytes)
		{
			if (_disableDirectStreaming) cs.ResponseBodyInBytes = bytes;

			var t = typeof(TReturn);
			var s = t == typeof(string) || t == typeof(byte[]) || t == typeof(VoidResponse) || t == typeof(Stream);
			if (!s) return false;

			using (Trace(nameof(SetSpecialTypes)))
			{
				if (t == typeof(string))
					ResponseBuilderStatics.SetStringResult(cs as ElasticsearchResponse<string>, bytes);
				else if (t == typeof(byte[]))
					ResponseBuilderStatics.SetByteResult(cs as ElasticsearchResponse<byte[]>, bytes);
				else if (t == typeof(VoidResponse))
					ResponseBuilderStatics.SetVoidResult(cs as ElasticsearchResponse<VoidResponse>, responseStream);
				else if (t == typeof(Stream))
					ResponseBuilderStatics.SetStreamResult(cs as ElasticsearchResponse<Stream>, responseStream);
				return true;
			}
		}

	}
}
