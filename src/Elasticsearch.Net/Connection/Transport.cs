using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Serialization;
using PurifyNet;

namespace Elasticsearch.Net.Connection
{
	public class Transport : ITransport
	{
		protected static readonly string MaxRetryExceptionMessage = "Unable to perform request: '{0} {1}' on any of the nodes after retrying {2} times.";
		protected internal readonly IConnectionConfigurationValues ConfigurationValues;
		protected internal readonly IConnection Connection;
		private readonly IElasticsearchSerializer _serializer;

		private readonly IConnectionPool _connectionPool;
		private readonly IDateTimeProvider _dateTimeProvider;
		private DateTime? _lastSniff;

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

			this._dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();

			this._lastSniff = this._dateTimeProvider.Now();

			this.Settings.Serializer = this._serializer;
			if (this._connectionPool.AcceptsUpdates && this.Settings.SniffsOnStartup)
				this.Sniff();
		}


		public virtual bool Ping(Uri baseUri)
		{
			var pingTimeout = this.Settings.PingTimeout.GetValueOrDefault(50);
			var requestOverrides = new RequestConfiguration
			{
				ConnectTimeout = pingTimeout,
				RequestTimeout = pingTimeout
			};
			var response = this.Connection.HeadSync(CreateUriToPath(baseUri, ""), requestOverrides);
			if (response.Response == null) return false;
			using(response.Response)
				return response.Success;
		}
		
		public virtual Task<bool> PingAsync(Uri baseUri)
		{
			var pingTimeout = this.Settings.PingTimeout.GetValueOrDefault(50);
			var requestOverrides = new RequestConfiguration
			{
				ConnectTimeout = pingTimeout,
				RequestTimeout = pingTimeout
			};

			return this.Connection.Head(CreateUriToPath(baseUri, ""), requestOverrides)
				.ContinueWith(t=>
				{
					var response = t.Result;
					if (response.Response == null) return false;

					using(response.Response)
						return response.Success;
				});
		}

		public IList<Uri> Sniff()
		{
			var pingTimeout = this.Settings.PingTimeout.GetValueOrDefault(50);
			var requestOverrides = new RequestConfiguration
			{
				ConnectTimeout = pingTimeout,
				RequestTimeout = pingTimeout,
				DisableSniff = true //sniff call should never recurse 
			};

			var requestParameters = new RequestParameters { RequestConfiguration = requestOverrides };
			
			var path = "_nodes/_all/clear?timeout=" + pingTimeout;

			using (var requestState = new TransportRequestState<Stream>(this.Settings, requestParameters, "GET", path))
			{
				var response = this.DoRequest(requestState);
				if (response.Response == null) return null;

				using (response.Response)
				{
					return Sniffer.FromStream(response, response.Response, this.Serializer);
				}
			}
		}
		
		public virtual void SniffClusterState()
		{
			if (!this._connectionPool.AcceptsUpdates)
				return;

			var newClusterState = this.Sniff();
			if (!newClusterState.HasAny())
				return;

			this._connectionPool.UpdateNodeList(newClusterState);
			this._lastSniff = this._dateTimeProvider.Now();
		
		}

		private void SniffIfInformationIsTooOld(int retried)
		{
			var sniffLifeSpan = this.ConfigurationValues.SniffInformationLifeSpan;
			var now = this._dateTimeProvider.Now();
			if (retried == 0 && this._lastSniff.HasValue &&
				sniffLifeSpan.HasValue && sniffLifeSpan.Value < (now - this._lastSniff.Value))
				this.SniffClusterState();
		}

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

		private bool SniffOnFaultDiscoveredMoreNodes<T>(TransportRequestState<T> requestState, int retried, ElasticsearchResponse<Stream> streamResponse)
		{
			if (retried != 0 || streamResponse.SuccessOrKnownError) return false;
			SniffOnConnectionFailure(requestState, retried);
			return this.GetMaximumRetries(requestState.RequestConfiguration) > 0;
		}

		private void SetErrorDiagnosticsAndPatchSuccess<T>(TransportRequestState<T> requestState,
			ElasticsearchServerError error, ElasticsearchResponse<T> typedResponse, ElasticsearchResponse<Stream> streamResponse)
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


