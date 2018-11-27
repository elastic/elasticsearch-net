using System.Runtime.Serialization;

namespace Nest
{
	public partial interface IPostLicenseRequest
	{
		[DataMember(Name ="license")]
		License License { get; set; }
	}

	public partial class PostLicenseRequest
	{
		public License License { get; set; }

		public static implicit operator PostLicenseRequest(License license) => new PostLicenseRequest { License = license };
	}

	[DescriptorFor("XpackLicensePost")]
	public partial class PostLicenseDescriptor
	{
		License IPostLicenseRequest.License { get; set; }

		public PostLicenseDescriptor License(License license) =>
			Assign(a => a.License = license);
	}
}
