using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Connection.RequestState;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Serialization;
using PurifyNet;

namespace Elasticsearch.Net.Connection
{
	public class Transport : ITransport
	{
		const int BUFFER_SIZE = 4096;

		protected static readonly string MaxRetryExceptionMessage = "Failed after retrying {2} times: '{0} {1}'. {3}";
		protected static readonly string MaxRetryInnerMessage = "InnerException: {0}, InnerMessage: {1}, InnerStackTrace: {2}";
		protected internal readonly IConnectionConfigurationValues ConfigurationValues;
		protected internal readonly IConnection Connection;
		private readonly IElasticsearchSerializer _serializer;

		private readonly IConnectionPool _connectionPool;
		private readonly IDateTimeProvider _dateTimeProvider;
		private DateTime? _lastSniff;
		private bool _throwMaxRetry;

		public IConnectionConfigurationValues Settings { get { return ConfigurationValues; } }
		public IElasticsearchSerializer Serializer { get { return _serializer; } }

		public Transport(
			IConnectionConfigurationValues configurationValues,
			IConnection connection,
			IElasticsearchSerializer serializer,
			IDateTimeProvider dateTimeProvider = null
			)
		{
			this.ConfigurationValues = configurationValues;
			this.Connection = connection ?? new HttpConnection(configurationValues);
			this._serializer = serializer ?? new ElasticsearchDefaultSerializer();
			this._connectionPool = this.ConfigurationValues.ConnectionPool;
			this._throwMaxRetry = !(this._connectionPool is SingleNodeConnectionPool);

			this._dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();

			this._lastSniff = this._dateTimeProvider.Now();

			this.Settings.Serializer = this._serializer;
			if (this._connectionPool.AcceptsUpdates && this.Settings.SniffsOnStartup)
				this.SniffClusterState();
		}


		/* PING/SNIFF	*** ********************************************/

		private bool Ping(ITransportRequestState requestState)
		{
			var pingTimeout = this.Settings.PingTimeout.GetValueOrDefault(200);
			pingTimeout = requestState.RequestConfiguration != null
				? requestState.RequestConfiguration.ConnectTimeout.GetValueOrDefault(pingTimeout)
				: pingTimeout;
			var requestOverrides = new RequestConfiguration
			{
				ConnectTimeout = pingTimeout,
				RequestTimeout = pingTimeout
			};
			try
			{
				ElasticsearchResponse<Stream> response;
				using (var rq = requestState.InitiateRequest(RequestType.Ping))
				{
					response = this.Connection.HeadSync(requestState.CreatePathOnCurrentNode(""), requestOverrides);
					rq.Finish(response.Success, response.HttpStatusCode);
				}
				if (!response.HttpStatusCode.HasValue || response.HttpStatusCode.Value == -1)
					throw new Exception("ping returned no status code", response.OriginalException);
				if (response.Response == null)
					return response.Success;


				using (response.Response)
					return response.Success;

			}
			catch (Exception e)
			{
				throw new PingException(requestState.CurrentNode, e);
			}
		}

		private Task<bool> PingAsync(ITransportRequestState requestState)
		{
			var pingTimeout = this.Settings.PingTimeout.GetValueOrDefault(200);
			pingTimeout = requestState.RequestConfiguration != null
				? requestState.RequestConfiguration.ConnectTimeout.GetValueOrDefault(pingTimeout)
				: pingTimeout;
			var requestOverrides = new RequestConfiguration
			{
				ConnectTimeout = pingTimeout,
				RequestTimeout = pingTimeout
			};
			var rq = requestState.InitiateRequest(RequestType.Ping);
			try
			{
				return this.Connection.Head(requestState.CreatePathOnCurrentNode(""), requestOverrides)
					.ContinueWith(t =>
					{
						if (t.IsFaulted)
						{
							rq.Finish(false, null);
							rq.Dispose();
							throw new PingException(requestState.CurrentNode, t.Exception);
						}
						rq.Finish(t.Result.Success, t.Result.HttpStatusCode);
						rq.Dispose();
						var response = t.Result;
						if (!response.HttpStatusCode.HasValue || response.HttpStatusCode.Value == -1)
							throw new PingException(requestState.CurrentNode, t.Exception);

						using (response.Response)
							return response.Success;
					});
			}
			catch (Exception e)
			{
				var tcs = new TaskCompletionSource<bool>();
				var pingException = new PingException(requestState.CurrentNode, e);
				tcs.SetException(pingException);
				return tcs.Task;
			}
		}

