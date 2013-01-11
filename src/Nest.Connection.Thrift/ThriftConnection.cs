using System;
using System.Collections.Generic;
using System.Text;
using Nest.Thrift;
using Thrift.Protocol;
using Thrift.Transport;
using Nest;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Nest.Thrift
{
	public class ThriftConnection : IConnection, IDisposable
	{
		private readonly ConcurrentQueue<Rest.Client> _clients = new ConcurrentQueue<Rest.Client>();
		private readonly Semaphore _resourceLock;
		private readonly int _timeout;
		private readonly int _poolSize;
		private bool _disposed;

		public ThriftConnection(IConnectionSettings connectionSettings)
		{
			this._timeout = connectionSettings.Timeout;
			this._poolSize = connectionSettings.MaximumAsyncConnections;

			this._resourceLock = new Semaphore(_poolSize, _poolSize);

			for (var i = 0; i <= connectionSettings.MaximumAsyncConnections; i++)
			{
				var tsocket = new TSocket(connectionSettings.Host, connectionSettings.Port);
				var transport = new TBufferedTransport(tsocket, 1024);
				var protocol = new TBinaryProtocol(transport);
				var client = new Rest.Client(protocol);
				_clients.Enqueue(client);
			}
		}

		#region IConnection Members

		public Task<ConnectionStatus> Get(string path)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.GET;
			restRequest.Uri = path;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ConnectionStatus>(() =>
			{
				return this.Execute(restRequest);
			});
		}
	
		public Task<ConnectionStatus> Head(string path)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.HEAD;
			restRequest.Uri = path;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ConnectionStatus>(()=> 
			{
				return this.Execute(restRequest);
			});
		}

		public ConnectionStatus GetSync(string path)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.GET;
			restRequest.Uri = path;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest);
		}

		public ConnectionStatus HeadSync(string path)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.HEAD;
			restRequest.Uri = path;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest);
		}

		public Task<ConnectionStatus> Post(string path, string data)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.POST;
			restRequest.Uri = path;

			if (!string.IsNullOrEmpty(data))
			{
				restRequest.Body = Encoding.UTF8.GetBytes(data);
			}
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ConnectionStatus>(() =>
			{
				return this.Execute(restRequest);
			});
		}
		public Task<ConnectionStatus> Put(string path, string data)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.PUT;
			restRequest.Uri = path;

			if (!string.IsNullOrEmpty(data))
			{
				restRequest.Body = Encoding.UTF8.GetBytes(data);
			}
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ConnectionStatus>(() =>
			{
				return this.Execute(restRequest);
			});
		}
		public Task<ConnectionStatus> Delete(string path, string data)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = path;

			if (!string.IsNullOrEmpty(data))
			{
				restRequest.Body = Encoding.UTF8.GetBytes(data);
			}
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ConnectionStatus>(() =>
			{
				return this.Execute(restRequest);
			});
		}

		public ConnectionStatus PostSync(string path, string data)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.POST;
			restRequest.Uri = path;

			if (!string.IsNullOrEmpty(data))
			{
				restRequest.Body = Encoding.UTF8.GetBytes(data);
			}
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest);
		}
		public ConnectionStatus PutSync(string path, string data)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.PUT;
			restRequest.Uri = path;

			if (!string.IsNullOrEmpty(data))
			{
				restRequest.Body = Encoding.UTF8.GetBytes(data);
			}
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest);
		}
		public Task<ConnectionStatus> Delete(string path)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = path;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return Task.Factory.StartNew<ConnectionStatus>(() =>
			{
				return this.Execute(restRequest);
			});
		}

		public ConnectionStatus DeleteSync(string path)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = path;

			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest);
		}
		public ConnectionStatus DeleteSync(string path, string data)
		{
			var restRequest = new RestRequest();
			restRequest.Method = Method.DELETE;
			restRequest.Uri = path;

			if (!string.IsNullOrEmpty(data))
			{
				restRequest.Body = Encoding.UTF8.GetBytes(data);
			}
			restRequest.Headers = new Dictionary<string, string>();
			restRequest.Headers.Add("Content-Type", "application/json");
			return this.Execute(restRequest);
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
			{
				return;
			}

			foreach (var c in this._clients)
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
		/// <see cref="Connection"/> is reclaimed by garbage collection.
		/// </summary>
		~ThriftConnection()
		{
			Dispose(false);
		}



		private ConnectionStatus Execute(RestRequest restRequest)
		{
			//RestResponse result = GetClient().execute(restRequest);
			//

			if (!this._resourceLock.WaitOne(this._timeout))
			{
				var m = "Could not start the thrift operation before the timeout of " + this._timeout + "ms completed while waiting for the semaphore";
				return new ConnectionStatus(new TimeoutException(m));
			}
			try
			{
				Rest.Client client = null;
				if (!this._clients.TryDequeue(out client))
				{
					var m = string.Format("Could dequeue a thrift client from internal socket pool of size {0}", this._poolSize);
					var status = new ConnectionStatus(new Exception(m));
					return status;
				}
				try
				{
					if (!client.InputProtocol.Transport.IsOpen)
						client.InputProtocol.Transport.Open();

					var result = client.execute(restRequest);
					return new ConnectionStatus(DecodeStr(result.Body));
				}
				catch
				{
					throw;
				}
				finally
				{
					//make sure we make the client available again.
					this._clients.Enqueue(client);
				}

			}
			catch (Exception e)
			{
				return new ConnectionStatus(e);
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