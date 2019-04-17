using System.Runtime.Serialization;

namespace Nest
{
	public class RevertModelSnapshotResponse : ResponseBase
	{
		[DataMember(Name ="model")]
		public ModelSnapshot Model { get; internal set; }
	}
}
