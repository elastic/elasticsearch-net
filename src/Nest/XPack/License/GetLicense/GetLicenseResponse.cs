using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetLicenseResponse : IResponse
	{
		[JsonProperty("license")]
		LicenseInformation License { get; }
	}

	public class GetLicenseResponse : ResponseBase, IGetLicenseResponse
	{
		public override bool IsValid => base.IsValid && (!License?.UID.IsNullOrEmpty() ?? false);

		public LicenseInformation License { get; internal set; }
	}

	public class LicenseInformation
	{
		[JsonProperty("status")]
		public LicenseStatus Status { get; internal set; }

		[JsonProperty("uid")]
		public string UID { get; internal set; }

		[JsonProperty("type")]
		public LicenseType Type { get; internal set; }

		[JsonProperty("issue_date")]
		public DateTime IssueDate { get; internal set; }

		[JsonProperty("issue_date_in_millis")]
		public long IssueDateInMilliseconds { get; internal set; }

		[JsonProperty("expiry_date")]
		public DateTime ExpiryDate { get; internal set; }

		[JsonProperty("expiry_date_in_millis")]
		public long ExpiryDateInMilliseconds { get; internal set; }

		[JsonProperty("max_nodes")]
		public long MaxNodes { get; internal set; }

		[JsonProperty("issued_to")]
		public string IssuedTo { get; internal set; }

		[JsonProperty("issuer")]
		public string Issuer { get; internal set; }
	}

	public class License
	{
		[JsonProperty("uid")]
		public string UID { get; set; }

		[JsonProperty("signature")]
		public string Signature { get; set; }

		[JsonProperty("type")]
		public LicenseType Type { get; set; }

		[JsonProperty("issue_date_in_millis")]
		public long IssueDateInMilliseconds { get; set; }

		[JsonProperty("expiry_date_in_millis")]
		public long ExpiryDateInMilliseconds { get; set; }

		[JsonProperty("max_nodes")]
		public long MaxNodes { get; set; }

		[JsonProperty("issued_to")]
		public string IssuedTo { get; set; }

		[JsonProperty("issuer")]
		public string Issuer { get; set; }

		private class Wrapped
		{
			[JsonProperty("license")]
			public License License { get; set; }
		}
		public static License LoadFromDisk(string path)
		{
			var contents = File.ReadAllText(path);
			var license = JsonConvert.DeserializeObject<Wrapped>(contents)?.License;
			return license;
		}
	}
}