		private Uri GetNextBaseUri<T>(TransportRequestState<T> requestState, out int initialSeed, out bool shouldPingHint)
		{
			if (requestState.RequestConfiguration != null && requestState.RequestConfiguration.ForceNode != null)
			{
				initialSeed = 0; 
				shouldPingHint = false;
				return requestState.RequestConfiguration.ForceNode;
			}
			var baseUri = this._connectionPool.GetNext(requestState.Seed, out initialSeed, out shouldPingHint);
			return baseUri;
		}

		private Uri CreateUriToPath(Uri baseUri, string path)
		{
			var s = this.Settings;
			if (s.QueryStringParameters != null)
			{
				var tempUri = new Uri(baseUri, path);
				var qs = s.QueryStringParameters.ToQueryString(tempUri.Query.IsNullOrEmpty() ? "?" : "&");
				path += qs;
			}
			var uri = path.IsNullOrEmpty() ? baseUri : new Uri(baseUri, path);
			return uri.Purify();
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

		private void SetStringResult(ElasticsearchResponse<string> response, byte[] rawResponse)
		{
			response.Response = rawResponse.Utf8String();
		}

		private void SetByteResult(ElasticsearchResponse<byte[]> response, byte[] rawResponse)
		{
			response.Response = rawResponse;
		}
		
		private void SniffOnConnectionFailure<T>(TransportRequestState<T> requestState, int retried)
		{
			if (requestState.SniffedOnConnectionFailure 
				|| SniffingDisabled(requestState.RequestConfiguration)
				|| !this.ConfigurationValues.SniffsOnConnectionFault 
				|| retried != 0) return;

			this.SniffClusterState();
			requestState.SniffedOnConnectionFailure = true;
		}
		
		/* SYNC *** */
		
	
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

		private ElasticsearchResponse<T> DoRequest<T>(TransportRequestState<T> requestState, int retried = 0)
		{
			if (!SniffingDisabled(requestState.RequestConfiguration))
				SniffIfInformationIsTooOld(retried);

			var aliveResponse = false;

			int initialSeed; bool shouldPingHint;
			var baseUri = GetNextBaseUri(requestState, out initialSeed, out shouldPingHint);
			requestState.Seed = initialSeed;

			var uri = CreateUriToPath(baseUri, requestState.Path);
			bool seenError = false;
			var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);
			try
			{
				if (shouldPingHint 
					&& !this.ConfigurationValues.DisablePings
					&& (requestState.RequestConfiguration == null
						|| !requestState.RequestConfiguration.DisablePing.GetValueOrDefault(false))
					)
					this.Ping(baseUri);

				var streamResponse = _doRequest(requestState.Method, uri, requestState.PostData, requestState.RequestConfiguration);
				if (streamResponse.SuccessOrKnownError 
					|| (
						maxRetries == 0 && retried == 0 && !SniffOnFaultDiscoveredMoreNodes(requestState, retried, streamResponse))
					)
				{
					var error = ThrowOrGetErrorFromStreamResponse(requestState, streamResponse);

					var typedResponse = this.StreamToTypedResponse<T>(streamResponse, requestState.DeserializationState);
					typedResponse.NumberOfRetries = retried;
					this.SetErrorDiagnosticsAndPatchSuccess(requestState, error, typedResponse, streamResponse);
					aliveResponse = typedResponse.SuccessOrKnownError;
					return typedResponse;
				}
			}
			catch (Exception e)
			{
				if (maxRetries == 0 && retried == 0)
					throw;
				seenError = true;
				return RetryRequest<T>(requestState, baseUri, retried, e);
			}
			finally
			{
				//make sure we always call markalive on the uri if the connection was succesful
				if (!seenError && aliveResponse)
					this._connectionPool.MarkAlive(baseUri);
			}
			return RetryRequest<T>(requestState, baseUri, retried);
		}

		private ElasticsearchResponse<T> RetryRequest<T>(TransportRequestState<T> requestState, Uri baseUri, int retried, Exception e = null)
		{
			var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);
			var exceptionMessage = MaxRetryExceptionMessage.F(requestState.Method, requestState.Path.IsNullOrEmpty() ? "/" : requestState.Path, retried);

			this._connectionPool.MarkDead(baseUri, this.ConfigurationValues.DeadTimeout, this.ConfigurationValues.MaxDeadTimeout);

			SniffOnConnectionFailure(requestState, retried);

			if (retried >= maxRetries) throw new MaxRetryException(exceptionMessage, e);

