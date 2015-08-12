using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using System.Collections.Specialized;
using Elasticsearch.Net.Connection.Security;
using System.Threading;
using System.IO.Compression;

namespace Elasticsearch.Net.Connection
{
	public class RequestData
	{
		public const string MimeType = "application/json";
		public const int BufferSize = 8096;

		public Uri Uri { get; internal set; }
		public HttpMethod Method { get; internal set; }
		public string Path { get; }
		public PostData<object> Data { get; }

		public TimeSpan RequestTimeout { get; }
		public int KeepAliveTime { get; }
		public int KeepAliveInterval { get; }


		public bool Pipelined { get; }
		public bool HttpCompression { get; }
		public string ContentType { get; }

		public NameValueCollection Headers { get; }
		public string ProxyAddress { get; }
		public string ProxyUsername { get; }
		public string ProxyPassword { get; }
		public bool DisableAutomaticProxyDetection { get; }
		public BasicAuthorizationCredentials BasicAuthorizationCredentials { get; }
		public CancellationToken CancellationToken { get; }

		private readonly IConnectionConfigurationValues _settings;
		private readonly IMemoryStreamFactory _memoryStreamFactory;

		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IMemoryStreamFactory memoryStreamFactory)
			: this(method, path, data, global, null, memoryStreamFactory)
		{ }

		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestConfiguration local, IMemoryStreamFactory memoryStreamFactory)
		{
			this._settings = global;
			this._memoryStreamFactory = memoryStreamFactory;
			this.Method = method;
			this.Data = data;
			this.Path = path;

			//TODO default to true in 2.0?
			this.Pipelined = global.HttpPipeliningEnabled || (local?.EnableHttpPipelining).GetValueOrDefault(false);
			this.HttpCompression = global.EnableHttpCompression;
			this.ContentType = local?.ContentType ?? MimeType;
			this.Headers = global.Headers;

			this.RequestTimeout = local?.RequestTimeout ?? global.Timeout;
			this.KeepAliveInterval = (int)(global.KeepAliveInterval?.TotalMilliseconds ?? 2000);
			this.KeepAliveTime = (int)(global.KeepAliveTime?.TotalMilliseconds ?? 2000);

			this.ProxyAddress = global.ProxyAddress;
			this.ProxyUsername = global.ProxyUsername;
			this.ProxyPassword = global.ProxyPassword;
			this.DisableAutomaticProxyDetection = global.DisableAutomaticProxyDetection;
			this.BasicAuthorizationCredentials = local?.BasicAuthenticationCredentials ?? global.BasicAuthenticationCredentials;
			this.CancellationToken = local?.CancellationToken ?? CancellationToken.None;
		}

		public void Write(Stream writableStream)
		{
			this.Data.Write(writableStream, this._settings);
		}
		public Task WriteAsync(Stream writableStream)
		{
			return this.Data.WriteAsync(writableStream, this._settings, this.CancellationToken);
		}

		public ElasticsearchResponse<TReturn> CreateResponse<TReturn>(int statusCode, Stream responseStream, Exception innerException = null)
		{
			var cs = InitializeResponse<TReturn>(statusCode, innerException);
			byte[] bytes = null;
			if (NeedsToEagerReadStream<TReturn>())
			{
				var inMemoryStream = this._memoryStreamFactory.Create();
				responseStream.CopyTo(inMemoryStream, BufferSize);
				bytes = this.SwapStreams(ref responseStream, ref inMemoryStream);
			}

			if (!SetSpecialTypes(responseStream, cs, bytes))
				cs.Response = this._settings.Serializer.Deserialize<TReturn>(responseStream);

			return FinalizeReponse(cs);
		}

		public async Task<ElasticsearchResponse<TReturn>> CreateResponseAsync<TReturn>(int statusCode, Stream responseStream, Exception innerException = null)
		{
			var cs = InitializeResponse<TReturn>(statusCode, innerException);
			byte[] bytes = null;
			if (NeedsToEagerReadStream<TReturn>())
			{
				var inMemoryStream = this._memoryStreamFactory.Create();
				await responseStream.CopyToAsync(inMemoryStream, BufferSize, this.CancellationToken);
				bytes = this.SwapStreams(ref responseStream, ref inMemoryStream);
			}

			if (!SetSpecialTypes(responseStream, cs, bytes))
				cs.Response = await this._settings.Serializer.DeserializeAsync<TReturn>(responseStream, this.CancellationToken);

			return FinalizeReponse(cs);
		}

		private static ElasticsearchResponse<TReturn> FinalizeReponse<TReturn>(ElasticsearchResponse<TReturn> cs)
		{
			var passAlongConnectionStatus = cs.Response as IResponseWithRequestInformation;
			if (passAlongConnectionStatus != null) passAlongConnectionStatus.RequestInformation = cs;
			return cs;
		}

		public ElasticsearchResponse<TReturn> CreateResponse<TReturn>(Exception e)
		{
			var cs = new ElasticsearchResponse<TReturn>(this._settings, e);
			cs.Request = this.Data?.Bytes;
			cs.RequestUri = this.Uri;
			cs.RequestMethod = this.Method;
			cs.OriginalException = e;
			return cs;
		}

		private ElasticsearchResponse<TReturn> InitializeResponse<TReturn>(int statusCode, Exception innerException)
		{
			var cs = new ElasticsearchResponse<TReturn>(this._settings, statusCode);
			cs.Request = this.Data?.Bytes;
			cs.RequestUri = this.Uri;
			cs.RequestMethod = this.Method;
			cs.OriginalException = innerException;

			return cs;
		}

		private bool NeedsToEagerReadStream<TReturn>() =>
			this._settings.DisableDirectStreaming || typeof(TReturn) == typeof(string) || typeof(TReturn) == typeof(byte[]);

		private byte[] SwapStreams(ref Stream responseStream, ref MemoryStream ms)
		{
			var bytes = ms.ToArray();
			responseStream.Close();
			responseStream = ms;
			responseStream.Position = 0;
			return bytes;

		}

		private bool SetSpecialTypes<TReturn>(Stream responseStream, ElasticsearchResponse<TReturn> cs, byte[] bytes)
		{
			var setSpecial = true;
			if (this._settings.DisableDirectStreaming)
				cs.ResponseRaw = bytes;

			if (cs.Response is string)
				this.SetStringResult(cs as ElasticsearchResponse<string>, bytes);
			else if (cs.Response is byte[])
				this.SetByteResult(cs as ElasticsearchResponse<byte[]>, bytes);
			else if (cs.Response is VoidResponse)
				this.SetVoidResult(cs as ElasticsearchResponse<VoidResponse>, responseStream);
			else if (cs.Response is Stream)
				this.SetStreamResult(cs as ElasticsearchResponse<Stream>, responseStream);
			else
				setSpecial = false;
			return setSpecial;
		}

		private void SetStringResult(ElasticsearchResponse<string> result, byte[] bytes) => result.Response = bytes.Utf8String();

		private void SetByteResult(ElasticsearchResponse<byte[]> result, byte[] bytes) => result.Response = bytes;

		private void SetStreamResult(ElasticsearchResponse<Stream> result, Stream response) => result.Response = response;

		private static VoidResponse _void = new VoidResponse();

		private void SetVoidResult(ElasticsearchResponse<VoidResponse> result, Stream response)
		{
			response.Close();
			result.Response = _void;
		}


	}
}