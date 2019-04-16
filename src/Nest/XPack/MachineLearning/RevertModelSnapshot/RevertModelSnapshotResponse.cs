using System.Runtime.Serialization;

namespace Nest
{
	public interface IRevertModelSnapshotResponse : IResponse
	{
		[DataMember(Name ="model")]
		ModelSnapshot Model { get; }
	}

	public class RevertModelSnapshotResponse : ResponseBase, IRevertModelSnapshotResponse
	{
		public ModelSnapshot Model { get; internal set; }
	}
}
