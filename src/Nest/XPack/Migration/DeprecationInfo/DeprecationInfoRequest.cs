namespace Nest
{
	[MapsApi("migration.deprecations.json")]
	public partial interface IDeprecationInfoRequest { }

	public partial class DeprecationInfoRequest { }

	public partial class DeprecationInfoDescriptor : IDeprecationInfoRequest { }
}
