// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
