namespace Nest
{
	public partial interface IMigrationAssistanceRequest { }

	public partial class MigrationAssistanceRequest { }

	[DescriptorFor("XpackMigrationGetAssistance")]
	public partial class MigrationAssistanceDescriptor : IMigrationAssistanceRequest { }
}
