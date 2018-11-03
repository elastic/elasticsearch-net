namespace Nest
{
	public interface ISyncedFlushResponse : IShardsOperationResponse { }

	public class SyncedFlushResponse : ShardsOperationResponseBase, ISyncedFlushResponse { }
}
