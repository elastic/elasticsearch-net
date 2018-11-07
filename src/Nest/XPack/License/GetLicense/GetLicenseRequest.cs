namespace Nest
{
	public partial interface IGetLicenseRequest { }

	public partial class GetLicenseRequest { }

	[DescriptorFor("XpackLicenseGet")]
	public partial class GetLicenseDescriptor : IGetLicenseRequest { }
}
