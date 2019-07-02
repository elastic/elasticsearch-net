using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class SnapshotResponse : ResponseBase
	{
		private bool _accepted;

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
