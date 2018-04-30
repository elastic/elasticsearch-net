using System;
using System.Collections.Concurrent;
using Version = SemVer.Version;

namespace Tests.Framework.Versions
{
	public class ElasticsearchVersion : Version
	{
		private readonly ElasticsearchVersionResolver _resolver;

		private static readonly ConcurrentDictionary<string, ElasticsearchVersion> Versions = new ConcurrentDictionary<string, ElasticsearchVersion>();

		private const string ArtifactsUrl = "https://artifacts.elastic.co/downloads/elasticsearch";
		private const string StagingUrlFormat = "https://staging.elastic.co/{0}-{1}";

		public enum ReleaseState { Released, Snapshot, BuildCandidate }
		public ReleaseState State { get; set; }
		public string Version { get; }

		private string RootUrl { get; }
		public string ElasticsearchDownloadUrl { get; }
		public string ZipFilename { get; }
		/// <summary> The folder name to expect to be in the zip distribution </summary>
		public string FolderInZip => $"elasticsearch-{this.Version}";

		/// <summary> The local folder name to extract too </summary>
		public string LocalFolderName { get; }

		/// <summary>
		/// Represents an Elasticsearch version and its download locations
		/// <pre>Follows a static factory pattern to create new instances because based on the version it calls out to the internet to learn more</pre>
		/// </summary>
		/// <param name="version">Can be either a released version, snapshot version or a build candidate following a githash:version pattern</param>
		/// <param name="resolver">A resolver that can find out the latest elasticsearch version</param>
		/// <returns></returns>
		public static ElasticsearchVersion Create(string version, ElasticsearchVersionResolver resolver = null)
		{
			return resolver == null
				? Versions.GetOrAdd(version, v => new ElasticsearchVersion(v, ElasticsearchVersionResolver.Default))
				: new ElasticsearchVersion(version, resolver);
		}

		public static implicit operator ElasticsearchVersion(string version) => string.IsNullOrWhiteSpace(version)
			? null
			: Create(version);

		internal ElasticsearchVersion(string version, ElasticsearchVersionResolver resolver)
			: base(TranslateConfigVersion(version, resolver))
		{
			this._resolver = resolver;
			this.Version = version;
			this.ZipFilename = $"elasticsearch-{this.Version}.zip";
			switch (version)
			{
				case "latest":
					this.State = ReleaseState.Released;
					this.Version = _resolver.LatestVersion;
					this.ZipFilename = _resolver.LatestSnapshot;
					this.RootUrl = ArtifactsUrl;
					this.LocalFolderName = this.Version;
					break;
				case string snapShotVersion when version.EndsWith("-snapshot", StringComparison.OrdinalIgnoreCase):
					this.State = ReleaseState.Snapshot;
					this.RootUrl = ElasticsearchVersionResolver.SonaTypeUrl;
					this.ZipFilename = resolver.SnapshotZipFilename(version);
					this.LocalFolderName = this.ZipFilename?.Replace(".zip", "").Replace("elasticsearch-", "");
					this.ElasticsearchDownloadUrl = $"{this.RootUrl}/{this.Version}/{this.ZipFilename}";
					break;
				case string bcVersion when TryParseBuildCandidate(version, out var v, out var gitHash):
					this.State = ReleaseState.BuildCandidate;
					this.Version = v;
					this.RootUrl = string.Format(StagingUrlFormat, v, gitHash);
					this.ZipFilename = $"elasticsearch-{this.Version}.zip";
					this.ElasticsearchDownloadUrl = $"{this.RootUrl}/downloads/elasticsearch/{this.ZipFilename}";
					this.LocalFolderName = $"{v}-bc+{gitHash}";
					break;
				default:
					this.State = ReleaseState.Released;
					this.RootUrl = ArtifactsUrl;
					break;
			}


			this.ElasticsearchDownloadUrl = this.ElasticsearchDownloadUrl ?? $"{this.RootUrl}/{this.ZipFilename}";
			this.LocalFolderName = this.LocalFolderName ?? this.Version;
		}

		public string PluginDownloadUrl(string moniker)
		{
			var zip = $"{moniker}-{this.Version}.zip";
			switch (State)
			{
				case ReleaseState.Snapshot: return $"https://snapshots.elastic.co/downloads/elasticsearch-plugins/{moniker}/{zip}";
				case ReleaseState.BuildCandidate:
					return $"{this.RootUrl}/downloads/elasticsearch-plugins/{moniker}/{zip}";
			}
			return moniker;
		}

		private static string TranslateConfigVersion(string configMoniker, ElasticsearchVersionResolver resolver)
		{
			switch (configMoniker)
			{
				case "latest" : return resolver.LatestVersion;
				case string _ when TryParseBuildCandidate(configMoniker, out var version, out _):
					return version;
				default: return configMoniker;

			}
		}

		private static bool TryParseBuildCandidate(string passedVersion, out string version, out string gitHash)
		{
			version = null;
			gitHash = null;
			var tokens = passedVersion.Split(':');
			if (tokens.Length < 2) return false;
			version = tokens[1].Trim();
			gitHash = tokens[0].Trim();
			return true;
		}

		public override string ToString() => this.Version;
	}
}
