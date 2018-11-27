using System.Runtime.Serialization;

namespace Nest
{
	public interface ISnapshotResponse : IResponse
	{
		[DataMember(Name ="accepted")]
		bool Accepted { get; }

		[DataMember(Name ="snapshot")]
		Snapshot Snapshot { get; set; }
	}

	[DataContract]
	public class SnapshotResponse : ResponseBase, ISnapshotResponse
	{
		private bool _accepted = false;

		[DataMember(Name ="accepted")]
		public bool Accepted
		{
			get => _accepted ? _accepted : Snapshot != null;
			internal set => _accepted = value;
		}

		[DataMember(Name ="snapshot")]
		public Snapshot Snapshot { get; set; }
	}
}
