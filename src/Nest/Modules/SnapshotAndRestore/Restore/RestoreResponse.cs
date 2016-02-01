using Newtonsoft.Json;

namespace Nest
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
