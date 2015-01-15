using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Connection.RequestState;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection.RequestHandlers
{
	internal class RequestHandler : RequestHandlerBase
	{
		public RequestHandler(
			IConnectionConfigurationValues settings,
			IConnectionPool connectionPool,
			IConnection connection,
			IElasticsearchSerializer serializer,
			IMemoryStreamProvider memoryStreamProvider,
			ITransportDelegator delegator)
			: base(settings, connection, connectionPool, serializer, memoryStreamProvider, delegator)
		{
		}

		public ElasticsearchResponse<T> Request<T>(TransportRequestState<T> requestState, object data = null)
		{
			var postData = PostData(data);
			requestState.TickSerialization(postData);

			var response = this.DoRequest<T>(requestState);
			requestState.SetResult(response);
			return response;
		}

		private ElasticsearchResponse<T> SelectNextNode<T>(TransportRequestState<T> requestState)
		{
			// Select the next node to hit and signal whether the selected node needs a ping
			var nodeRequiresPinging = this._delegator.SelectNextNode(requestState);
			if (!nodeRequiresPinging) return null;

			var pingSuccess = this._delegator.Ping(requestState);
			// If ping is not successful retry request on the next node the connection pool selects
			return !pingSuccess ? RetryRequest<T>(requestState) : null;
		}

		private ElasticsearchResponse<Stream> DoElasticsearchCall<T>(TransportRequestState<T> requestState)
		{
			// Do the actual request by calling into IConnection
			// We wrap it in a IRequestTimings to audit the request
			ElasticsearchResponse<Stream> streamResponse;
			using (var requestAudit = requestState.InitiateRequest(RequestType.ElasticsearchCall))
			{
				streamResponse = this.CallInToConnection(requestState);
				requestAudit.Finish(streamResponse.Success, streamResponse.HttpStatusCode);
			}
			return streamResponse;
		}

		private ElasticsearchResponse<T> ReturnStreamOrVoidResponse<T>(
			TransportRequestState<T> requestState, ElasticsearchResponse<Stream> streamResponse)
		{
			// If the response never recieved a status code and has a caught exception make sure we throw it
			if (streamResponse.HttpStatusCode.GetValueOrDefault(-1) <= 0 && streamResponse.OriginalException != null)
				throw streamResponse.OriginalException;

			// If the user explicitly wants a stream returned the undisposed stream
			if (typeof(Stream).IsAssignableFrom(typeof(T)))
				return streamResponse as ElasticsearchResponse<T>;

			if (!typeof(VoidResponse).IsAssignableFrom(typeof(T))) return null;

			var voidResponse = ElasticsearchResponse.CloneFrom<VoidResponse>(streamResponse, null);

			return voidResponse as ElasticsearchResponse<T>;
		}

		private ElasticsearchResponse<T> ReturnTypedResponse<T>(
			TransportRequestState<T> requestState,
			ElasticsearchResponse<Stream> streamResponse,
			out ElasticsearchServerError error)
		{
			error = null;
			
			// Read to memory stream if needed
			var hasResponse = streamResponse.Response != null;
			var forceRead = this._settings.KeepRawResponse || typeof(T) == typeof(string) || typeof(T) == typeof(byte[]);
			byte[] bytes = null;
			if (hasResponse && forceRead)
			{
				var ms = this._memoryStreamProvider.New();
				streamResponse.Response.CopyTo(ms);
				bytes = ms.ToArray();
				streamResponse.Response.Close();
				streamResponse.Response = ms;
				streamResponse.Response.Position = 0;
			}
			// Set rawresponse if needed
			if (this._settings.KeepRawResponse) streamResponse.ResponseRaw = bytes;

			var isValidResponse = IsValidResponse(requestState, streamResponse);
			if (isValidResponse) 
				return this.StreamToTypedResponse<T>(streamResponse, requestState, bytes);

			// If error read error 
			error = GetErrorFromStream<T>(streamResponse.Response);
			var typedResponse = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));
			this.SetStringOrByteResult(typedResponse, bytes);
			return typedResponse;
		}

		private ElasticsearchResponse<T> CoordinateRequest<T>(TransportRequestState<T> requestState, int maxRetries, int retried, ref bool aliveResponse)
		{
			var pingRetryRequest = this.SelectNextNode(requestState);
			if (pingRetryRequest != null) return pingRetryRequest;

			var streamResponse = this.DoElasticsearchCall(requestState);

			if (streamResponse.OriginalException != null)
				requestState.SeenExceptions.Add(streamResponse.OriginalException);

			aliveResponse = streamResponse.SuccessOrKnownError;

			if (!this.DoneProcessing(streamResponse, requestState, maxRetries, retried)) 
				return null;

			ElasticsearchServerError error = null;
			var typedResponse = this.ReturnStreamOrVoidResponse(requestState, streamResponse)
				?? this.ReturnTypedResponse(requestState, streamResponse, out error);

			this.OptionallyCloseResponseStreamAndSetSuccess(requestState, error, typedResponse, streamResponse);
			if (error != null && this._settings.ThrowOnElasticsearchServerExceptions)
				throw new ElasticsearchServerException(error);
			return typedResponse;
		}

		private ElasticsearchResponse<T> DoRequest<T>(TransportRequestState<T> requestState)
		{
			var sniffAuthResponse = this.TrySniffOnStaleClusterState(requestState);
			if (sniffAuthResponse != null) return sniffAuthResponse;

			bool aliveResponse = false;
			bool seenError = false;
			int retried = requestState.Retried;
			int maxRetries = this._delegator.GetMaximumRetries(requestState.RequestConfiguration);

			try
			{
				var response = this.CoordinateRequest(requestState, maxRetries, retried, ref aliveResponse);
				if (response != null) return response;
			}
			catch (ElasticsearchAuthException e)
			{
				return this.HandleAuthenticationException(requestState, e);
			}
			catch (MaxRetryException)
			{
				//TODO ifdef ExceptionDispatchInfo.Capture(ex).Throw();
				throw;
			}
			catch (ElasticsearchServerException)
			{
				//TODO ifdef ExceptionDispatchInfo.Capture(ex).Throw();
				throw;
			}
			catch (Exception e)
			{
				requestState.SeenExceptions.Add(e);
				if (maxRetries == 0 && retried == 0)
				{
					//TODO ifdef ExceptionDispatchInfo.Capture(ex).Throw();
					throw;
				}
				seenError = true;
				return RetryRequest<T>(requestState);
			}
			finally
			{
				//make sure we always call markalive on the uri if the connection was succesful
				if (!seenError && aliveResponse)
					this._connectionPool.MarkAlive(requestState.CurrentNode);
			}
			return this.RetryRequest(requestState);
		}

		private ElasticsearchResponse<T> RetryRequest<T>(TransportRequestState<T> requestState)
		{

			var maxRetries = this._delegator.GetMaximumRetries(requestState.RequestConfiguration);

			this._connectionPool.MarkDead(requestState.CurrentNode, this._settings.DeadTimeout, this._settings.MaxDeadTimeout);

			try
			{
				this._delegator.SniffOnConnectionFailure(requestState);
			}
			catch (ElasticsearchAuthException e)
			{
				//If the sniff already returned a 401 fail/return a response as early as possible
				return this.HandleAuthenticationException(requestState, e);
			}

			this.ThrowMaxRetryExceptionWhenNeeded(requestState, maxRetries);

			return this.DoRequest<T>(requestState);
		}

		private ElasticsearchResponse<Stream> CallInToConnection<T>(TransportRequestState<T> requestState)
		{
			var uri = requestState.CreatePathOnCurrentNode();
			var postData = requestState.PostData;
			var requestConfiguration = requestState.RequestConfiguration;
			switch (requestState.Method.ToLowerInvariant())
			{
				case "post": return this._connection.PostSync(uri, postData, requestConfiguration);
				case "put": return this._connection.PutSync(uri, postData, requestConfiguration);
				case "head": return this._connection.HeadSync(uri, requestConfiguration);
				case "get": return this._connection.GetSync(uri, requestConfiguration);
				case "delete":
					return postData == null || postData.Length == 0
						? this._connection.DeleteSync(uri, requestConfiguration)
						: this._connection.DeleteSync(uri, postData, requestConfiguration);
			}
			throw new Exception("Unknown HTTP method " + requestState.Method);
		}

		protected ElasticsearchResponse<T> StreamToTypedResponse<T>(
			ElasticsearchResponse<Stream> streamResponse,
			ITransportRequestState requestState,
			byte[] readBytes
			)
		{
			//set response
			if (typeof(T) == typeof(string) || typeof(T) == typeof(byte[]))
			{
				var clone = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));
				this.SetStringOrByteResult(clone, readBytes);
				return clone;
			}
			var typedResponse = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));
			using (streamResponse.Response)
			{
				var deserializationState = requestState.ResponseCreationOverride;
				var customConverter = deserializationState as Func<IElasticsearchResponse, Stream, T>;
				if (customConverter != null)
				{
					var t = customConverter(typedResponse, streamResponse.Response);
					typedResponse.Response = t;
					return typedResponse;
				}
				var deserialized = this._serializer.Deserialize<T>(streamResponse.Response);
				typedResponse.Response = deserialized;
				return typedResponse;
			}
		}

	}
}
