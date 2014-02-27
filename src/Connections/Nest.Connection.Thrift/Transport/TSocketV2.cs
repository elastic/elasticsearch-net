using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Elasticsearch.Net.Connection.Thrift.Transport
{
	internal class TSocketV2 : Socket
	{
		public static readonly int FrameHeaderLength = 4;
		private readonly AsyncCallback buildSocketCallback;

		private readonly ManualResetEvent connectEvent = new ManualResetEvent(false);
		private readonly AsyncCallback receiveCallback;
		private readonly AutoResetEvent receiveEvent = new AutoResetEvent(false);
		private readonly TSocketSettings settings;

		private MemoryStream frameBuffer;
		private byte[] receiveBuffer;
		private MemoryStream receivedFrame;

		public TSocketV2(TSocketSettings settings)
			: base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
		{
			SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, settings.SendBufferSize);
			SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, settings.ReceiveBufferSize);
			SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, settings.SendTimeout);
			SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, settings.ReceiveTimeout);

			this.settings = settings;
			buildSocketCallback = new AsyncCallback(BuildSocketCallback);
			receiveCallback = new AsyncCallback(ReceiveCallback);
			LastError = SocketError.Success;
		}

		public SocketError LastError { get; private set; }

		public void Connect(IPEndPoint remoteEndPoint)
		{
			var state = new SocketConnectState(this);
			BeginConnect(remoteEndPoint, buildSocketCallback, state);

			if (!connectEvent.WaitOne(settings.ConnectTimeout, false))
			{
				connectEvent.Reset();
				throw new SocketException((int) SocketError.TimedOut);
			}

			if (state.Exception != null)
				throw state.Exception;

			connectEvent.Reset();
			BeginReceive(GetReceiveBuffer(), 0, settings.ReceiveBufferSize, SocketFlags.None, receiveCallback, null);
		}

		private byte[] GetReceiveBuffer()
		{
			if (receiveBuffer == null || receiveBuffer.Length != settings.ReceiveBufferSize)
				receiveBuffer = new byte[settings.ReceiveBufferSize];
			return receiveBuffer;
		}

		private MemoryStream GetFrameBuffer()
		{
			if (frameBuffer == null)
				frameBuffer = new MemoryStream(4*settings.ReceiveBufferSize);
			else
				frameBuffer.Seek(0, SeekOrigin.Begin);
			return frameBuffer;
		}

		private void BuildSocketCallback(IAsyncResult ar)
		{
			var state = ar.AsyncState as SocketConnectState;
			try
			{
				state.Socket.EndConnect(ar);
			}
			catch (SocketException ex)
			{
				state.Exception = ex;
			}
			connectEvent.Set();
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			SocketError error = SocketError.Success;
			int received = 0;

			#region EndReceive

			try
			{
				received = EndReceive(ar, out error);
			}
			catch (ObjectDisposedException)
			{
				return;
			}
			catch (SocketException socketException)
			{
				Debug.WriteLine(string.Format("SocketException {0} during EndReceive from {1}",
				                              socketException.Message, RemoteEndPoint));
				LastError = socketException.SocketErrorCode;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Exception {0} during EndReceive from {1}",
				                              ex.Message, RemoteEndPoint));
			}

			#endregion

			#region OperationAborted

			if (error == SocketError.OperationAborted)
			{
				Debug.WriteLine("Operation aborted on thread " + Thread.CurrentThread.ManagedThreadId);
				try
				{
					BeginReceive(GetReceiveBuffer(), 0, settings.ReceiveBufferSize, SocketFlags.None, receiveCallback, null);
				}
				catch (ObjectDisposedException)
				{
				}
				catch (SocketException sex)
				{
					LastError = sex.SocketErrorCode;
				}
				return;
			}

			#endregion

			if (error == SocketError.Success)
			{
				if (received == 0)
				{
					PostError(SocketError.ConnectionReset);
					return;
				}

				try
				{
					while (received < FrameHeaderLength)
						received += Receive(receiveBuffer, received, receiveBuffer.Length - received, SocketFlags.None);

					int frameLength = GetFrameLength(receiveBuffer);
					MemoryStream frameBuffer = GetFrameBuffer();
					frameBuffer.Write(receiveBuffer, FrameHeaderLength, received - FrameHeaderLength);

					while (frameBuffer.Position < frameLength)
					{
						received = Receive(receiveBuffer);
						frameBuffer.Write(receiveBuffer, 0, received);
					}

					MemoryStream frame = GetFrame(frameBuffer, frameLength);

					BeginReceive(GetReceiveBuffer(), 0, settings.ReceiveBufferSize, SocketFlags.None, receiveCallback, null);

					PostReply(frame);
				}
				catch (ObjectDisposedException)
				{
				}
				catch (SocketException socketException)
				{
					LastError = socketException.SocketErrorCode;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(string.Format("Exception {0} during Receive from {1}",
					                              ex.Message, RemoteEndPoint));
				}
			}
			else
			{
				PostError(error);
			}
		}

		private MemoryStream GetFrame(MemoryStream frameBuffer, int frameLength)
		{
			frameBuffer.Seek(0, SeekOrigin.Begin);
			var frame = new MemoryStream(frameLength);
			frame.Write(frameBuffer.GetBuffer(), 0, frameLength);
			frame.Seek(0, SeekOrigin.Begin);
			return frame;
		}

		private int GetFrameLength(byte[] buffer)
		{
			return (buffer[0] << 24)
			       | (buffer[1] << 16)
			       | (buffer[2] << 8)
			       | (buffer[3]);
		}

		private void PostError(SocketError error)
		{
			LastError = error;
			receiveEvent.Set();
		}

		private void PostReply(MemoryStream frame)
		{
			receivedFrame = frame;
			receiveEvent.Set();
		}

		public MemoryStream ReadFrame()
		{
			if (receiveEvent.WaitOne(settings.ReceiveTimeout, false))
			{
				if (LastError != SocketError.Success)
					throw new SocketException((int) LastError);
				MemoryStream frame = receivedFrame;
				receivedFrame = null;
				return frame;
			}

			receivedFrame = null;
			throw new SocketException((int) SocketError.TimedOut);
		}
	}
}