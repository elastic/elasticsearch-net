using Newtonsoft.Json;

namespace Nest
{
	public interface ISnapshotResponse : IResponse
	{
		[JsonProperty("accepted")]
		bool Accepted { get; }

		[JsonProperty("snapshot")]
		Snapshot Snapshot { get; set; }
	}

	[JsonObject]
	public class SnapshotResponse : ResponseBase, ISnapshotResponse
	{
		private bool _accepted = false;
		[JsonProperty("accepted")]
		public bool Accepted
		{
			get
			{
				return  _accepted ? _accepted : this.Snapshot != null;
			}
			internal set { _accepted = value; }
		}

		[JsonProperty("snapshot")]
		public Snapshot Snapshot { get; set; }

	}
}
