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

			// This code is expected to be called infrequently so we're not concerns with over optimising this

			_headerValue = new StringBuilder(64)
				.Append(serviceIdentifier).Append("=").Append(ClientVersion).Append(Separator)
				.Append("a=").Append(isAsync ? "1" : "0").Append(Separator)
				.Append("net=").Append(RuntimeVersion).Append(Separator)
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
