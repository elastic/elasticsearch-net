namespace Nest
{
	[MapsApi("license.delete.json")]
	public partial interface IDeleteLicenseRequest { }

	public partial class DeleteLicenseRequest { }

	public partial class DeleteLicenseDescriptor : IDeleteLicenseRequest { }
}
