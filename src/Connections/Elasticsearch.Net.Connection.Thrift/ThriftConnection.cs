using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Thrift.Protocol;
using Elasticsearch.Net.Connection.Thrift.Transport;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.Connection.Thrift
{
	public class ThriftConnection : IConnection, IDisposable
	{
		private readonly ConcurrentDictionary<Uri, ConcurrentQueue<Rest.Client>> _clients =
			new ConcurrentDictionary<Uri, ConcurrentQueue<Rest.Client>>();
		private readonly Semaphore _resourceLock;
		private readonly int _timeout;
		private readonly int _poolSize;
		private bool _disposed;
		private readonly IConnectionConfigurationValues _connectionSettings;

		public ThriftConnection(IConnectionConfigurationValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this._timeout = connectionSettings.Timeout;
			var connectionPool = this._connectionSettings.ConnectionPool;
			var maximumConnections = Math.Max(1, connectionSettings.MaximumAsyncConnections);
			var maximumUrls = connectionPool.MaxRetries + 1;
			this._poolSize = maximumConnections;
			this._resourceLock = new Semaphore(_poolSize, _poolSize);

			int seed; int initialSeed = 0; bool shouldPingHint;

			for (var i = 0; i < maximumUrls; i++)
			{
				var uri = connectionPool.GetNext(initialSeed, out seed, out shouldPingHint);
				var queue = new ConcurrentQueue<Rest.Client>();
				for (var c = 0; c < maximumConnections; c++)
				{
					var host = uri.Host;
					var port = uri.Port;
					var tsocket = new TSocket(host, port);
					var transport = new TBufferedTransport(tsocket, 1024);
					var protocol = new TBinaryProtocol(transport);
					var client = new Rest.Client(protocol);
					tsocket.Timeout = this._connectionSettings.Timeout;
					tsocket.TcpClient.SendTimeout = this._connectionSettings.Timeout;
					tsocket.TcpClient.ReceiveTimeout = this._connectionSettings.Timeout;
					//tsocket.TcpClient.NoDelay = true;

					queue.Enqueue(client);

					initialSeed = seed;
				}
				_clients.TryAdd(uri, queue);
			}
		}

		#region IConnection Members

		public Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.GET;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, deserializationState);
			});
		}

		public Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.HEAD;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, deserializationState);
			});
		}

		public ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.GET;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, deserializationState);
		}

		public ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.HEAD;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, deserializationState);
		}

		public Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.POST;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, deserializationState);
			});
		}
		public Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.PUT;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, deserializationState);
			});
		}
		public Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, deserializationState);
			});
		}

		public ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.POST;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, deserializationState);
		}
		public ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.PUT;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, deserializationState);
		}
		public Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, deserializationState);
			});
		}

		public ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, deserializationState);
		}
		public ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConnectionConfiguration deserializationState = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, deserializationState);
		}


		#endregion

		#region IDisposable Members

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		#endregion

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			foreach (var c in this._clients.SelectMany(c=>c.Value))
			{
				if (c != null
					&& c.InputProtocol != null
					&& c.InputProtocol.Transport != null
					&& c.InputProtocol.Transport.IsOpen)
					c.InputProtocol.Transport.Close();
			}
			_disposed = true;
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="HttpConnection"/> is reclaimed by garbage collection.
		/// </summary>
		~ThriftConnection()
		{
			Dispose(false);
		}



		private ElasticsearchResponse<Stream> Execute(RestRequest restRequest, object deserializationState)
		{
			//RestResponse result = GetClient().execute(restRequest);
			//
			var method = Enum.GetName(typeof(Method), restRequest.Method);
			var uri = restRequest.Uri;
			var path = uri.ToString();
			var baseUri = new Uri(string.Format("{0}://{1}:{2}", uri.Scheme, uri.Host, uri.Port));
			var requestData = restRequest.Body;
			if (!this._resourceLock.WaitOne(this._timeout))
			{
				var m = "Could not start the thrift operation before the timeout of " + this._timeout + "ms completed while waiting for the semaphore";
				return ElasticsearchResponse<Stream>.CreateError(this._connectionSettings, new TimeoutException(m), method, path, requestData);
			}
			try
			{
				ConcurrentQueue<Rest.Client> queue;
				if (!this._clients.TryGetValue(baseUri,out queue))
				{
					var m = string.Format("Could dequeue a thrift client from internal socket pool of size {0}", this._poolSize);
					var status = ElasticsearchResponse<Stream>.CreateError(this._connectionSettings, new Exception(m), method, path, requestData);
					return status;
				}


				Rest.Client client;
				if (!queue.TryDequeue(out client))
				{
					var m = string.Format("Could dequeue a thrift client from internal socket pool of size {0}", this._poolSize);
					var status = ElasticsearchResponse<Stream>.CreateError(this._connectionSettings, new Exception(m), method, path, requestData);
					return status;
				}
				try
				{
					if (!client.InputProtocol.Transport.IsOpen)
						client.InputProtocol.Transport.Open();

					var result = client.execute(restRequest);
					if (result.Status == Status.OK || result.Status == Status.CREATED || result.Status == Status.ACCEPTED)
					{
						var response = ElasticsearchResponse<Stream>.Create(
							this._connectionSettings, (int)result.Status, method, path, requestData, new MemoryStream(result.Body ?? new byte[0]));
						return response;
					}
					else
					{
						var response = ElasticsearchResponse<Stream>.Create(
							this._connectionSettings, (int)result.Status, method, path, requestData, new MemoryStream(result.Body ?? new byte[0]));
						return response;
					}
				}
				catch (SocketException)
				{
					client.InputProtocol.Transport.Close();
					throw;
				}
				catch (IOException)
				{
					client.InputProtocol.Transport.Close();
					throw;
				}
				finally
				{
					//make sure we make the client available again.
					if (queue != null && client != null)
						queue.Enqueue(client);
				}

			}
			catch (Exception e)
			{
				return ElasticsearchResponse<Stream>.CreateError(this._connectionSettings, e, method, path, requestData);
			}
			finally
			{
				this._resourceLock.Release();
			}
		}

		public string DecodeStr(byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				return Encoding.UTF8.GetString(bytes);
			}
			return string.Empty;
		}
	}
}