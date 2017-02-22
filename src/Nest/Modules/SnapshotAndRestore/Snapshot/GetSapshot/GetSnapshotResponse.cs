using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IGetSnapshotResponse : IResponse
	{
		[JsonProperty("snapshots")]
		IReadOnlyCollection<Snapshot> Snapshots { get; }
	}

	[JsonObject]
	public class GetSnapshotResponse : ResponseBase, IGetSnapshotResponse
	{

		[JsonProperty("snapshots")]
		public IReadOnlyCollection<Snapshot> Snapshots { get; internal set; } = EmptyReadOnly<Snapshot>.Collection;

	}
}
