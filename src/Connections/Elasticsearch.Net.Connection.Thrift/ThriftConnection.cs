using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Connection.Thrift.Protocol;
using Elasticsearch.Net.Connection.Thrift.Transport;

namespace Elasticsearch.Net.Connection.Thrift
{
	public class ThriftConnection : IConnection, IDisposable
	{
		private static readonly string[] _transports = { "thrift" };
		public string[] PreferedTransportOrder { get { return _transports; } }

		private readonly ConcurrentDictionary<Uri, ConcurrentQueue<Rest.Client>> _clients =
			new ConcurrentDictionary<Uri, ConcurrentQueue<Rest.Client>>();
		private readonly Semaphore _resourceLock;
		private readonly int _timeout;
		private readonly int _poolSize;
		private bool _disposed;
		private readonly IConnectionConfigurationValues _connectionSettings;
		private readonly TProtocolFactory _protocolFactory;
		private readonly int _maximumConnections;

		public ThriftConnection(IConnectionConfigurationValues connectionSettings, TProtocolFactory protocolFactory = null)
		{
			this._connectionSettings = connectionSettings;
			this._protocolFactory = protocolFactory;
			this._timeout = connectionSettings.Timeout;

			this._maximumConnections = this._connectionSettings.MaximumAsyncConnections;
			if (this._maximumConnections > 0)
				this._resourceLock = new Semaphore(this._maximumConnections, this._maximumConnections);

		}

		#region IConnection Members

		public Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.GET;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, requestConfiguration);
			});
		}

		public Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.HEAD;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, requestConfiguration);
			});
		}

		public ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.GET;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, requestConfiguration);
		}

		public ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.HEAD;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, requestConfiguration);
		}

		public Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.POST;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, requestConfiguration);
			});
		}
		public Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.PUT;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, requestConfiguration);
			});
		}
		public Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, requestConfiguration);
			});
		}

		public ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.POST;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, requestConfiguration);
		}
		public ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.PUT;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, requestConfiguration);
		}
		public Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ElasticsearchResponse<Stream>>(() =>
			{
				return this.Execute(restRequest, requestConfiguration);
			});
		}

		public ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, requestConfiguration);
		}
		public ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConfiguration requestConfiguration = null)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = uri;

			restRequest.Body = data;
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest, requestConfiguration);
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

			foreach (var c in this._clients.SelectMany(c => c.Value))
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

		private Rest.Client CreateClient(Uri uri, ConcurrentQueue<Rest.Client> queue)
		{
			var host = uri.Host;
			var port = uri.Port;
			var tsocket = new TSocket(host, port, this._connectionSettings.Timeout);
			var transport = new TBufferedTransport(tsocket, 1024);
			var protocol = _protocolFactory == null ? new TBinaryProtocol(transport) : _protocolFactory.GetProtocol(transport);

			var client = new Rest.Client(protocol);
			tsocket.ConnectTimeout = this._connectionSettings.PingTimeout.GetValueOrDefault(200);
			tsocket.Timeout = this._connectionSettings.Timeout;
			tsocket.TcpClient.SendTimeout = this._connectionSettings.Timeout;
			tsocket.TcpClient.ReceiveTimeout = this._connectionSettings.Timeout;
			tsocket.TcpClient.NoDelay = true;
			queue.Enqueue(client);
			return client;
		}

		private Uri GetBaseBaseUri(Uri uri)
		{
			var path = uri.ToString();
			var baseUri = new Uri(string.Format("{0}://{1}:{2}", uri.Scheme, uri.Host, uri.Port));
			return baseUri;
		}

		private void EnqueueClient(Uri baseUri, Rest.Client connection)
		{
			ConcurrentQueue<Rest.Client> queue;
			if (!this._clients.TryGetValue(baseUri, out queue))
				return;
			queue.Enqueue(connection);
		}

		private Rest.Client GetClientForUri(Uri baseUri, out string errorMessage)
		{
			errorMessage = null;
			ConcurrentQueue<Rest.Client> queue;
			Rest.Client client;

			if (!this._clients.TryGetValue(baseUri, out queue))
			{
				//unknown endpoint lets set up some closed connections
				queue = new ConcurrentQueue<Rest.Client>();
				var max = Math.Max(10, this._maximumConnections);
				for (var i = 0; i < max; i++)
					CreateClient(baseUri, queue);
				this._clients.TryAdd(baseUri, queue);
			}
			if (!queue.TryDequeue(out client))
				errorMessage = string.Format("Could not dequeue connection for {0}", baseUri);
			return client;
		}

		private ElasticsearchResponse<Stream> Execute(RestRequest restRequest, object requestConfiguration)
		{
			var method = Enum.GetName(typeof(Method), restRequest.Method);
			var requestData = restRequest.Body;
			var uri = restRequest.Uri;
			var path = uri.ToString();

			if (this._resourceLock != null && !this._resourceLock.WaitOne(this._timeout))
			{
				var m = "Could not start the thrift operation before the timeout of " + this._timeout + "ms completed while waiting for the semaphore";
				return ElasticsearchResponse<Stream>.CreateError(this._connectionSettings, new TimeoutException(m), method, path, requestData);
			}
			try
			{
				var baseUri = this.GetBaseBaseUri(uri);
				string errorMessage;
				var client = this.GetClientForUri(baseUri, out errorMessage);
				if (client == null)
					return ElasticsearchResponse<Stream>.CreateError(
						this._connectionSettings, new Exception(errorMessage), method, path, requestData);
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
				catch (TTransportException)
				{
					client.InputProtocol.Transport.Close();
					throw;
				}
				finally
				{
					this.EnqueueClient(baseUri, client);
				}

			}
			catch (Exception e)
			{
				return ElasticsearchResponse<Stream>.CreateError(this._connectionSettings, e, method, path, requestData);
			}
			finally
			{
				if (this._resourceLock != null)
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