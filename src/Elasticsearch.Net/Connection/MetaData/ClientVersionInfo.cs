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
