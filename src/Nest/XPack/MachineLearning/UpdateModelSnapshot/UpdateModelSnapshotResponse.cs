using System.Runtime.Serialization;

namespace Nest
{
	public class UpdateModelSnapshotResponse : AcknowledgedResponseBase
	{
		[DataMember(Name ="model")]
		public ModelSnapshot Model { get; internal set; }
	}
}
