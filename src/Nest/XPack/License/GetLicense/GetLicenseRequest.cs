namespace Nest
{
	[MapsApi("license.get.json")]
	public partial interface IGetLicenseRequest { }

	public partial class GetLicenseRequest { }

	public partial class GetLicenseDescriptor : IGetLicenseRequest { }
}
