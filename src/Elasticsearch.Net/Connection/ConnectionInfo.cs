/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
#if DOTNETCORE
using System.Net.Http;
#endif

namespace Elasticsearch.Net
{
	public static class ConnectionInfo
	{
		public static bool UsingCurlHandler
		{
			get
			{
#if !DOTNETCORE
				return false;
#else
				var curlHandlerExists = typeof(HttpClientHandler).Assembly.GetType("System.Net.Http.CurlHandler") != null;
				if (!curlHandlerExists)
					return false;

				var socketsHandlerExists = typeof(HttpClientHandler).Assembly.GetType("System.Net.Http.SocketsHttpHandler") != null;
				// running on a .NET core version with CurlHandler, before the existence of SocketsHttpHandler.
				// Must be using CurlHandler.
				if (!socketsHandlerExists)
					return true;

				if (AppContext.TryGetSwitch("System.Net.Http.UseSocketsHttpHandler", out var isEnabled))
					return !isEnabled;

				var environmentVariable =
					Environment.GetEnvironmentVariable("DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER");

				// SocketsHandler exists and no environment variable exists to disable it.
				// Must be using SocketsHandler and not CurlHandler
				if (environmentVariable == null)
					return false;

				return environmentVariable.Equals("false", StringComparison.OrdinalIgnoreCase) ||
					environmentVariable.Equals("0");
#endif
			}
		}
	}
}
