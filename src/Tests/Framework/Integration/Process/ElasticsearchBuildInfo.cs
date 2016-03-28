using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Framework.Integration
{
	public class ElasticsearchBuildInfo
	{
		public string DownloadUrl { get; set; }
		public string Zip { get; set; }
		public string Version { get; set; }
		public Version ParsedVersion { get; set; }
		public string SnapshotBuildNumber { get; set; }
	}
}
