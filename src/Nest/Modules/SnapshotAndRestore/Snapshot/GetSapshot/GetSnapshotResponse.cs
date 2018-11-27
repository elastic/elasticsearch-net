using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetSnapshotResponse : IResponse
	{
		[DataMember(Name ="snapshots")]
		IReadOnlyCollection<Snapshot> Snapshots { get; }
	}

	[DataContract]
	public class GetSnapshotResponse : ResponseBase, IGetSnapshotResponse
	{
		[DataMember(Name ="snapshots")]
		public IReadOnlyCollection<Snapshot> Snapshots { get; internal set; } = EmptyReadOnly<Snapshot>.Collection;
	}
}
