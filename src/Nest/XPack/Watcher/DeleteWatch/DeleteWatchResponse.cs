using System.Runtime.Serialization;

namespace Nest
{
	public class DeleteWatchResponse : ResponseBase
	{
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_version")]
		public int Version { get; internal set; }
	}
}
