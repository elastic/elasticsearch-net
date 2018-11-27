using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IGetWatchResponse : IResponse
	{
		[DataMember(Name ="found")]
		bool Found { get; }

		[DataMember(Name ="_id")]
		string Id { get; }

		[DataMember(Name ="status")]
		WatchStatus Status { get; }

		[DataMember(Name ="watch")]
		Watch Watch { get; }
	}

	public class GetWatchResponse : ResponseBase, IGetWatchResponse
	{
		public bool Found { get; internal set; }
		public string Id { get; internal set; }
		public WatchStatus Status { get; internal set; }
		public Watch Watch { get; internal set; }
	}
}
