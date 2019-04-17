using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class RestoreResponse : ResponseBase
	{
		[DataMember(Name ="snapshot")]
		public SnapshotRestore Snapshot { get; set; }
	}
}
