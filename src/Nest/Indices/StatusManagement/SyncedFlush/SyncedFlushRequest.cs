namespace Nest
{
	public partial interface ISyncedFlushRequest { }

	public partial class SyncedFlushRequest { }
	
	[DescriptorFor("IndicesFlushSynced")]
	public partial class SyncedFlushDescriptor { }
}
