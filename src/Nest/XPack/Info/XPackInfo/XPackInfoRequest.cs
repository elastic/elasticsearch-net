namespace Nest
{
	[MapsApi("info.json")]
	public partial interface IXPackInfoRequest { }

	public partial class XPackInfoRequest { }

	public partial class XPackInfoDescriptor : IXPackInfoRequest { }
}
