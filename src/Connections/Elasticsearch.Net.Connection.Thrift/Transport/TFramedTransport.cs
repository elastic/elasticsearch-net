/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements. See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership. The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Elasticsearch.Net.Connection.Thrift.Transport
{
	public class TFramedTransport : TTransport
	{
		public static readonly int FrameHeaderLength = 4;

		private readonly IPEndPoint remoteEndPoint;
		private readonly TSocketV2 socket;

		private MemoryStream readBuffer;
		private MemoryStream writeBuffer = new MemoryStream(1024);

		public TFramedTransport(string host, int port, TSocketSettings socketSettings)
		{
			if (string.IsNullOrEmpty(host))
				throw new ArgumentNullException("host");
			if (socketSettings == null)
				throw new ArgumentNullException("socketSettings");

			if (host.ToLowerInvariant() == "localhost")
			{
				host = "127.0.0.1";
			}
			remoteEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
			socket = new TSocketV2(socketSettings);
		}

		public override bool IsOpen
		{
			get { return socket.Connected; }
		}

		public override void Open()
		{
			socket.Connect(remoteEndPoint);
		}

		public override void Close()
		{
			socket.Close();
		}

		public override int Read(byte[] buf, int off, int len)
		{
			if (readBuffer != null)
			{
				int got = readBuffer.Read(buf, off, len);
				if (got > 0)
					return got;
			}

			// Read another frame of data
			ReadFrame();
			return readBuffer.Read(buf, off, len);
		}

		private void ReadFrame()
		{
			readBuffer = socket.ReadFrame();
		}

		public override void Write(byte[] buf, int off, int len)
		{
			writeBuffer.Write(buf, off, len);
		}

		public override void Flush()
		{
			var frame = new byte[FrameHeaderLength + writeBuffer.Length];
			byte[] data = writeBuffer.GetBuffer();
			var dataLength = (int) writeBuffer.Length;

			writeBuffer = new MemoryStream(writeBuffer.Capacity);

			frame[0] = (byte) (0xff & (dataLength >> 24));
			frame[1] = (byte) (0xff & (dataLength >> 16));
			frame[2] = (byte) (0xff & (dataLength >> 8));
			frame[3] = (byte) (0xff & (dataLength));
			Array.Copy(data, 0, frame, FrameHeaderLength, dataLength);

			socket.Send(frame, frame.Length, SocketFlags.None);
		}
	}

//	public class TFramedTransport : TTransport
//	{
//		protected TTransport transport = null;
//		protected MemoryStream writeBuffer = new MemoryStream(1024);
//		protected MemoryStream readBuffer = null;
//
//		public class Factory : TTransportFactory
//		{
//			public override TTransport GetTransport(TTransport trans)
//			{
//				return new TFramedTransport(trans);
//			}
//		}
//
//		public TFramedTransport(TTransport transport)
//		{
//			this.transport = transport;
//		}
//
//		public override void Open()
//		{
//			transport.Open();
//		}
//
//		public override bool IsOpen
//		{
//			get
//			{
//				return transport.IsOpen;
//			}
//		}
//
//		public override void Close()
//		{
//			transport.Close();
//		}
//
//		public override int Read(byte[] buf, int off, int len)
//		{
//			if (readBuffer != null)
//			{
//				int got = readBuffer.Read(buf, off, len);
//				if (got > 0)
//				{
//					return got;
//				}
//			}
//
	// Read another frame of data
//			ReadFrame();
//
//			return readBuffer.Read(buf, off, len);
//		}
//
//		private void ReadFrame()
//		{
//			byte[] i32rd = new byte[4];
//			transport.ReadAll(i32rd, 0, 4);
//			int size =
//				((i32rd[0] & 0xff) << 24) |
//				((i32rd[1] & 0xff) << 16) |
//				((i32rd[2] & 0xff) <<  8) |
//				((i32rd[3] & 0xff));
//
//			byte[] buff = new byte[size];
//			transport.ReadAll(buff, 0, size);
//			readBuffer = new MemoryStream(buff);
//		}
//
//		public override void Write(byte[] buf, int off, int len)
//		{
//			writeBuffer.Write(buf, off, len);
//		}
//
//		public override void Flush()
//		{
//			byte[] buf = writeBuffer.GetBuffer();
//			int len = (int)writeBuffer.Length;
//			writeBuffer = new MemoryStream(writeBuffer.Capacity);
//
//			byte[] i32out = new byte[4];
//			i32out[0] = (byte)(0xff & (len >> 24));
//			i32out[1] = (byte)(0xff & (len >> 16));
//			i32out[2] = (byte)(0xff & (len >> 8));
//			i32out[3] = (byte)(0xff & (len));
//			transport.Write(i32out, 0, 4);
//			transport.Write(buf, 0, len);
//			transport.Flush();
//		}
//	}
}