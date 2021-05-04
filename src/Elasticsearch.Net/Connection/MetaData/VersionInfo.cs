// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;

namespace Elasticsearch.Net
{
	internal abstract class VersionInfo
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
				parsedVersion = new Version(EmptyVersion);

			var finalVersion = parsedVersion;

			if (parsedVersion.Minor == -1 || parsedVersion.Build == -1)
				finalVersion = new Version(parsedVersion.Major, parsedVersion.Minor > -1
					? parsedVersion.Minor
					: 0, parsedVersion.Build > -1
						? parsedVersion.Build
						: 0);

			Version = finalVersion;
			IsPrerelease = !IsEmpty(parsedVersion) && ContainsPrerelease(fullVersion);
		}

		private bool IsEmpty(Version version) => version.Major == 0 && version.Minor == 0 && version.Build == 0;

		protected virtual bool ContainsPrerelease(string version) => version.Contains("-");

		private static string GetParsableVersionPart(string fullVersionName) =>
			new(fullVersionName.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());

		public override string ToString() => IsPrerelease ? $"{Version}p" : Version.ToString();
	}
}
