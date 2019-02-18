namespace Nest
{
	[MapsApi("xpack.usage.json")]
	public partial interface IXPackUsageRequest { }

	public partial class XPackUsageRequest { }

	public partial class XPackUsageDescriptor : IXPackUsageRequest { }
}
