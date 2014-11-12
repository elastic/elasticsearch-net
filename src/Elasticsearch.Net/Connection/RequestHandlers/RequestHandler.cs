using System;
using System.Collections.Generic;
using System.IO;
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

		private ElasticsearchResponse<T> DoRequest<T>(TransportRequestState<T> requestState)
		{
			try
			{
				//If connectionSettings is configured to sniff periodically, sniff when stale.
				this._delegator.SniffIfInformationIsTooOld(requestState);
			}
			catch(ElasticsearchAuthenticationException e)
			{
				return this.HandleAuthenticationException(requestState, e);
			}

			var aliveResponse = false;
			var seenError = false;
			var retried = requestState.Retried;

			//select the next node to hit and signal wheter the selected node needs a ping
			var nodeRequiresPinging = this._delegator.SelectNextNode(requestState);

			//figure out the maximum number of retries, this might 
			var maxRetries = this._delegator.GetMaximumRetries(requestState.RequestConfiguration);
			try
			{
				//connection pool signalled that the current node needs pinging
				if (nodeRequiresPinging)
				{
					var pingSuccess = this._delegator.Ping(requestState);
					//if ping is not succesful retry request on the next node the connection pool selects
					if (!pingSuccess) return RetryRequest<T>(requestState);
				}

				//do the actual request by calling into IConnection
				//we wrap it in a IRequestTimings to audit the request.
				ElasticsearchResponse<Stream> streamResponse;
				using (var requestAudit = requestState.InitiateRequest(RequestType.ElasticsearchCall))
				{
					streamResponse = this.CallInToConnection(requestState);
					requestAudit.Finish(streamResponse.Success, streamResponse.HttpStatusCode);
				}

				if (this.DoneProcessing(streamResponse, requestState, maxRetries, retried))
				{
					//if the response never recieved a status code and has a caught exception make sure we throw it
					if (streamResponse.HttpStatusCode.GetValueOrDefault(-1) <= 0 && streamResponse.OriginalException != null)
						throw streamResponse.OriginalException;

					//if the user explicitly wants a stream returned the undisposed stream
					if (typeof(Stream).IsAssignableFrom(typeof(T)))
						return streamResponse as ElasticsearchResponse<T>;

					ElasticsearchResponse<T> typedResponse = null;
					ElasticsearchServerError error = null;
					if (typeof(VoidResponse).IsAssignableFrom(typeof(T)))
					{
						streamResponse.Response.Close();
						var voidResponse = ElasticsearchResponse.CloneFrom<VoidResponse>(streamResponse, null);
						typedResponse = voidResponse as ElasticsearchResponse<T>;
					}
					else
					{
						//read to ms if needed
						var hasResponse = streamResponse.Response != null && streamResponse.Response.Length > 0;
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
						//set rawresponse if needed
						if (this._settings.KeepRawResponse) streamResponse.ResponseRaw = bytes;

						//if error read error 
						var isValidResponse = IsValidResponse(requestState, streamResponse);
						if (!isValidResponse)
						{
							error = GetErrorFromStream<T>(streamResponse.Response);
							typedResponse = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));
							this.ReturnStringOrByteArray(typedResponse, bytes);

						}
						else typedResponse = this.StreamToTypedResponse<T>(streamResponse, requestState, bytes);
					}
					this.SetErrorDiagnosticsAndPatchSuccess(requestState, error, typedResponse, streamResponse);
					aliveResponse = typedResponse.SuccessOrKnownError;
					if (error != null && this._settings.ThrowOnElasticsearchServerExceptions)
						throw new ElasticsearchServerException(error);
					return typedResponse;


				}
			}
			catch (ElasticsearchAuthenticationException e)
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
				if (!requestState.UsingPooling || maxRetries == 0 && retried == 0)
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

			try
			{
				return RetryRequest<T>(requestState);
			}
			catch(ElasticsearchAuthenticationException e)
			{
				return this.HandleAuthenticationException(requestState, e);
			}
		}

		private ElasticsearchResponse<T> RetryRequest<T>(TransportRequestState<T> requestState)
		{
			var maxRetries = this._delegator.GetMaximumRetries(requestState.RequestConfiguration);

			this._connectionPool.MarkDead(requestState.CurrentNode, this._settings.DeadTimeout, this._settings.MaxDeadTimeout);

			this._delegator.SniffOnConnectionFailure(requestState);

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
				this.ReturnStringOrByteArray(clone, readBytes);
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