			return this.DoRequest<T>(requestState, ++retried);
		}

		private ElasticsearchResponse<Stream> _doRequest(string method, Uri uri, byte[] postData, IRequestConfiguration requestSpecificConfig)
		{
			switch (method.ToLowerInvariant())
			{
				case "post": return this.Connection.PostSync(uri, postData, requestSpecificConfig);
				case "put": return this.Connection.PutSync(uri, postData, requestSpecificConfig);
				case "head": return this.Connection.HeadSync(uri, requestSpecificConfig);
				case "get": return this.Connection.GetSync(uri, requestSpecificConfig);
				case "delete":
					return postData == null || postData.Length == 0
						? this.Connection.DeleteSync(uri, requestSpecificConfig)
						: this.Connection.DeleteSync(uri, postData, requestSpecificConfig);
			}
			throw new Exception("Unknown HTTP method " + method);
		}

		/* ASYNC *** */
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
							tcs.SetException(t.Exception.Flatten());
						else
						{
							tcs.SetResult(t.Result);
						}

						requestState.SetResult(t.Result);

						return tcs.Task;
					}).Unwrap();
			}
		}

		private Task<ElasticsearchResponse<T>> DoRequestAsync<T>(TransportRequestState<T> requestState, int retried = 0)
		{
			SniffIfInformationIsTooOld(retried);
		
			int initialSeed; bool shouldPingHint;
			var baseUri = this.GetNextBaseUri(requestState, out initialSeed, out shouldPingHint);
			requestState.Seed = initialSeed;

			var uri = CreateUriToPath(baseUri, requestState.Path);
			
			if (shouldPingHint && !this.ConfigurationValues.DisablePings)
			{
				return this.PingAsync(baseUri)
					.ContinueWith(t =>
					{
						if (t.IsCompleted)
							return this._doRequestAsyncOrRetry(requestState, retried, uri, baseUri);

						return this.RetryRequestAsync(requestState, baseUri, retried, t.Exception);
					}).Unwrap();
			}
			
			return _doRequestAsyncOrRetry(requestState, retried, uri, baseUri);
		}

		private Task<ElasticsearchResponse<T>>  _doRequestAsyncOrRetry<T>(
			TransportRequestState<T> requestState, int retried, Uri uri, Uri baseUri)
		{
			return
				_doRequestAsync(requestState.Method, uri, requestState.PostData, requestState.RequestConfiguration).ContinueWith(t =>
				{
					if (t.IsCanceled)
						return null;
					var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);
					if (t.IsFaulted)
					{
						if (maxRetries == 0 && retried == 0)
							throw t.Exception;
						return this.RetryRequestAsync<T>(requestState, baseUri, retried, t.Exception);				        
					}
					
					if (t.Result.SuccessOrKnownError 
					|| (
						maxRetries == 0 && retried == 0 && !SniffOnFaultDiscoveredMoreNodes(requestState, retried, t.Result))
					)
					{
						var error = ThrowOrGetErrorFromStreamResponse(requestState, t.Result);
						return this.StreamToTypedResponseAsync<T>(t.Result, requestState.DeserializationState)
							.ContinueWith(tt =>
							{
								tt.Result.NumberOfRetries = retried;
								this.SetErrorDiagnosticsAndPatchSuccess(requestState, error, tt.Result, t.Result);
								if (tt.Result.SuccessOrKnownError)
									this._connectionPool.MarkAlive(baseUri);
								return tt;
							}).Unwrap();
					}
					return this.RetryRequestAsync<T>(requestState, baseUri, retried);
				}).Unwrap();
		}

		private Task<ElasticsearchResponse<T>> RetryRequestAsync<T>(TransportRequestState<T> requestState, Uri baseUri, int retried, Exception e = null)
		{
			var maxRetries = this.GetMaximumRetries(requestState.RequestConfiguration);
			var exceptionMessage = MaxRetryExceptionMessage.F(requestState.Method, requestState.Path, retried);

			this._connectionPool.MarkDead(baseUri, this.ConfigurationValues.DeadTimeout, this.ConfigurationValues.MaxDeadTimeout);

			this.SniffOnConnectionFailure(requestState, retried);

			if (retried < maxRetries)
				return this.DoRequestAsync<T>(requestState, ++retried);
			
			throw new MaxRetryException(exceptionMessage, e);
		}

		private Task<ElasticsearchResponse<Stream>> _doRequestAsync(string method, Uri uri, byte[] postData, IRequestConfiguration requestSpecificConfig)
		{
			switch (method.ToLowerInvariant())
			{
				case "head": return this.Connection.Head(uri, requestSpecificConfig);
				case "get": return this.Connection.Get(uri, requestSpecificConfig);
				case "post": return this.Connection.Post(uri, postData, requestSpecificConfig);
				case "put": return this.Connection.Put(uri, postData, requestSpecificConfig);
				case "delete":
					return postData == null || postData.Length == 0
						? this.Connection.Delete(uri, requestSpecificConfig)
						: this.Connection.Delete(uri, postData, requestSpecificConfig);
			}
			throw new Exception("Unknown HTTP method " + method);
		}


		private ElasticsearchServerError ThrowOrGetErrorFromStreamResponse<T>(
			TransportRequestState<T> requestState,
			ElasticsearchResponse<Stream> streamResponse)
		{
			if ((streamResponse.Success || requestState.RequestConfiguration != null) &&
			    (streamResponse.Success || requestState.RequestConfiguration == null ||
			     requestState.RequestConfiguration.AllowedStatusCodes.Any(i => i == streamResponse.HttpStatusCode)))
				return null;
			
			ElasticsearchServerError error = null;
			
			var type = typeof(T);
			if (typeof(Stream).IsAssignableFrom(typeof(T)) || typeof(T) == typeof(VoidResponse))
				return null;

			if (streamResponse.Response != null && !(this.Settings.KeepRawResponse || this.TypeOfResponseCopiesDirectly<T>()))
			{
				var e =	this.Serializer.Deserialize<OneToOneServerException>(streamResponse.Response);
				error = ElasticsearchServerError.Create(e);
			}
			else if (streamResponse.Response != null)
			{
				var ms = new MemoryStream();
				streamResponse.Response.CopyTo(ms);
				ms.Position = 0;
				streamResponse.ResponseRaw = this.Settings.KeepRawResponse ? ms.ToArray() : null;
				var e =this.Serializer.Deserialize<OneToOneServerException>(ms);
				error = ElasticsearchServerError.Create(e);
				ms.Position = 0;
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

		private bool TypeOfResponseCopiesDirectly<T>()
		{
			var type = typeof(T);
			return type == typeof(string) || type == typeof(byte[]) || typeof(Stream).IsAssignableFrom(typeof(T));
		}

		private ElasticsearchResponse<T> StreamToTypedResponse<T>(
			ElasticsearchResponse<Stream> streamResponse, 
			Func<IElasticsearchResponse, Stream, object> deserializationState
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
					return this._deserializeToResponse(cs, streamResponse.Response, deserializationState);


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
				return this._deserializeToResponse(cs, memoryStream, deserializationState);
			}
		}

		private Task<ElasticsearchResponse<T>> StreamToTypedResponseAsync<T>(ElasticsearchResponse<Stream> streamResponse,  object deserializationState)
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
				return _deserializeAsyncToResponse(streamResponse.Response, deserializationState, cs);

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

					return _deserializeAsyncToResponse(readStream, deserializationState, cs);
				})
				.Unwrap();

		}
		
		private ElasticsearchResponse<T> _deserializeToResponse<T>(
			ElasticsearchResponse<T> typedResponse,
			Stream responseStream, 
			Func<IElasticsearchResponse, Stream, object> deserializationState)
		{
			if (responseStream == null
				|| (!typedResponse.HttpStatusCode.HasValue)
				|| ((typedResponse.HttpStatusCode.Value < 200 || typedResponse.HttpStatusCode >= 300) && typedResponse.HttpStatusCode != 404)
				)
				return typedResponse;

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

		private Task<ElasticsearchResponse<T>> _deserializeAsyncToResponse<T>(Stream response, object deserializationState, ElasticsearchResponse<T> cs)
		{
			var tcs = new TaskCompletionSource<ElasticsearchResponse<T>>();
			if (response == null
				|| (!cs.HttpStatusCode.HasValue)
				|| ((cs.HttpStatusCode.Value < 200 || cs.HttpStatusCode >= 300) && cs.HttpStatusCode != 404)
				)
			{
				tcs.SetResult(cs);
				return tcs.Task;
			}
			var customConverter = deserializationState as Func<IElasticsearchResponse, Stream, T>;
			if (customConverter != null)
			{
				using (response)
				{
					var t = customConverter(cs, response);
					cs.Response = t;
					tcs.SetResult(cs);
					return tcs.Task;
				}
			}
			return this.Serializer.DeserializeAsync<T>(response)
				.ContinueWith(t =>
				{
					cs.Response = t.Result;
					return cs;
				});
		}

		const int BUFFER_SIZE = 4096;
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

	}
}