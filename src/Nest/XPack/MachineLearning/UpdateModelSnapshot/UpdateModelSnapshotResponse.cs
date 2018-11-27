using System.Runtime.Serialization;

namespace Nest
{
	public interface IUpdateModelSnapshotResponse : IAcknowledgedResponse
	{
		[DataMember(Name ="model")]
		ModelSnapshot Model { get; }
	}

	public class UpdateModelSnapshotResponse : AcknowledgedResponseBase, IUpdateModelSnapshotResponse
	{
		public ModelSnapshot Model { get; internal set; }
	}
}
