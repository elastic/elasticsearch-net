using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Xml.Linq;
using Version = SemVer.Version;

namespace Tests.Framework.Versions
{
	public class ElasticsearchVersionResolver
	{
		private static readonly object _lock = new { };

		public const string SonaTypeUrl = "https://oss.sonatype.org/content/repositories/snapshots/org/elasticsearch/distribution/zip/elasticsearch";

		//public virtual string LatestVersion

		public static readonly ElasticsearchVersionResolver Default = new ElasticsearchVersionResolver();

		public virtual string LatestVersion => _latestVersion.Value;
		public virtual string LatestSnapshot => _latestSnapshot.Value;

		private static readonly ConcurrentDictionary<string, string> SnapshotVersions = new ConcurrentDictionary<string, string>();

		public virtual string SnapshotZipFilename(string version)
		{
			lock (_lock)
			{
				if (SnapshotVersions.TryGetValue(version, out var zipLocation))
					return zipLocation;

				zipLocation = ResolveLatestSnapshotFor(version);
				SnapshotVersions.TryAdd(version, zipLocation);
				return zipLocation;
			}
		}

		private readonly Lazy<string> _latestSnapshot = new Lazy<string>(ResolveLatestSnapshot, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string ResolveLatestSnapshot()
		{
			var version = Default.LatestVersion;
			return ResolveLatestSnapshotFor(version);
		}

		private static string ResolveLatestSnapshotFor(string version)
		{
			var url = $"{SonaTypeUrl}/{version}/maven-metadata.xml";
			try
			{
				using (var httpclient = new HttpClient())
				{
					var response = httpclient.GetAsync(url).Result;
					var mavenMetadata = XElement.Load(response.Content.ReadAsStreamAsync().Result);
					var snapshot = mavenMetadata.Descendants("versioning").Descendants("snapshot").FirstOrDefault();
					var snapshotTimestamp = snapshot.Descendants("timestamp").FirstOrDefault().Value;
					var snapshotBuildNumber = snapshot.Descendants("buildNumber").FirstOrDefault().Value;
					var identifier = $"{snapshotTimestamp}-{snapshotBuildNumber}";
					var zip = $"elasticsearch-{version.Replace("SNAPSHOT", "")}{identifier}.zip";
					return zip;
				}

			}
			catch (Exception e)
			{
				throw new Exception($"Can not download maven data from {url}", e);
			}
		}

		private readonly Lazy<string> _latestVersion = new Lazy<string>(ResolveLatestVersion, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string ResolveLatestVersion()
		{
			var url = $"{SonaTypeUrl}/maven-metadata.xml";
			try
			{
				using(var httpclient = new HttpClient())
				{
					var response = httpclient.GetAsync(url).Result;
					var xml = XElement.Load(response.Content.ReadAsStreamAsync().Result);
					var versions = xml
						.Descendants("version")
						.Select(n => new Version(n.Value))
						.OrderByDescending(n => n);
					return versions.First().ToString();
				}
			}
			catch (Exception e)
			{
				throw new Exception($"Can not download maven data from {url}", e);
			}
		}
	}
}
