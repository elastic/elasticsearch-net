using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.RequestState;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection.RequestHandlers
{
	public class RequestHandlerAsync : RequestHandlerBase
	{
		public RequestHandlerAsync(
			IConnectionConfigurationValues settings,
			IConnectionPool connectionPool,
			IConnection connection,
			IElasticsearchSerializer serializer,
			IMemoryStreamProvider memoryStreamProvider,
			ITransportDelegator delegator)
			: base(settings, connection, connectionPool, serializer, memoryStreamProvider, delegator)
		{
		}

		public Task<ElasticsearchResponse<T>> RequestAsync<T>(TransportRequestState<T> requestState, object data = null)
		{
			var bytes = PostData(data);
			requestState.TickSerialization(bytes);

			return this.DoRequestAsync<T>(requestState)
				.ContinueWith(t =>
				{
					var tcs = new TaskCompletionSource<ElasticsearchResponse<T>>();
					if (t.Exception != null)
					{
						tcs.SetException(t.Exception.Flatten());
						requestState.SetResult(null);
					}
					else
					{
						tcs.SetResult(t.Result);
						requestState.SetResult(t.Result);
					}

					return tcs.Task;
				}).Unwrap()
				;
		}

		private Task<ElasticsearchResponse<T>> DoRequestAsync<T>(TransportRequestState<T> requestState)
		{
			this._delegator.SniffIfInformationIsTooOld(requestState);

			var uriRequiresPing = this._delegator.SelectNextNode(requestState);
			if (uriRequiresPing)
			{
				return this._delegator.PingAsync(requestState)
					.ContinueWith(t =>
					{
						if (t.IsFaulted)
						{
							requestState.SeenExceptions.Add(t.Exception.InnerException);
							return this.RetryRequestAsync(requestState);
						}
						if (t.IsCompleted)
						{
							return this.FinishOrRetryRequestAsync(requestState);
						}
						return null;
					}).Unwrap();
			}

			return FinishOrRetryRequestAsync(requestState);
		}

		private Task<ElasticsearchResponse<T>> FinishOrRetryRequestAsync<T>(TransportRequestState<T> requestState)
		{
			var rq = requestState.InitiateRequest(RequestType.ElasticsearchCall);
			return CallIntoConnectionAsync(requestState)
				.ContinueWith(t =>
				{
					var retried = requestState.Retried;
					if (t.IsCanceled)
						return null;
					var maxRetries = this._delegator.GetMaximumRetries(requestState.RequestConfiguration);
					if (t.IsFaulted)
					{
						rq.Dispose();
						requestState.SeenExceptions.Add(t.Exception);
						if (!requestState.UsingPooling || maxRetries == 0 && retried == 0) throw t.Exception;
						return this.RetryRequestAsync<T>(requestState);
					}

					if (t.Result.SuccessOrKnownError
					|| (
						maxRetries == 0 && retried == 0 && !this._delegator.SniffOnFaultDiscoveredMoreNodes(requestState, retried, t.Result))
					)
					{
						rq.Finish(t.Result.Success, t.Result.HttpStatusCode);
						rq.Dispose();
						var error = ThrowOrGetErrorFromStreamResponse(requestState, t.Result);
						return this.StreamToTypedResponseAsync<T>(t.Result, requestState)
							.ContinueWith(tt =>
							{

								this.SetErrorDiagnosticsAndPatchSuccess(requestState, error, tt.Result, t.Result);


								if (tt.Result.SuccessOrKnownError)
									this._connectionPool.MarkAlive(requestState.CurrentNode);
								return tt;
							}).Unwrap();
					}
					if (t.Result != null)
						rq.Finish(t.Result.Success, t.Result.HttpStatusCode);
					rq.Dispose();
					return this.RetryRequestAsync<T>(requestState);
				}).Unwrap();
		}

		private Task<ElasticsearchResponse<T>> RetryRequestAsync<T>(TransportRequestState<T> requestState)
		{
			var maxRetries = this._delegator.GetMaximumRetries(requestState.RequestConfiguration);

			this._connectionPool.MarkDead(requestState.CurrentNode, this._settings.DeadTimeout, this._settings.MaxDeadTimeout);

			this._delegator.SniffOnConnectionFailure(requestState);

			this.ThrowMaxRetryExceptionWhenNeeded(requestState, maxRetries);

			return this.DoRequestAsync<T>(requestState);
		}

		private Task<ElasticsearchResponse<Stream>> CallIntoConnectionAsync<T>(TransportRequestState<T> requestState)
		{
			var uri = requestState.CreatePathOnCurrentNode();
			var postData = requestState.PostData;
			var requestConfiguration = requestState.RequestConfiguration;
			var method = requestState.Method.ToLowerInvariant();
			try
			{
				switch (method)
				{
					case "head": return this._connection.Head(uri, requestConfiguration);
					case "get": return this._connection.Get(uri, requestConfiguration);
					case "post": return this._connection.Post(uri, postData, requestConfiguration);
					case "put": return this._connection.Put(uri, postData, requestConfiguration);
					case "delete":
						return postData == null || postData.Length == 0
							? this._connection.Delete(uri, requestConfiguration)
							: this._connection.Delete(uri, postData, requestConfiguration);
					default:
						throw new Exception("Unknown HTTP method " + requestState.Method);
				}
			}
			catch (Exception e)
			{
				var tcs = new TaskCompletionSource<ElasticsearchResponse<Stream>>();
				tcs.SetException(e);
				return tcs.Task;
			}
		}

		private Task<MemoryStream> Iterate(IEnumerable<Task> asyncIterator, MemoryStream memoryStream)
		{
			var tcs = new TaskCompletionSource<MemoryStream>();
			var enumerator = asyncIterator.GetEnumerator();
			Action<Task> recursiveBody = null;
			recursiveBody = completedTask =>
			{
				if (completedTask != null && completedTask.IsFaulted)
				{
					var exception = completedTask.Exception.InnerException;
					tcs.TrySetException(exception);
					enumerator.Dispose();
				}
				else if (enumerator.MoveNext())
				{
					enumerator.Current.ContinueWith(recursiveBody);
				}
				else
				{
					tcs.SetResult(memoryStream);
					enumerator.Dispose();
				}
			};
			recursiveBody(null);
			return tcs.Task;
		}

		private Task<ElasticsearchResponse<T>> _deserializeAsyncToResponse<T>(
			Stream response,
			ITransportRequestState requestState,
			ElasticsearchResponse<T> typedResponse
			)
		{
			var tcs = new TaskCompletionSource<ElasticsearchResponse<T>>();
			if (!IsValidResponse(requestState, typedResponse))
			{
				tcs.SetResult(typedResponse);
				return tcs.Task;
			}
			var responseInstantiater = requestState.ResponseCreationOverride;
			var customConverter = responseInstantiater as Func<IElasticsearchResponse, Stream, T>;
			if (customConverter != null)
			{
				using (response)
				{
					var t = customConverter(typedResponse, response);
					typedResponse.Response = t;
					tcs.SetResult(typedResponse);
					return tcs.Task;
				}
			}
			return this._serializer.DeserializeAsync<T>(response)
				.ContinueWith(t =>
				{
					typedResponse.Response = t.Result;
					return typedResponse;
				});
		}

		private IEnumerable<Task<MemoryStream>> ReadStreamAsync(Stream responseStream, MemoryStream memoryStream)
		{
			var buffer = new byte[BUFFER_SIZE];
			try
			{
				while (responseStream != null)
				{
					var read = Task<int>.Factory.FromAsync(responseStream.BeginRead, responseStream.EndRead, buffer, 0, BUFFER_SIZE, null);
					yield return read.ContinueWith(t => memoryStream);
					if (read.Result == 0) break;
					memoryStream.Write(buffer, 0, read.Result);
				}
			}
			finally
			{
				if (responseStream != null)
					responseStream.Dispose();
			}
		}

		private Task<ElasticsearchResponse<T>> StreamToTypedResponseAsync<T>(
			ElasticsearchResponse<Stream> streamResponse,
			ITransportRequestState requestState
			)
		{
			var tcs = new TaskCompletionSource<ElasticsearchResponse<T>>();

			//if the user explicitly wants a stream return the undisposed stream
			if (typeof(Stream).IsAssignableFrom(typeof(T)))
			{
				tcs.SetResult(streamResponse as ElasticsearchResponse<T>);
				return tcs.Task;
			}

			var cs = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));

			if (typeof(T) == typeof(VoidResponse))
			{
				tcs.SetResult(cs);
				if (streamResponse.Response != null)
					streamResponse.Response.Dispose();
				return tcs.Task;
			}

			if (!(this._settings.KeepRawResponse || this.TypeOfResponseCopiesDirectly<T>()))
				return _deserializeAsyncToResponse(streamResponse.Response, requestState, cs);

			var memoryStream = this._memoryStreamProvider.New();
			return this.Iterate(this.ReadStreamAsync(streamResponse.Response, memoryStream), memoryStream)
				.ContinueWith(t =>
				{
					var readStream = t.Result;
					readStream.Position = 0;
					var bytes = readStream.ToArray();
					cs.ResponseRaw = this._settings.KeepRawResponse ? bytes : null;

					var type = typeof(T);
					if (type == typeof(string))
					{
						this.SetStringResult(cs as ElasticsearchResponse<string>, bytes);
						tcs.SetResult(cs);
						return tcs.Task;
					}
					if (type == typeof(byte[]))
					{
						this.SetByteResult(cs as ElasticsearchResponse<byte[]>, bytes);
						tcs.SetResult(cs);
						return tcs.Task;
					}

					return _deserializeAsyncToResponse(readStream, requestState, cs);
				})
				.Unwrap();

		}
	}
}
