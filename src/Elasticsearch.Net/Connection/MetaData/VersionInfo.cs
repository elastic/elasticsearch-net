// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;

namespace Elasticsearch.Net
{
	public abstract class VersionInfo
	{
		protected const string EmptyVersion = "0.0.0";

		public Version Version { get; protected set; }
		public bool IsPrerelease { get; protected set; }

		protected void StoreVersion(string fullVersion)
		{
			if (string.IsNullOrEmpty(fullVersion))
				fullVersion = EmptyVersion;

			var clientVersion = GetParsableVersionPart(fullVersion);

			if (!Version.TryParse(clientVersion, out var parsedVersion))
				throw new ArgumentException("Invalid version string", nameof(fullVersion));

			var finalVersion = parsedVersion;

			if (parsedVersion.Minor == -1 || parsedVersion.Build == -1)
				finalVersion = new Version(parsedVersion.Major, parsedVersion.Minor > -1
					? parsedVersion.Minor
					: 0, parsedVersion.Build > -1
						? parsedVersion.Build
						: 0);

			Version = finalVersion;
			IsPrerelease = ContainsPrerelease(fullVersion);
		}

		protected virtual bool ContainsPrerelease(string version) => version.Contains("-");

		private static string GetParsableVersionPart(string fullVersionName) =>
			new string(fullVersionName.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());

		public override string ToString() => IsPrerelease ? Version.ToString() + "p" : Version.ToString();
	}
}
