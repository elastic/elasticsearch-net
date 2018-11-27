using System.Runtime.Serialization;

namespace Nest
{
	public interface IRestoreResponse : IResponse
	{
		[DataMember(Name ="snapshot")]
		SnapshotRestore Snapshot { get; set; }
	}

	[DataContract]
	public class RestoreResponse : ResponseBase, IRestoreResponse
	{
		[DataMember(Name ="snapshot")]
		public SnapshotRestore Snapshot { get; set; }
	}
}
