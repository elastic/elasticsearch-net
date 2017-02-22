using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IRestoreResponse : IResponse
	{
		[JsonProperty("snapshot")]
		SnapshotRestore Snapshot { get; set; }
	}

	[JsonObject]
	public class RestoreResponse : ResponseBase, IRestoreResponse
	{
		[JsonProperty("snapshot")]
		public SnapshotRestore Snapshot { get; set; }
	}
}
