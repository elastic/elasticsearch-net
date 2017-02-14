using System;
using System.Collections.Concurrent;
using System.IO.Packaging;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using Version = SemVer.Version;

namespace Tests.Framework.Versions
{
	public class ElasticsearchVersion : Version
	{
		private static readonly Lazy<string> LatestVersion = new Lazy<string>(ResolveLatestVersion, LazyThreadSafetyMode.ExecutionAndPublication);
		private static readonly Lazy<string> LatestSnapshot = new Lazy<string>(ResolveLatestSnapshot, LazyThreadSafetyMode.ExecutionAndPublication);
		private static readonly object _lock = new { };
		private static readonly ConcurrentDictionary<string, string> SnapshotVersions = new ConcurrentDictionary<string, string>();
		private static readonly string SonaTypeUrl = "https://oss.sonatype.org/content/repositories/snapshots/org/elasticsearch/distribution/zip/elasticsearch";

		private string RootUrl => this.IsSnapshot
			? SonaTypeUrl
			: "https://artifacts.elastic.co/downloads/elasticsearch";

		private static string ResolveLatestSnapshot()
		{
			var version = LatestVersion.Value;
			return ResolveLatestSnapshotFor(version);
		}

		private static string ResolveLatestSnapshotFor(string version)
		{
			var mavenMetadata = XElement.Load($"{SonaTypeUrl}/{version}/maven-metadata.xml");
			var snapshot = mavenMetadata.Descendants("versioning").Descendants("snapshot").FirstOrDefault();
			var snapshotTimestamp = snapshot.Descendants("timestamp").FirstOrDefault().Value;
			var snapshotBuildNumber = snapshot.Descendants("buildNumber").FirstOrDefault().Value;
			var identifier = $"{snapshotTimestamp}-{snapshotBuildNumber}";
			var zip = $"elasticsearch-{version.Replace("SNAPSHOT", "")}{identifier}.zip";
			return zip;
		}

		private static string ResolveLatestVersion()
		{
			var versions = XElement.Load($"{SonaTypeUrl}/maven-metadata.xml")
				.Descendants("version")
				.Select(n => new Version(n.Value))
				.OrderByDescending(n => n);
			return versions.First().ToString();
		}

		private static string TranslateConfigVersion(string configMoniker) => configMoniker == "latest" ? LatestVersion.Value : configMoniker;

		public ElasticsearchVersion(string version) : base(TranslateConfigVersion(version))
		{
			this.Version = version;
			if (this.Version.Equals("latest", StringComparison.OrdinalIgnoreCase))
				this.Version = LatestVersion.Value;
			else if (this.IsSnapshot && this.Version.Equals("snapshot", StringComparison.OrdinalIgnoreCase))
				this.Zip = LatestSnapshot.Value;
			else if (this.IsSnapshot)
			{
				lock (_lock)
				{
					string zipLocation;
					if (SnapshotVersions.TryGetValue(version, out zipLocation))
						this.Zip = zipLocation;
					else
					{
						zipLocation = ResolveLatestSnapshotFor(version);
						SnapshotVersions.TryAdd(version, zipLocation);
						this.Zip = zipLocation;
					}
				}
			}

			this.Zip = this.Zip ?? $"elasticsearch-{this.Version}.zip";
			if (this.IsSnapshot) this.DownloadUrl = $"{this.RootUrl}/{this.Version}/{this.Zip}";
			else this.DownloadUrl = $"{this.RootUrl}/{this.Zip}";
		}

		public string DownloadUrl { get; }
		public string Zip { get; }
		public string Version { get; }

		/// <summary>
		/// Returns the version in elasticsearch-{version} format, for SNAPSHOTS this includes a
		/// datetime suffix
		/// </summary>
		public string FullyQualifiedVersion => this.Zip?.Replace(".zip", "").Replace("elasticsearch-", "");

		/// <summary>
		/// The folder name to expect to be in the zip distribution
		/// </summary>
		public string FolderInZip => $"elasticsearch-{this.Version}";

		/// <summary>
		/// Whether this version is a snapshot or officicially released distribution
		/// </summary>
		public bool IsSnapshot => this.Version?.ToLower().Contains("snapshot") ?? false;
	}
}
