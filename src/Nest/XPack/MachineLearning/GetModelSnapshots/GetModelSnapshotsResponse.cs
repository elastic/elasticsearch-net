using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetModelSnapshotsResponse : IResponse
	{
		[DataMember(Name ="count")]
		long Count { get; }

		[DataMember(Name ="model_snapshots")]
		IReadOnlyCollection<ModelSnapshot> ModelSnapshots { get; }
	}

	public class GetModelSnapshotsResponse : ResponseBase, IGetModelSnapshotsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<ModelSnapshot> ModelSnapshots { get; internal set; } = EmptyReadOnly<ModelSnapshot>.Collection;
	}
}
