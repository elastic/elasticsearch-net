using System.Runtime.Serialization;

namespace Nest
{
	public interface IRevertModelSnapshotResponse : IAcknowledgedResponse
	{
		[DataMember(Name ="model")]
		ModelSnapshot Model { get; }
	}

	public class RevertModelSnapshotResponse : AcknowledgedResponseBase, IRevertModelSnapshotResponse
	{
		public ModelSnapshot Model { get; internal set; }
	}
}
