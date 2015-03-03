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
 *
 * Contains some contributions under the Thrift Software License.
 * Please see doc/old-thrift-license.txt in the Thrift distribution for
 * details.
 */

using System;
using System.Net.Sockets;
using System.Threading;

namespace Elasticsearch.Net.Connection.Thrift.Transport
{
	public class TSocket : TStreamTransport
	{
		private readonly string host;
		private readonly int port;
		private TcpClient client;
		private int timeout;

		private bool isConnectionSuccessful = false;
		private Exception socketexception;
		private ManualResetEvent timeoutObject = new ManualResetEvent(false);

		public TcpClient Connect(TcpClient _client)
		{
			timeoutObject.Reset();
			socketexception = null;

			client.BeginConnect(host, port, new AsyncCallback(CallBackMethod), client);

			if (timeoutObject.WaitOne(timeout, false))
			{
				if (isConnectionSuccessful)
				{
					return client;
				}

				throw socketexception ?? new Exception("Socket exception should not be null.");
			}

			client.Close();
			throw new TimeoutException("TimeOut Exception");
		}

		private void CallBackMethod(IAsyncResult asyncresult)
		{
			try
			{
				isConnectionSuccessful = false;
				var tcpclient = asyncresult.AsyncState as TcpClient;

				if (tcpclient != null && tcpclient.Client != null)
				{
					tcpclient.EndConnect(asyncresult);
					isConnectionSuccessful = true;
				}
			}
			catch (Exception ex)
			{
				isConnectionSuccessful = false;
				socketexception = ex;
			}
			finally
			{
				timeoutObject.Set();
			}
		}

		public TSocket(TcpClient client)
		{
			this.client = client;

			if (IsOpen)
			{
				inputStream = client.GetStream();
				outputStream = client.GetStream();
			}
		}

		public TSocket(string host, int port)
			: this(host, port, 0)
		{
		}

		public TSocket(string host, int port, int timeout)
		{
			this.host = host;
			this.port = port;
			this.timeout = timeout;

			InitSocket();
		}

		public int ConnectTimeout { get; set; }

		public int Timeout
		{
			set { client.ReceiveTimeout = client.SendTimeout = timeout = value; }
		}

		public TcpClient TcpClient
		{
			get { return client; }
		}

		public string Host
		{
			get { return host; }
		}

		public int Port
		{
			get { return port; }
		}

		public override bool IsOpen
		{
			get
			{
				if (client == null)
				{
					return false;
				}

				return client.Connected;
			}
		}

		private void InitSocket()
		{
			client = new TcpClient();
			client.ReceiveTimeout = client.SendTimeout = timeout;
		}

		public override void Open()
		{
			if (IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.AlreadyOpen, "Socket already connected");
			}

			if (String.IsNullOrEmpty(host))
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open null host");
			}

			if (port <= 0)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open without port");
			}

			if (client == null)
			{
				InitSocket();
			}

			client = Connect(client);

			inputStream = client.GetStream();
			outputStream = client.GetStream();
		}

		public override void Close()
		{
			base.Close();
			if (client != null)
			{
				client.Close();
				client = null;
			}
		}
	}
}