		private IList<Uri> Sniff(ITransportRequestState ownerState = null)
		{
			var pingTimeout = this.Settings.PingTimeout.GetValueOrDefault(50);
			var requestOverrides = new RequestConfiguration
			{
				ConnectTimeout = pingTimeout,
				RequestTimeout = pingTimeout,
				DisableSniff = true //sniff call should never recurse 
			};

			var requestParameters = new RequestParameters { RequestConfiguration = requestOverrides };
			try
			{

				var path = "_nodes/_all/clear?timeout=" + pingTimeout;
				ElasticsearchResponse<Stream> response;
				using (var requestState = new TransportRequestState<Stream>(this.Settings, requestParameters, "GET", path))
				{
					response = this.DoRequest(requestState);

					//inform the owing request state of the requests the sniffs did.
					if (requestState.RequestMetrics != null && ownerState != null)
					{
						foreach (var r in requestState.RequestMetrics.Where(p => p.RequestType == RequestType.ElasticsearchCall))
							r.RequestType = RequestType.Sniff;


						if (ownerState.RequestMetrics == null) ownerState.RequestMetrics = new List<RequestMetrics>();
						ownerState.RequestMetrics.AddRange(requestState.RequestMetrics);
					}
					if (response.Response == null) return null;

					using (response.Response)
					{
						return Sniffer.FromStream(response, response.Response, this.Serializer);
					}
				}
			}
			catch (MaxRetryException e)
			{
				throw new MaxRetryException(new SniffException(e));
			}
		}

		private void SniffClusterState(ITransportRequestState requestState = null)
		{
			if (!this._connectionPool.AcceptsUpdates)
				return;

			var newClusterState = this.Sniff(requestState);
			if (!newClusterState.HasAny())
				return;

			this._connectionPool.UpdateNodeList(newClusterState);
			this._lastSniff = this._dateTimeProvider.Now();

		}

		private void SniffIfInformationIsTooOld(ITransportRequestState requestState)
		{
			if (SniffingDisabled(requestState.RequestConfiguration))
				return;

			var sniffLifeSpan = this.ConfigurationValues.SniffInformationLifeSpan;
			var now = this._dateTimeProvider.Now();
			if (requestState.Retried == 0 && this._lastSniff.HasValue &&
				sniffLifeSpan.HasValue && sniffLifeSpan.Value < (now - this._lastSniff.Value))
				this.SniffClusterState(requestState);
		}

		private void SniffOnConnectionFailure(ITransportRequestState requestState)
		{
			if (requestState.SniffedOnConnectionFailure
				|| SniffingDisabled(requestState.RequestConfiguration)
				|| !this.ConfigurationValues.SniffsOnConnectionFault
				|| requestState.Retried != 0) return;

			this.SniffClusterState(requestState);
			requestState.SniffedOnConnectionFailure = true;
		}

		/* REQUEST STATE *** ********************************************/

		/// <summary>
		/// Returns either the fixed maximum set on the connection configuration settings or the number of nodes
		/// </summary>
		/// <param name="requestState"></param>
		private int GetMaximumRetries(IRequestConfiguration requestConfiguration)
		{
			//if we have a request specific max retry setting use that
			if (requestConfiguration != null && requestConfiguration.MaxRetries.HasValue)
				return requestConfiguration.MaxRetries.Value;

			return this.ConfigurationValues.MaxRetries.GetValueOrDefault(this._connectionPool.MaxRetries);
		}

		private bool SniffingDisabled(IRequestConfiguration requestConfiguration)
		{
			if (!this._connectionPool.AcceptsUpdates)
				return true;
			if (requestConfiguration == null)
				return false;
			return requestConfiguration.DisableSniff.GetValueOrDefault(false);
		}

		private bool SniffOnFaultDiscoveredMoreNodes(ITransportRequestState requestState, int retried, ElasticsearchResponse<Stream> streamResponse)
		{
			if (retried != 0 || streamResponse.SuccessOrKnownError) return false;
			SniffOnConnectionFailure(requestState);
			return this.GetMaximumRetries(requestState.RequestConfiguration) > 0;
		}

