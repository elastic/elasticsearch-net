using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetSnapshotResponse : IResponse
	{
		[JsonProperty("snapshots")]
		IEnumerable<Snapshot> Snapshots { get; set; }
	}

	[JsonObject]
	public class GetSnapshotResponse : ResponseBase, IGetSnapshotResponse
	{

		[JsonProperty("snapshots")]
		public IEnumerable<Snapshot> Snapshots { get; set; }

	}
}
