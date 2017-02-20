using System;
using System.Collections.Concurrent;
using System.Linq;
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
		private static readonly string OldDownloadUrl = "https://download.elasticsearch.org/elasticsearch/release/org/elasticsearch/distribution/zip/elasticsearch";

		private string RootUrl => this.IsSnapshot
			? SonaTypeUrl
			: TestClient.VersionSatisfiedBy("<5.0.0", this)
				? OldDownloadUrl
				: "https://artifacts.elastic.co/downloads/elasticsearch";

		private static string ResolveLatestSnapshot()
		{
			var version = LatestVersion.Value;
			return ResolveLatestSnapshotFor(version);
		}

		private static string ResolveLatestSnapshotFor(string version)
		{
			var url = $"{SonaTypeUrl}/{version}/maven-metadata.xml";
			try
			{
				var mavenMetadata = XElement.Load(url);
				var snapshot = mavenMetadata.Descendants("versioning").Descendants("snapshot").FirstOrDefault();
				var snapshotTimestamp = snapshot.Descendants("timestamp").FirstOrDefault().Value;
				var snapshotBuildNumber = snapshot.Descendants("buildNumber").FirstOrDefault().Value;
				var identifier = $"{snapshotTimestamp}-{snapshotBuildNumber}";
				var zip = $"elasticsearch-{version.Replace("SNAPSHOT", "")}{identifier}.zip";
				return zip;

			}
			catch (Exception e)
			{
				throw new Exception($"Can not download maven data from {url}", e);
			}
		}

		private static string ResolveLatestVersion()
		{
			var url = $"{SonaTypeUrl}/maven-metadata.xml";
			try
			{
                var versions = XElement.Load(url)
                    .Descendants("version")
                    .Select(n => new Version(n.Value))
                    .OrderByDescending(n => n);
                return versions.First().ToString();
			}
			catch (Exception e)
			{
				throw new Exception($"Can not download maven data from {url}", e);
			}
		}

		private static string TranslateConfigVersion(string configMoniker) => configMoniker == "latest" ? LatestVersion.Value : configMoniker;

		public ElasticsearchVersion(string version) : base(TranslateConfigVersion(version))
		{
			this.Version = version;
			if (this.Version.Equals("latest", StringComparison.OrdinalIgnoreCase))
			{
				this.Version = LatestVersion.Value;
				this.Zip = LatestSnapshot.Value;
			}
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
			else if(TestClient.VersionSatisfiedBy("<5.0.0", this))
				this.DownloadUrl = $"{this.RootUrl}/{this.Version}/{this.Zip}";
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

		public override string ToString() => this.Version;
	}
}