		/// <summary>
		/// Selects next node uri on request state
		/// </summary>
		/// <returns>bool hint whether the new current node needs to pinged first</returns>
		private bool SelectNextNode(ITransportRequestState requestState)
		{
			if (requestState.RequestConfiguration != null && requestState.RequestConfiguration.ForceNode != null)
			{
				requestState.Seed = 0;
				return false;
			}
			int initialSeed;
			bool shouldPingHint;
			var baseUri = this._connectionPool.GetNext(requestState.Seed, out initialSeed, out shouldPingHint);
			requestState.Seed = initialSeed;
			requestState.CurrentNode = baseUri;
			return shouldPingHint
				&& !this.ConfigurationValues.DisablePings
				&& (requestState.RequestConfiguration == null
					|| !requestState.RequestConfiguration.DisablePing.GetValueOrDefault(false));

		}

		private byte[] PostData(object data)
		{
			var bytes = data as byte[];
			if (bytes != null)
				return bytes;

			var s = data as string;
			if (s != null)
				return s.Utf8Bytes();
			if (data == null) return null;
			var ss = data as IEnumerable<string>;
			if (ss != null)
				return (string.Join("\n", ss) + "\n").Utf8Bytes();

			var so = data as IEnumerable<object>;
			if (so == null)
				return this._serializer.Serialize(data);
			var joined = string.Join("\n", so
				.Select(soo => this._serializer.Serialize(soo, SerializationFormatting.None).Utf8String())) + "\n";
			return joined.Utf8Bytes();
		}


		/* SYNC			*** ********************************************/

		public ElasticsearchResponse<T> DoRequest<T>(string method, string path, object data = null, IRequestParameters requestParameters = null)
		{
			using (var requestState = new TransportRequestState<T>(this.Settings, requestParameters, method, path))
			{
				var bytes = PostData(data);
				requestState.TickSerialization(bytes);

				var result = this.DoRequest<T>(requestState);

				requestState.SetResult(result);

				return result;
			}
		}

		private ElasticsearchResponse<T> DoRequest<T>(TransportRequestState<T> requestState)
		{
			var retried = requestState.Retried;

			this.SniffIfInformationIsTooOld(requestState);

			var aliveResponse = false;

			var nodeRequiresPinging = this.SelectNextNode(requestState);

			var seenError = false;
			var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);

			try
			{
				if (nodeRequiresPinging)
				{
					var pingSuccess = this.Ping(requestState);
					if (!pingSuccess)
					{
						return RetryRequest<T>(requestState);
					}
				}

				ElasticsearchResponse<Stream> streamResponse;
				using (var rq = requestState.InitiateRequest(RequestType.ElasticsearchCall))
				{
					streamResponse = this.CallInToConnection(requestState);
					rq.Finish(streamResponse.Success, streamResponse.HttpStatusCode);
				}

				if (streamResponse.SuccessOrKnownError
					|| (
						maxRetries == 0 && retried == 0 && !SniffOnFaultDiscoveredMoreNodes(requestState, retried, streamResponse))
					)
				{
					var error = ThrowOrGetErrorFromStreamResponse(requestState, streamResponse);

					var typedResponse = this.StreamToTypedResponse<T>(streamResponse, requestState);
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
				if (maxRetries == 0 && retried == 0)
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
			var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);

			this._connectionPool.MarkDead(requestState.CurrentNode, this.ConfigurationValues.DeadTimeout, this.ConfigurationValues.MaxDeadTimeout);

			SniffOnConnectionFailure(requestState);

			ThrowMaxRetryExceptionWhenNeeded(requestState, maxRetries);

			return this.DoRequest<T>(requestState);
		}

		private ElasticsearchResponse<Stream> CallInToConnection<T>(TransportRequestState<T> requestState)
		{
			var uri = requestState.CreatePathOnCurrentNode();
			var postData = requestState.PostData;
			var requestConfiguration = requestState.RequestConfiguration;
			switch (requestState.Method.ToLowerInvariant())
			{
				case "post": return this.Connection.PostSync(uri, postData, requestConfiguration);
				case "put": return this.Connection.PutSync(uri, postData, requestConfiguration);
				case "head": return this.Connection.HeadSync(uri, requestConfiguration);
				case "get": return this.Connection.GetSync(uri, requestConfiguration);
				case "delete":
					return postData == null || postData.Length == 0
						? this.Connection.DeleteSync(uri, requestConfiguration)
						: this.Connection.DeleteSync(uri, postData, requestConfiguration);
			}
			throw new Exception("Unknown HTTP method " + requestState.Method);
		}


		/* ASYNC		*** ********************************************/

		public Task<ElasticsearchResponse<T>> DoRequestAsync<T>(string method, string path, object data = null, IRequestParameters requestParameters = null)
		{
			using (var requestState = new TransportRequestState<T>(this.Settings, requestParameters, method, path))
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
		}

