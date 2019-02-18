using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("license.post.json")]
	public partial interface IPostLicenseRequest
	{
		[JsonProperty("license")]
		License License { get; set; }
	}

	public partial class PostLicenseRequest
	{
		public License License { get; set; }

		public static implicit operator PostLicenseRequest(License license) => new PostLicenseRequest { License = license };
	}

	public partial class PostLicenseDescriptor
	{
		License IPostLicenseRequest.License { get; set; }

		public PostLicenseDescriptor License(License license) =>
			Assign(a => a.License = license);
	}
}
