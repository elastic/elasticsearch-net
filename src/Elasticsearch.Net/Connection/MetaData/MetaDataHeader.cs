// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;

namespace Elasticsearch.Net
{
	internal sealed class MetaDataHeader
	{
		private const char _separator = ',';

		private readonly string _headerValue;

		public MetaDataHeader(VersionInfo version, string serviceIdentifier, bool isAsync)
		{
			ClientVersion = version.ToString();
			RuntimeVersion = new RuntimeVersionInfo().ToString();
			ServiceIdentifier = serviceIdentifier;

			// This code is expected to be called infrequently so we're not concerns with over optimising this

			_headerValue = new StringBuilder(64)
				.Append(serviceIdentifier).Append("=").Append(ClientVersion).Append(_separator)
				.Append("a=").Append(isAsync ? "1" : "0").Append(_separator)
				.Append("net=").Append(RuntimeVersion).Append(_separator)
				.Append(_httpClientIdentifier).Append("=").Append(RuntimeVersion)
				.ToString();
		}

		private static readonly string _httpClientIdentifier =
#if DOTNETCORE
			ConnectionInfo.UsingCurlHandler ? "cu" : "so";
#else
			"wr";
#endif

		public string ServiceIdentifier { get; private set; }
		public string ClientVersion { get; private set; }
		public string RuntimeVersion { get; private set; }

		public override string ToString() => _headerValue;
	}
}
