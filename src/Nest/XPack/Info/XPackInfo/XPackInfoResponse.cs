using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IXPackInfoResponse : IResponse
	{
		[JsonProperty("build")]
		XPackBuildInformation Build { get; }
		[JsonProperty("license")]
		MinimalLicenseInformation License { get; }
		[JsonProperty("features")]
		XPackFeatures Features { get; }
		[JsonProperty("tagline")]
		string Tagline { get; }
	}

	public class XPackInfoResponse : ResponseBase, IXPackInfoResponse
	{
		public XPackBuildInformation Build { get; internal set; }
		public MinimalLicenseInformation License { get; internal set; }
		public XPackFeatures Features { get; internal set; }
		public string Tagline { get; internal set; }
	}

	public class XPackBuildInformation
	{
		[JsonProperty("date")]
		public DateTimeOffset Date { get; internal set; }
		[JsonProperty("hash")]
		public string Hash { get; internal set; }
	}

	public class MinimalLicenseInformation
	{
		[JsonProperty("uid")]
		public string UID { get; internal set; }

		[JsonProperty("type")]
		public LicenseType Type { get; internal set; }

		[JsonProperty("mode")]
		public LicenseType Mode { get; internal set; }

		[JsonProperty("status")]
		public LicenseStatus Status { get; internal set; }

		[JsonProperty("expiry_date_in_millis")]
		public long ExpiryDateInMilliseconds { get; set; }
	}

	public class XPackFeatures
	{
		[JsonProperty("watcher")]
		public XPackFeature Watcher { get; internal set; }
		[JsonProperty("graph")]
		public XPackFeature Graph { get; internal set; }
		[JsonProperty("ml")]
		public XPackFeature MachineLearning { get; internal set; }
		[JsonProperty("monitoring")]
		public XPackFeature Monitoring { get; internal set; }
		[JsonProperty("security")]
		public XPackFeature Security { get; internal set; }
	}

	public class XPackFeature
	{
		[JsonProperty("description")]
		public string Description { get; internal set; }
		[JsonProperty("available")]
		public bool Available { get; internal set; }
		[JsonProperty("enabled")]
		public bool Enabled { get; internal set; }
		[JsonProperty("native_code_info")]
		public NativeCodeInformation NativeCodeInformation { get; internal set; }
	}
	public class NativeCodeInformation
	{
		[JsonProperty("version")]
		public string Version { get; internal set; }
		[JsonProperty("build_hash")]
		public string BuildHash { get; internal set; }
	}

}
