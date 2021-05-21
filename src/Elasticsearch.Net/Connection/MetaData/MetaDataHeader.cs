// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;

namespace Elasticsearch.Net
{
	internal sealed class MetaDataHeader
	{
		private const char Separator = ',';

		private readonly string _headerValue;

		private static readonly string RuntimeVersionString = new RuntimeVersionInfo().ToString();

		public MetaDataHeader(VersionInfo version, string serviceIdentifier, bool isAsync)
		{
			ClientVersion = version.ToString();
			ServiceIdentifier = serviceIdentifier;
			RuntimeVersion = RuntimeVersionString;

			// This code is expected to be called infrequently so we're not concerned with over optimising this.

			_headerValue = new StringBuilder(64)
				.Append(serviceIdentifier).Append("=").Append(ClientVersion).Append(Separator)
				.Append("net=").Append(RuntimeVersion).Append(Separator)
				.Append("t=").Append(ClientVersion).Append(Separator)
				.Append("a=").Append(isAsync ? "1" : "0").Append(Separator)
				.Append(HttpClientIdentifier).Append("=").Append(RuntimeVersion)
				.ToString();
		}

		private static readonly string HttpClientIdentifier =
#if DOTNETCORE
			ConnectionInfo.UsingCurlHandler ? "cu" : "so";
#else
			"wr";
#endif

		public string ServiceIdentifier { get; }
		public string ClientVersion { get; }
		public string RuntimeVersion { get; }

		public override string ToString() => _headerValue;
	}
}
