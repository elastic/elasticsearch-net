// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Elasticsearch.Net
{
	internal sealed class ClientVersionInfo : VersionInfo
	{
		private static readonly Regex VersionRegex = new Regex(@"(\d+\.)(\d+\.)(\d)");

		public static readonly ClientVersionInfo Empty = new ClientVersionInfo { Version = new Version(0, 0, 0), IsPrerelease = false };

		public static readonly ClientVersionInfo LowLevelClientVersionInfo = Create<IElasticLowLevelClient>();

		private ClientVersionInfo() { }

		public static ClientVersionInfo Create<T>()
		{
			var fullVersion = DetermineClientVersion(typeof(T));

			var clientVersion = new ClientVersionInfo();
			clientVersion.StoreVersion(fullVersion);
			return clientVersion;
		}

		private static string DetermineClientVersion(Type type)
		{
			try
			{
				var productVersion = FileVersionInfo.GetVersionInfo(type.GetTypeInfo().Assembly.Location)?.ProductVersion ?? EmptyVersion;

				if (productVersion == EmptyVersion)
					productVersion = Assembly.GetAssembly(type).GetName().Version.ToString();

				var match = VersionRegex.Match(productVersion);

				return match.Success ? match.Value : EmptyVersion;
			}
			catch
			{
				// ignore failures and fall through
			}

			return EmptyVersion;
		}
	}
}
