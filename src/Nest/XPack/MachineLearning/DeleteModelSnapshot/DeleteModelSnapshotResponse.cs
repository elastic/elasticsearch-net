namespace Nest
{
	public interface IDeleteModelSnapshotResponse : IAcknowledgedResponse {}

	public class DeleteModelSnapshotResponse : AcknowledgedResponseBase, IDeleteModelSnapshotResponse {}
}
