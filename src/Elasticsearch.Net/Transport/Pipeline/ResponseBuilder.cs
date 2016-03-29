using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ResponseBuilder<TReturn>
		where TReturn : class
	{
		public const int BufferSize = 8096;

		public Exception Exception { get; set; }
		public int? StatusCode { get; set; }
		public Stream Stream { get; set; }

		private RequestData _requestData;

		public ResponseBuilder(RequestData requestData)
		{
			_requestData = requestData;
		}

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
			return response;
		}

		private void SetBody(ElasticsearchResponse<TReturn> response, Stream stream)
		{
			byte[] bytes = null;
			if (NeedsToEagerReadStream())
			{
				var inMemoryStream = this._requestData.MemoryStreamFactory.Create();
				stream.CopyTo(inMemoryStream, BufferSize);
				bytes = this.SwapStreams(ref stream, ref inMemoryStream);
			}

			if (response.Success)
			{
				if (!SetSpecialTypes(stream, response, bytes))
				{
					if (this._requestData.CustomConverter != null) response.Body = this._requestData.CustomConverter(response, stream) as TReturn;
					else response.Body = this._requestData.ConnectionSettings.Serializer.Deserialize<TReturn>(stream);
				}
			}
			else if (response.HttpStatusCode != null)
			{
				ServerError serverError;
				if (ServerError.TryCreate(stream, out serverError))
					response.ServerError = serverError;
				if (this._requestData.ConnectionSettings.DisableDirectStreaming)
					response.ResponseBodyInBytes = bytes;
			}
		}

		private async Task SetBodyAsync(ElasticsearchResponse<TReturn> response, Stream stream)
		{
			byte[] bytes = null;
			if (NeedsToEagerReadStream())
			{
				var inMemoryStream = this._requestData.MemoryStreamFactory.Create();
				await stream.CopyToAsync(inMemoryStream, BufferSize, this._requestData.CancellationToken).ConfigureAwait(false);
				bytes = this.SwapStreams(ref stream, ref inMemoryStream);
			}

			if (response.Success)
			{
				if (!SetSpecialTypes(stream, response, bytes))
				{
					if (this._requestData.CustomConverter != null) response.Body = this._requestData.CustomConverter(response, stream) as TReturn;
					else response.Body = await this._requestData.ConnectionSettings.Serializer.DeserializeAsync<TReturn>(stream, this._requestData.CancellationToken).ConfigureAwait(false);
				}
			}
			else if (response.HttpStatusCode != null)
			{
				response.ServerError = await ServerError.TryCreateAsync(stream, this._requestData.CancellationToken).ConfigureAwait(false);
				if (this._requestData.ConnectionSettings.DisableDirectStreaming)
					response.ResponseBodyInBytes = bytes;
			}
		}

		private void Finalize(ElasticsearchResponse<TReturn> response)
		{
			var passAlongConnectionStatus = response.Body as IBodyWithApiCallDetails;
			if (passAlongConnectionStatus != null)
			{
				passAlongConnectionStatus.CallDetails = response;
			}
		}

		private bool NeedsToEagerReadStream() =>
			this._requestData.ConnectionSettings.DisableDirectStreaming || typeof(TReturn) == typeof(string) || typeof(TReturn) == typeof(byte[]);

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
			if (this._requestData.ConnectionSettings.DisableDirectStreaming)
				cs.ResponseBodyInBytes = bytes;
		    var returnType = typeof (TReturn);
			if (returnType == typeof(string))
				this.SetStringResult(cs as ElasticsearchResponse<string>, bytes);
			else if (returnType == typeof(byte[]))
				this.SetByteResult(cs as ElasticsearchResponse<byte[]>, bytes);
			else if (returnType == typeof(VoidResponse))
				this.SetVoidResult(cs as ElasticsearchResponse<VoidResponse>, responseStream);
            else if (returnType == typeof(Stream))
                this.SetStreamResult(cs as ElasticsearchResponse<Stream>, responseStream);
			else
				setSpecial = false;
			return setSpecial;
		}

		private void SetStringResult(ElasticsearchResponse<string> result, byte[] bytes) => result.Body = bytes.Utf8String();

		private void SetByteResult(ElasticsearchResponse<byte[]> result, byte[] bytes) => result.Body = bytes;

		private void SetStreamResult(ElasticsearchResponse<Stream> result, Stream response) => result.Body = response;

		private static VoidResponse _void = new VoidResponse();

		private void SetVoidResult(ElasticsearchResponse<VoidResponse> result, Stream response)
		{
			response.Dispose();
			result.Body = _void;
		}
	}
}
