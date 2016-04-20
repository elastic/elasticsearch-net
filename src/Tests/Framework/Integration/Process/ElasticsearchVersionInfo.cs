using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Tests.Framework.Integration
{
	public class ElasticsearchVersionInfo
	{
		public ElasticsearchVersionInfo(string version)
		{
			this.Version = version;

			if (this.IsSnapshot)
			{
				var mavenMetadata = XElement.Load($"{this.RootUrl}/{this.Version}/maven-metadata.xml");
				var snapshot = mavenMetadata.Descendants("versioning").Descendants("snapshot").FirstOrDefault();
				this.SnapshotTimestamp = snapshot.Descendants("timestamp").FirstOrDefault().Value;
				this.SnapshotBuildNumber = snapshot.Descendants("buildNumber").FirstOrDefault().Value;
				this.Zip = $"elasticsearch-{this.Version.Replace("SNAPSHOT", "")}{this.SnapshotIdentifier}.zip";
			}

			this.Zip = this.Zip ?? $"elasticsearch-{this.Version}.zip";
			this.DownloadUrl = $"{this.RootUrl}/{this.Version}/{this.Zip}";
			this.ParsedVersion = new Version(Regex.Replace(this.Version, @"(?:\-.+)$", ""));
		}

		public string DownloadUrl { get; set; }
		public string Zip { get; set; }
		public string Version { get; set; }
		public Version ParsedVersion { get; set; }
		public string SnapshotTimestamp { get; set; }
		public string SnapshotBuildNumber { get; set; }

		public string SnapshotIdentifier => $"{this.SnapshotTimestamp}-{this.SnapshotBuildNumber}";
		public bool IsSnapshot => this.Version?.ToLower().Contains("snapshot") ?? false;
		public string RootUrl => this.IsSnapshot
			? "https://oss.sonatype.org/content/repositories/snapshots/org/elasticsearch/distribution/zip/elasticsearch"
			: "https://download.elasticsearch.org/elasticsearch/release/org/elasticsearch/distribution/zip/elasticsearch";
	}
}