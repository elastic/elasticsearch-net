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
	public class RequestHandler : RequestHandlerBase
	{
		public RequestHandler(
			IConnectionConfigurationValues settings, 
			IConnectionPool connectionPool,
			IConnection connection, 
			IElasticsearchSerializer serializer, 
			IMemoryStreamProvider memoryStreamProvider,
			ITransportDelegator delegator)
			: base (settings, connection, connectionPool, serializer, memoryStreamProvider, delegator)
		{
		}

		public ElasticsearchResponse<T> Request<T>(TransportRequestState<T> requestState, object data = null)
		{
				var bytes = PostData(data);
				requestState.TickSerialization(bytes);

				var result = this.DoRequest<T>(requestState);

				requestState.SetResult(result);
				return result;
		}

		private ElasticsearchResponse<T> DoRequest<T>(TransportRequestState<T> requestState)
		{
			this._delegator.SniffIfInformationIsTooOld(requestState);

			var aliveResponse = false;
			var seenError = false;

			var retried = requestState.Retried;
			var nodeRequiresPinging = this._delegator.SelectNextNode(requestState);

			var maxRetries = this._delegator.GetMaximumRetries(requestState.RequestConfiguration);

			try
			{
				if (nodeRequiresPinging)
				{
					var pingSuccess = this._delegator.Ping(requestState);
					if (!pingSuccess) return RetryRequest<T>(requestState);
				}

				ElasticsearchResponse<Stream> streamResponse;
				using (var rq = requestState.InitiateRequest(RequestType.ElasticsearchCall))
				{
					streamResponse = this.CallInToConnection(requestState);
					rq.Finish(streamResponse.Success, streamResponse.HttpStatusCode);
				}

				if (streamResponse.SuccessOrKnownError
					|| (
						maxRetries == 0 && retried == 0 && !this._delegator.SniffOnFaultDiscoveredMoreNodes(requestState, retried, streamResponse))
					)
				{
					var error = ThrowOrGetErrorFromStreamResponse(requestState, streamResponse);

					var typedResponse = this.StreamToTypedResponse<T>(streamResponse, requestState, error);
					this.SetErrorDiagnosticsAndPatchSuccess(requestState, error, typedResponse, streamResponse);
					aliveResponse = typedResponse.SuccessOrKnownError;
					return typedResponse;
				}
			}
			catch (MaxRetryException)
			{
				throw;
			}
			catch (ElasticsearchServerException)
			{
				throw;
			}
			catch (Exception e)
			{
				requestState.SeenExceptions.Add(e);
				if (!requestState.UsingPooling || maxRetries == 0 && retried == 0)
				{
					if (_throwMaxRetry)
						new MaxRetryException(e);
					else throw;
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
			return RetryRequest<T>(requestState);
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
		protected ElasticsearchResponse<T> StreamToTypedResponse<T>(ElasticsearchResponse<Stream> streamResponse, ITransportRequestState requestState, ElasticsearchServerError error)
		{
			//if the user explicitly wants a stream returned the undisposed stream
			if (typeof(Stream).IsAssignableFrom(typeof(T)))
				return streamResponse as ElasticsearchResponse<T>;

			if (error != null)
			{
				if (streamResponse.Response != null) streamResponse.Response.Close();
				var original = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));
				ElasticsearchResponse<T> unserializedResponse;
				if (this.ReturnStringOrByteArray(original, streamResponse.ResponseRaw, out unserializedResponse))
					return unserializedResponse;

				return original;
			}

			var cs = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));
			using (streamResponse.Response)
			{
				if (typeof(T) == typeof(VoidResponse))
					return cs;

				if (!(this._settings.KeepRawResponse || this.TypeOfResponseCopiesDirectly<T>()))
					return this._deserializeToResponse(cs, streamResponse.Response, requestState);

				using (var memoryStream = this._memoryStreamProvider.New())
				{
					if (streamResponse.Response != null)
						streamResponse.Response.CopyTo(memoryStream);
					memoryStream.Position = 0;
					var bytes = memoryStream.ToArray();
					cs.ResponseRaw = this._settings.KeepRawResponse ? bytes : null;
					ElasticsearchResponse<T> unserializedResponse;
					if (this.ReturnStringOrByteArray(cs, bytes, out unserializedResponse))
						return unserializedResponse;

					return this._deserializeToResponse(cs, memoryStream, requestState);
				}
			}
		}

		private ElasticsearchResponse<T> _deserializeToResponse<T>(
			ElasticsearchResponse<T> typedResponse,
			Stream responseStream,
			ITransportRequestState requestState
			)
		{
			if (!IsValidResponse(requestState, typedResponse))
				return typedResponse;

			var deserializationState = requestState.ResponseCreationOverride;
			var customConverter = deserializationState as Func<IElasticsearchResponse, Stream, T>;
			if (customConverter != null)
			{
				using (responseStream)
				{
					var t = customConverter(typedResponse, responseStream);
					typedResponse.Response = t;
					return typedResponse;
				}
			}
			var deserialized = this._serializer.Deserialize<T>(responseStream);
			typedResponse.Response = deserialized;
			return typedResponse;
		}


	}
}