		private Task<ElasticsearchResponse<T>> DoRequestAsync<T>(TransportRequestState<T> requestState)
		{
			this.SniffIfInformationIsTooOld(requestState);

			var uriRequiresPing = this.SelectNextNode(requestState);
			if (uriRequiresPing)
			{
				return this.PingAsync(requestState)
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
					var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);
					if (t.IsFaulted)
					{
						rq.Dispose();
						if (maxRetries == 0 && retried == 0) throw t.Exception;
						requestState.SeenExceptions.Add(t.Exception);
						return this.RetryRequestAsync<T>(requestState);
					}

					if (t.Result.SuccessOrKnownError
					|| (
						maxRetries == 0 && retried == 0 && !SniffOnFaultDiscoveredMoreNodes(requestState, retried, t.Result))
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
			var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);

			this._connectionPool.MarkDead(requestState.CurrentNode, this.ConfigurationValues.DeadTimeout, this.ConfigurationValues.MaxDeadTimeout);

			this.SniffOnConnectionFailure(requestState);

			ThrowMaxRetryExceptionWhenNeeded(requestState, maxRetries);

			return this.DoRequestAsync<T>(requestState);
		}

		private static void ThrowMaxRetryExceptionWhenNeeded<T>(TransportRequestState<T> requestState, int maxRetries)
		{
			if (requestState.Retried < maxRetries) return;
			var innerExceptions = requestState.SeenExceptions.Where(e => e != null).ToList();
			var innerException = !innerExceptions.HasAny() 
				? null
				: (innerExceptions.Count() == 1)
					? innerExceptions.First()
					: new AggregateException(requestState.SeenExceptions);
			var exceptionMessage = CreateMaxRetryExceptionMessage(requestState, innerException);
			throw new MaxRetryException(exceptionMessage, innerException);
		}

