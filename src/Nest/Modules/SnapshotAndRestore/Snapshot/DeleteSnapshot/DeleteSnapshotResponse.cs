namespace Nest
{
	public interface IDeleteSnapshotResponse : IAcknowledgedResponse { }

	public class DeleteSnapshotResponse : AcknowledgedResponseBase, IDeleteSnapshotResponse { }
}
