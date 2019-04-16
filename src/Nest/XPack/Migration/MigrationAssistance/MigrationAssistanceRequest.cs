namespace Nest
{
	[MapsApi("migration.get_assistance.json")]
	public partial interface IMigrationAssistanceRequest { }

	public partial class MigrationAssistanceRequest { }

	public partial class MigrationAssistanceDescriptor : IMigrationAssistanceRequest { }
}
