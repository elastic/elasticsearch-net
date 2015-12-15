using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Purify;

namespace Elasticsearch.Net
{
	public class RequestData
	{
		public const string MimeType = "application/json";
		public const int BufferSize = 8096;

		public Uri Uri => new Uri(this.Node.Uri, this.Path).Purify();

		public HttpMethod Method { get; private set; }
		public string Path { get; }
		public PostData<object> Data { get; }
		public Node Node { get; internal set; }
		public TimeSpan RequestTimeout { get; }
		public TimeSpan PingTimeout { get; }
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
		public BasicAuthenticationCredentials BasicAuthorizationCredentials { get; }
		public CancellationToken CancellationToken { get; }
		public IEnumerable<int> AllowedStatusCodes { get; }
		public Func<IApiCallDetails, Stream, object> CustomConverter { get; private set; }

		private readonly IConnectionConfigurationValues _settings;
		private readonly IMemoryStreamFactory _memoryStreamFactory;
		private readonly UrlFormatProvider _formatter;

		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IMemoryStreamFactory memoryStreamFactory)
			: this(method, path, data, global, (IRequestConfiguration)null, memoryStreamFactory)
		{ }

		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestParameters local, IMemoryStreamFactory memoryStreamFactory)
			: this(method, path, data, global, (IRequestConfiguration)local?.RequestConfiguration, memoryStreamFactory)
		{
			this.CustomConverter = local?.DeserializationOverride;
			this.Path = this.CreatePathWithQueryStrings(path, this._settings, local);
		}

		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestConfiguration local, IMemoryStreamFactory memoryStreamFactory)
		{
			this._settings = global;
			this._memoryStreamFactory = memoryStreamFactory;
			this._formatter = new UrlFormatProvider(this._settings);
			this.Method = method;
			this.Data = data;
			this.Path = this.CreatePathWithQueryStrings(path, this._settings, null);

			this.Pipelined = global.HttpPipeliningEnabled || (local?.EnableHttpPipelining).GetValueOrDefault(false);
			this.HttpCompression = global.EnableHttpCompression;
			this.ContentType = local?.ContentType ?? MimeType;
			this.Headers = global.Headers;

			this.RequestTimeout = local?.RequestTimeout ?? global.RequestTimeout;
			this.PingTimeout = 
				local?.PingTimeout
				?? global?.PingTimeout
				?? (global.ConnectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

			this.KeepAliveInterval = (int)(global.KeepAliveInterval?.TotalMilliseconds ?? 2000);
			this.KeepAliveTime = (int)(global.KeepAliveTime?.TotalMilliseconds ?? 2000);

			this.ProxyAddress = global.ProxyAddress;
			this.ProxyUsername = global.ProxyUsername;
			this.ProxyPassword = global.ProxyPassword;
			this.DisableAutomaticProxyDetection = global.DisableAutomaticProxyDetection;
			this.BasicAuthorizationCredentials = local?.BasicAuthenticationCredentials ?? global.BasicAuthenticationCredentials;
			this.CancellationToken = local?.CancellationToken ?? CancellationToken.None;
			this.AllowedStatusCodes = local?.AllowedStatusCodes ?? Enumerable.Empty<int>();
		}

		public void Write(Stream writableStream)
		{
			this.Data.Write(writableStream, this._settings);
		}

		public Task WriteAsync(Stream writableStream)
		{
			return this.Data.WriteAsync(writableStream, this._settings, this.CancellationToken);
		}

		public ElasticsearchResponse<TReturn> CreateResponse<TReturn>(int statusCode, Stream responseStream, Exception exception = null)
			where TReturn : class
		{
			var response = InitializeResponse<TReturn>(statusCode, exception);

			if (response.Success)
			{
				byte[] bytes = null;
				if (NeedsToEagerReadStream<TReturn>())
				{
					var inMemoryStream = this._memoryStreamFactory.Create();
					responseStream.CopyTo(inMemoryStream, BufferSize);
					bytes = this.SwapStreams(ref responseStream, ref inMemoryStream);
				}

				if (!SetSpecialTypes(responseStream, response, bytes))
				{
					if (this.CustomConverter != null) response.Body = this.CustomConverter(response, responseStream) as TReturn;
					else response.Body = this._settings.Serializer.Deserialize<TReturn>(responseStream);
				}
			}
			else if (response.SuccessOrKnownError)
			{
				response.ServerError = new ElasticsearchDefaultSerializer().Deserialize<ServerError>(responseStream);
			}

			return FinalizeReponse(response);
		}

		public async Task<ElasticsearchResponse<TReturn>> CreateResponseAsync<TReturn>(int statusCode, Stream responseStream, Exception exception = null)
			where TReturn : class
		{
			var response = InitializeResponse<TReturn>(statusCode, exception);

			if (response.Success)
			{
				byte[] bytes = null;
				if (NeedsToEagerReadStream<TReturn>())
				{
					var inMemoryStream = this._memoryStreamFactory.Create();
					await responseStream.CopyToAsync(inMemoryStream, BufferSize, this.CancellationToken);
					bytes = this.SwapStreams(ref responseStream, ref inMemoryStream);
				}

				if (!SetSpecialTypes(responseStream, response, bytes))
				{
					if (this.CustomConverter != null) response.Body = this.CustomConverter(response, responseStream) as TReturn;
					else response.Body = await this._settings.Serializer.DeserializeAsync<TReturn>(responseStream, this.CancellationToken);
				}
			}
			else if (response.SuccessOrKnownError)
			{
				response.ServerError = await new ElasticsearchDefaultSerializer().DeserializeAsync<ServerError>(responseStream, this.CancellationToken);
			}

			return FinalizeReponse(response);
		}

		public ElasticsearchResponse<TReturn> CreateResponse<TReturn>(Exception exception)
		{
			var cs = new ElasticsearchResponse<TReturn>(exception);
			cs.RequestBodyInBytes = this.Data?.WrittenBytes;
			cs.Uri = this.Uri;
			cs.HttpMethod = this.Method;
			cs.OriginalException = exception;
			return cs;
		}

		private ElasticsearchResponse<TReturn> InitializeResponse<TReturn>(int statusCode, Exception exception)
		{
			var cs = new ElasticsearchResponse<TReturn>(statusCode, this.AllowedStatusCodes);
			cs.RequestBodyInBytes = this.Data?.WrittenBytes;
			cs.Uri = this.Uri;
			cs.HttpMethod = this.Method;
			cs.OriginalException = exception;
			return cs;
		}

		private static ElasticsearchResponse<TReturn> FinalizeReponse<TReturn>(ElasticsearchResponse<TReturn> response)
		{
			var passAlongConnectionStatus = response.Body as IBodyWithApiCallDetails;
			if (passAlongConnectionStatus != null)
			{
				passAlongConnectionStatus.CallDetails = response;
			}
			return response;
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
				cs.ResponseBodyInBytes = bytes;

			if (cs.Body is string)
				this.SetStringResult(cs as ElasticsearchResponse<string>, bytes);
			else if (cs.Body is byte[])
				this.SetByteResult(cs as ElasticsearchResponse<byte[]>, bytes);
			else if (cs.Body is VoidResponse)
				this.SetVoidResult(cs as ElasticsearchResponse<VoidResponse>, responseStream);
			else if (cs.Body is Stream)
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
			response.Close();
			result.Body = _void;
		}


		private string CreatePathWithQueryStrings(string path, IConnectionConfigurationValues global, IRequestParameters request = null)
		{
			//Make sure we append global query string as well the request specific query string parameters
			var copy = new NameValueCollection(global.QueryStringParameters);
			if (request != null)
				copy.Add(request.QueryString.ToNameValueCollection(this._formatter));
			if (!copy.HasKeys()) return path;

			var queryString = copy.ToQueryString();
			path += queryString;
			return path;
		}
	}
}