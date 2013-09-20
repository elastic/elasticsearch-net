using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using Nest.Domain.Connection;

namespace ProtocolLoadTest
{
	public class AsyncRequestOperation : TaskCompletionSource<ConnectionStatus>, IDisposable
	{
		private readonly HttpWebRequest m_request;
		private readonly string m_requestData;
		private readonly IConnectionSettings m_connectionSettings;
		private ConnectionStatusTracer m_tracer;
		private WebResponse m_response;
		private Stream m_responseStream;

		public AsyncRequestOperation(HttpWebRequest request, string requestData, IConnectionSettings connectionSettings, ConnectionStatusTracer tracer)
		{
			m_request = request;
			m_requestData = requestData;
			m_connectionSettings = connectionSettings;
			m_tracer = tracer;
			Start();
		}

		private void Start()
		{
			if (this.m_requestData != null)
				WriteRequestDataAsync();
			else
				GetResponseAsync();
		}

		private void WriteRequestDataAsync()
		{
			this.m_request.BeginGetRequestStream(this.Monitor(ar =>
			{
				var r = this.m_request.EndGetRequestStream(ar);
				var buffer = Encoding.UTF8.GetBytes(this.m_requestData);
				r.BeginWrite(buffer, 0, buffer.Length, this.Monitor(writeIar =>
				{
					r.EndWrite(writeIar);
					GetResponseAsync();
				}), null);
			}), null);
		}

		private void GetResponseAsync()
		{
			this.m_request.BeginGetResponse(this.Monitor(iarResponse =>
			{
				m_response = m_request.EndGetResponse(iarResponse);
				m_responseStream = m_response.GetResponseStream();

				var buffer = new byte[8192];
				var result = new MemoryStream(buffer.Length);
				ReadResponseStreamAsync(this.m_responseStream, buffer, result);

			}), null);
		}

		private void ReadResponseStreamAsync(Stream stream, byte[] buffer, MemoryStream result)
		{
			stream.BeginRead(buffer, 0, buffer.Length, this.Monitor(iar =>
			{
				var bytes = stream.EndRead(iar);
				if (bytes == 0)
				{
					Done(result);
					return;
				}

				result.Write(buffer, 0, bytes);
				ReadResponseStreamAsync(stream, buffer, result);

			}), null);
		}

		private void Done(ConnectionStatus connectionStatus)
		{
			m_tracer.SetResult(connectionStatus);
			TrySetResult(connectionStatus);
			Dispose();
		}

		private void Done(Stream result)
		{
			result.Position = 0;
			var reader = new StreamReader(result);
			Done(new ConnectionStatus(this.m_connectionSettings, reader.ReadToEnd())
			{
				Request = this.m_requestData,
				RequestUrl = this.m_request.RequestUri.ToString(),
				RequestMethod = this.m_request.Method
			});

		}

		private AsyncCallback Monitor(AsyncCallback callback)
		{
			return ar =>
			{
				try
				{
					callback(ar);
				}
				catch (WebException webException)
				{
					var connectionStatus = new ConnectionStatus(this.m_connectionSettings, webException)
					{
						Request = this.m_requestData,
						RequestUrl = this.m_request.RequestUri.ToString(),
						RequestMethod = this.m_request.Method
					};
					m_connectionSettings.ConnectionStatusHandler(connectionStatus);
					Done(connectionStatus);
				}
				catch (Exception e)
				{
					TrySetException(e);
					Dispose();
				}
			};
		}

		public void Dispose()
		{
			Dispose(ref m_response);
			Dispose(ref m_responseStream);
			Dispose(ref m_tracer);
		}

		private static void Dispose<T>(ref T disposable) where T : class, IDisposable
		{
			var d = Interlocked.Exchange(ref disposable, null);
			if (d != null)
				d.Dispose();
		}
	}
}