		private static string CreateMaxRetryExceptionMessage<T>(TransportRequestState<T> requestState, Exception e)
		{
			string innerException = null;
			if (e != null)
			{
				var aggregate = e as AggregateException;
				if (aggregate != null)
				{

					aggregate = aggregate.Flatten();
					var innerExceptions = aggregate.InnerExceptions
						.Select(ae => MaxRetryInnerMessage.F(ae.GetType().Name, ae.Message, ae.StackTrace))
						.ToList();
					innerException = "\r\n" + string.Join("\r\n", innerExceptions);
				}
				else
					innerException = "\r\n" + MaxRetryInnerMessage.F(e.GetType().Name, e.Message, e.StackTrace);
			}
			var exceptionMessage = MaxRetryExceptionMessage
				.F(requestState.Method, requestState.Path, requestState.Retried, innerException);
			return exceptionMessage;
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
					case "head": return this.Connection.Head(uri, requestConfiguration);
					case "get": return this.Connection.Get(uri, requestConfiguration);
					case "post": return this.Connection.Post(uri, postData, requestConfiguration);
					case "put": return this.Connection.Put(uri, postData, requestConfiguration);
					case "delete":
						return postData == null || postData.Length == 0
							? this.Connection.Delete(uri, requestConfiguration)
							: this.Connection.Delete(uri, postData, requestConfiguration);
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

		/* STREAM HANDLING	********************************************/

		private ElasticsearchServerError ThrowOrGetErrorFromStreamResponse<T>(
			TransportRequestState<T> requestState,
			ElasticsearchResponse<Stream> streamResponse)
		{
			if (IsValidResponse(requestState, streamResponse))
				return null;

			if (((streamResponse.HttpStatusCode.HasValue && streamResponse.HttpStatusCode.Value <= 0)
				|| !streamResponse.HttpStatusCode.HasValue) && streamResponse.OriginalException != null)
				throw streamResponse.OriginalException;

			ElasticsearchServerError error = null;

			var type = typeof(T);
			if (typeof(Stream).IsAssignableFrom(typeof(T)) || typeof(T) == typeof(VoidResponse))
				return null;

			if (streamResponse.Response != null && !(this.Settings.KeepRawResponse || this.TypeOfResponseCopiesDirectly<T>()))
			{
				var e = this.Serializer.Deserialize<OneToOneServerException>(streamResponse.Response);
				error = ElasticsearchServerError.Create(e);
			}
			else if (streamResponse.Response != null)
			{
				var ms = new MemoryStream();
				streamResponse.Response.CopyTo(ms);
				ms.Position = 0;
				streamResponse.ResponseRaw = this.Settings.KeepRawResponse ? ms.ToArray() : null;
				try
				{
					var e = this.Serializer.Deserialize<OneToOneServerException>(ms);
					error = ElasticsearchServerError.Create(e);
				}
				catch (Exception e)
				{
					var raw = ms.ToArray().Utf8String();
				}
				ms.Position = 0;
				streamResponse.Response.Close();
				streamResponse.Response = ms;
			}
			else
				error = new ElasticsearchServerError
				{
					Status = streamResponse.HttpStatusCode.GetValueOrDefault(-1)
				};
			if (this.Settings.ThrowOnElasticsearchServerExceptions)
				throw new ElasticsearchServerException(error);
			return error;
		}

		private static bool IsValidResponse(ITransportRequestState requestState, IElasticsearchResponse streamResponse)
		{
			return streamResponse.Success ||
				(!streamResponse.Success
				&& requestState.RequestConfiguration != null
				&& requestState.RequestConfiguration.AllowedStatusCodes.HasAny(i => i == streamResponse.HttpStatusCode)
				);

		}

		private bool TypeOfResponseCopiesDirectly<T>()
		{
			var type = typeof(T);
			return type == typeof(string) || type == typeof(byte[]) || typeof(Stream).IsAssignableFrom(typeof(T));
		}

		private ElasticsearchResponse<T> StreamToTypedResponse<T>(
			ElasticsearchResponse<Stream> streamResponse,
			ITransportRequestState requestState
			)
		{
			//if the user explicitly wants a stream returned the undisposed stream
			if (typeof(Stream).IsAssignableFrom(typeof(T)))
				return streamResponse as ElasticsearchResponse<T>;

			var cs = ElasticsearchResponse.CloneFrom<T>(streamResponse, default(T));
			using (streamResponse.Response)
			using (var memoryStream = new MemoryStream())
			{
				if (typeof(T) == typeof(VoidResponse))
					return cs;

				var type = typeof(T);
				if (!(this.Settings.KeepRawResponse || this.TypeOfResponseCopiesDirectly<T>()))
					return this._deserializeToResponse(cs, streamResponse.Response, requestState);


				if (streamResponse.Response != null)
					streamResponse.Response.CopyTo(memoryStream);
				memoryStream.Position = 0;
				var bytes = memoryStream.ToArray();
				cs.ResponseRaw = this.Settings.KeepRawResponse ? bytes : null;
				if (type == typeof(string))
				{
					this.SetStringResult(cs as ElasticsearchResponse<string>, bytes);
					return cs;
				}
				if (type == typeof(byte[]))
				{
					this.SetByteResult(cs as ElasticsearchResponse<byte[]>, bytes);
					return cs;
				}
				return this._deserializeToResponse(cs, memoryStream, requestState);
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

			if (!(this.Settings.KeepRawResponse || this.TypeOfResponseCopiesDirectly<T>()))
				return _deserializeAsyncToResponse(streamResponse.Response, requestState, cs);

			var memoryStream = new MemoryStream();
			return this.Iterate(this.ReadStreamAsync(streamResponse.Response, memoryStream), memoryStream)
				.ContinueWith(t =>
				{
					var readStream = t.Result;
					readStream.Position = 0;
					var bytes = readStream.ToArray();
					cs.ResponseRaw = this.Settings.KeepRawResponse ? bytes : null;

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
			var deserialized = this.Serializer.Deserialize<T>(responseStream);
			typedResponse.Response = deserialized;
			return typedResponse;
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
			return this.Serializer.DeserializeAsync<T>(response)
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

		private void SetErrorDiagnosticsAndPatchSuccess<T>(
			ITransportRequestState requestState,
			ElasticsearchServerError error,
			ElasticsearchResponse<T> typedResponse,
			IElasticsearchResponse streamResponse)
		{
			if (error != null)
			{
				typedResponse.Success = false;
				typedResponse.OriginalException = new ElasticsearchServerException(error);
			}
			if (!typedResponse.Success
				&& requestState.RequestConfiguration != null
				&& requestState.RequestConfiguration.AllowedStatusCodes.HasAny(i => i == streamResponse.HttpStatusCode))
			{
				typedResponse.Success = true;
			}
		}

		private void SetStringResult(ElasticsearchResponse<string> response, byte[] rawResponse)
		{
			response.Response = rawResponse.Utf8String();
		}

		private void SetByteResult(ElasticsearchResponse<byte[]> response, byte[] rawResponse)
		{
			response.Response = rawResponse;
		}
	}
}