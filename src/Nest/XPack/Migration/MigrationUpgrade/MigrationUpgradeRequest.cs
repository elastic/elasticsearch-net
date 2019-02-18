namespace Nest
{
	[MapsApi("migration.upgrade.json")]
	public partial interface IMigrationUpgradeRequest { }

	public partial class MigrationUpgradeRequest { }

	public partial class MigrationUpgradeDescriptor : IMigrationUpgradeRequest { }
}
