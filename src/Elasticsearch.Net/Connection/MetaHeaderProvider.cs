// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Produces the meta header when this functionality is enabled in the <see cref="ConnectionConfiguration"/>.
	/// </summary>
	/// <typeparam name="T">A <see cref="Type"/> belonging to the assembly from which to attempt to load
	/// client version information.</typeparam>
	internal sealed class MetaHeaderProvider<T> : IHeaderProvider
	{
		private static readonly Regex versionRegex = new Regex(@"(\d+\.)(\d+\.)(\d)");

		private const string MetaHeaderName = "x-elastic-client-meta";
		private const string EmptyVersion = "0.0.0";
		private const char _separator = ',';

		private static readonly Lazy<string> _clientVersion = new Lazy<string>(() =>
		{
			try
			{
				var productVersion = FileVersionInfo.GetVersionInfo(typeof(T).GetTypeInfo().Assembly.Location)?.ProductVersion ?? EmptyVersion;

				if (productVersion == EmptyVersion)
					productVersion = Assembly.GetAssembly(typeof(T)).GetName().Version.ToString();

				var match = versionRegex.Match(productVersion);

				return match.Success ? match.Value : EmptyVersion;
			}
			catch
			{
				// ignore failures and fall through
			}

			return EmptyVersion;
		});

		private static readonly Lazy<string> _runtimeVersion = new Lazy<string>(() =>
		{
			try
			{
				return RuntimeVersion.GetVersion()?.ToString() ?? EmptyVersion;
			}
			catch
			{
				// ignore failures and fall through
			}

			return EmptyVersion;
		});

		public string HeaderName => MetaHeaderName;

		public string ProduceHeaderValue(RequestData requestData)
		{
			try
			{
				if (requestData.DisableMetaHeader)
					return null;

				var clientVersion = _clientVersion.Value;
				var runtimeVersion = _runtimeVersion.Value;

				var sb = StringBuilderCache.Acquire(64);

				sb.Append("es=").Append(clientVersion).Append(_separator);
				sb.Append("a=").Append(requestData.IsAsync ? "1" : "0").Append(_separator);
				sb.Append("net=").Append(runtimeVersion).Append(_separator);

				if (!string.IsNullOrEmpty(requestData.HttpClientIdentifier))
					sb.Append(requestData.HttpClientIdentifier).Append("=").Append(runtimeVersion).Append(_separator);

				foreach (var requestMetaData in requestData.RequestMetaData ?? EmptyReadOnly<string, string>.Dictionary)
				{
					if (requestMetaData.Key == RequestMetaData.HelperKey)
						sb.Append(requestMetaData.Key).Append("=").Append(requestMetaData.Value).Append(_separator);
				}

				sb.Remove(sb.Length - 1, 1); // remove trailing comma

				return StringBuilderCache.GetStringAndRelease(sb);
			}
			catch
			{
				// don't fail the application just because we cannot create this optional header
			}

			return string.Empty;
		}
	}
}
