using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetModelSnapshotsResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("model_snapshots")]
		IReadOnlyCollection<ModelSnapshot> ModelSnapshots { get; }
	}

	public class GetModelSnapshotsResponse : ResponseBase, IGetModelSnapshotsResponse
	{
		public long Count { get; internal set;  }

		public IReadOnlyCollection<ModelSnapshot> ModelSnapshots { get; internal set; } = EmptyReadOnly<ModelSnapshot>.Collection;
	}
}
