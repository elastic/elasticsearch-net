using System.Runtime.Serialization;

namespace Nest
{
	public class GetWatchResponse : ResponseBase
	{
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="status")]
		public WatchStatus Status { get; internal set; }

		[DataMember(Name ="watch")]
		public IWatch Watch { get; internal set; }
	}
}
