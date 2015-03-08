using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elasticsearch.Net.Connection.RequestState;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Serialization;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection.RequestHandlers
{
	internal class RequestHandlerBase
	{
		protected const int BufferSize = 4096;
		protected static readonly string MaxRetryExceptionMessage = "Failed after retrying {2} times: '{0} {1}'. {3}";
		protected static readonly string TookTooLongExceptionMessage = "Retry timeout {4} was hit after retrying {2} times: '{0} {1}'. {3}";
		protected static readonly string MaxRetryInnerMessage = "InnerException: {0}, InnerMessage: {1}, InnerStackTrace: {2}";

		protected readonly IConnectionConfigurationValues _settings;
		protected readonly IConnection _connection;
		protected readonly IConnectionPool _connectionPool;
		protected readonly IElasticsearchSerializer _serializer;
		protected readonly IMemoryStreamProvider _memoryStreamProvider;
		protected readonly ITransportDelegator _delegator;

		protected readonly bool _throwMaxRetry;

		protected RequestHandlerBase(
			IConnectionConfigurationValues settings,
			IConnection connection,
			IConnectionPool connectionPool,
			IElasticsearchSerializer serializer,
			IMemoryStreamProvider memoryStreamProvider,
			ITransportDelegator delegator)
		{
			_settings = settings;
			_connection = connection;
			_connectionPool = connectionPool;
			_serializer = serializer;
			_memoryStreamProvider = memoryStreamProvider;
			_delegator = delegator;

			this._throwMaxRetry = !(this._connectionPool is SingleNodeConnectionPool);
		}

		protected byte[] PostData(object data)
		{
			if (data == null) return null;

			var bytes = data as byte[];
			if (bytes != null) return bytes;

			var s = data as string;
			if (s != null) return s.Utf8Bytes();

			var ss = data as IEnumerable<string>;
			if (ss != null) return (string.Join("\n", ss) + "\n").Utf8Bytes();

			var so = data as IEnumerable<object>;
			if (so == null) return this._serializer.Serialize(data);
			var joined = string.Join("\n", so
				.Select(soo => this._serializer.Serialize(soo, SerializationFormatting.None).Utf8String())) + "\n";
			return joined.Utf8Bytes();
		}

		protected static bool IsValidResponse(ITransportRequestState requestState, IElasticsearchResponse streamResponse)
		{
			return streamResponse.Success 
				|| StatusCodeAllowed(requestState.RequestConfiguration, streamResponse.HttpStatusCode);
		}

		protected static bool StatusCodeAllowed(IRequestConfiguration requestConfiguration, int? statusCode)
		{
			if (requestConfiguration == null)
				return false;

			return requestConfiguration.AllowedStatusCodes.HasAny(i => i == statusCode);
		}

		protected bool TypeOfResponseCopiesDirectly<T>()
		{
			var type = typeof(T);
			return type == typeof(string) || type == typeof(byte[]) || typeof(Stream).IsAssignableFrom(typeof(T));
		}
		
		protected bool SetStringOrByteResult<T>(ElasticsearchResponse<T> original, byte[] bytes)
		{
			var type = typeof(T);
			if (type == typeof(string))
			{
				this.SetStringResult(original as ElasticsearchResponse<string>, bytes);
				return true;
			}
			if (type == typeof(byte[]))
			{
				this.SetByteResult(original as ElasticsearchResponse<byte[]>, bytes);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Determines whether the stream response is our final stream response:
		/// IF response is success or known error
		/// OR maxRetries is 0 and retried is 0 (maxRetries could change in between retries to 0)
		/// AND sniff on connection fault does not find more nodes (causing maxRetry to grow)
		/// AND maxretries is no retried
		/// </summary>
		protected bool DoneProcessing<T>(
			ElasticsearchResponse<Stream> streamResponse, 
			TransportRequestState<T> requestState, 
			int maxRetries,
			int retried)
		{
			return (streamResponse != null && streamResponse.SuccessOrKnownError)
				|| (maxRetries == 0 
					&& retried == 0 
					&& !this._delegator.SniffOnFaultDiscoveredMoreNodes(requestState, retried, streamResponse)
				);
		}

		protected void ThrowMaxRetryExceptionWhenNeeded<T>(TransportRequestState<T> requestState, int maxRetries)
		{
			var tookToLong = this._delegator.TookTooLongToRetry(requestState);
			
			//not out of date and we havent depleted our retries, get the hell out of here
			if (!tookToLong && requestState.Retried < maxRetries) return;

			var innerExceptions = requestState.SeenExceptions.Where(e => e != null).ToList();
			var innerException = !innerExceptions.HasAny()
				? null
				: (innerExceptions.Count() == 1)
					? innerExceptions.First()
					: new AggregateException(requestState.SeenExceptions);

			//When we are not using pooling we forcefully rethrow the exception
			if (!requestState.UsingPooling && innerException != null && maxRetries == 0)
				throw innerException;
		
			var exceptionMessage = tookToLong 
				? CreateTookTooLongExceptionMessage(requestState, innerException) 
				: CreateMaxRetryExceptionMessage(requestState, innerException);
			throw new MaxRetryException(exceptionMessage, innerException);
		}

		protected string CreateInnerExceptionMessage<T>(TransportRequestState<T> requestState, Exception e)
		{
			if (e == null) return null;
			var aggregate = e as AggregateException;
			if (aggregate == null) 
				return "\r\n" + MaxRetryInnerMessage.F(e.GetType().Name, e.Message, e.StackTrace);
			aggregate = aggregate.Flatten();
			var innerExceptions = aggregate.InnerExceptions
				.Select(ae => MaxRetryInnerMessage.F(ae.GetType().Name, ae.Message, ae.StackTrace))
				.ToList();
			return "\r\n" + string.Join("\r\n", innerExceptions);
		}

		protected string CreateMaxRetryExceptionMessage<T>(TransportRequestState<T> requestState, Exception e)
		{
			string innerException = CreateInnerExceptionMessage(requestState, e);
			var exceptionMessage = MaxRetryExceptionMessage
				.F(requestState.Method, requestState.Path, requestState.Retried, innerException);
			return exceptionMessage;
		}

		protected string CreateTookTooLongExceptionMessage<T>(TransportRequestState<T> requestState, Exception e)
		{
			string innerException = CreateInnerExceptionMessage(requestState, e);
			var timeout = this._settings.MaxRetryTimeout.GetValueOrDefault(TimeSpan.FromMilliseconds(this._settings.Timeout));
			var exceptionMessage = TookTooLongExceptionMessage
				.F(requestState.Method, requestState.Path, requestState.Retried, innerException, timeout);
			return exceptionMessage;
		}

		protected void OptionallyCloseResponseStreamAndSetSuccess<T>(
			ITransportRequestState requestState,
			ElasticsearchServerError error,
			ElasticsearchResponse<T> typedResponse,
			ElasticsearchResponse<Stream> streamResponse)
		{
			if (streamResponse.Response != null && !typeof(Stream).IsAssignableFrom(typeof(T))) 
				streamResponse.Response.Close();
			
			if (error != null)
			{
				typedResponse.Success = false;
				if (typedResponse.OriginalException == null)
					typedResponse.OriginalException = new ElasticsearchServerException(error);
			}

			//TODO UNIT TEST OR BEGONE
			if (!typedResponse.Success
			    && requestState.RequestConfiguration != null
			    && requestState.RequestConfiguration.AllowedStatusCodes.HasAny(i => i == streamResponse.HttpStatusCode))
			{
				typedResponse.Success = true;
			}
		}

		protected void SetStringResult(ElasticsearchResponse<string> response, byte[] rawResponse)
		{
			response.Response = rawResponse.Utf8String();
		}

		protected void SetByteResult(ElasticsearchResponse<byte[]> response, byte[] rawResponse)
		{
			response.Response = rawResponse;
		}

		protected ElasticsearchServerError GetErrorFromStream<T>(Stream stream)
		{
			try
			{
				var e = this._serializer.Deserialize<OneToOneServerException>(stream);
				return ElasticsearchServerError.Create(e);
			}
			// ReSharper disable once EmptyGeneralCatchClause
			// parsing failure of exception should not be fatal, its a best case helper.
			catch { }
			return null;
		}
		

		/// <summary>
		/// Sniffs when the cluster state is stale, when sniffing returns a 401 return a response for T to return directly
		/// </summary>
		protected ElasticsearchResponse<T> TrySniffOnStaleClusterState<T>(TransportRequestState<T> requestState)
		{
			try
			{
				//If connectionSettings is configured to sniff periodically, sniff when stale.
				this._delegator.SniffOnStaleClusterState(requestState);
				return null;
			}
			catch(ElasticsearchAuthException e)
			{
				return this.HandleAuthenticationException(requestState, e);
			}
		}

		protected ElasticsearchResponse<T> HandleAuthenticationException<T>(TransportRequestState<T> requestState, ElasticsearchAuthException exception)
		{
			if (requestState.ClientSettings.ThrowOnElasticsearchServerExceptions)
				throw exception.ToElasticsearchServerException();

			var response = ElasticsearchResponse.CloneFrom<T>(exception.Response, default(T));
			response.Request = requestState.PostData;
			response.RequestUrl = requestState.Path;
			response.RequestMethod = requestState.Method;
			return response;
		}
	}
}