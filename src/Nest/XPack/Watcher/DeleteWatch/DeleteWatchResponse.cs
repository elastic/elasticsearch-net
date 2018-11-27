using System.Runtime.Serialization;

namespace Nest
{
	public interface IDeleteWatchResponse : IResponse
	{
		[DataMember(Name ="found")]
		bool Found { get; }

		[DataMember(Name ="_id")]
		string Id { get; }

		[DataMember(Name ="_version")]
		int Version { get; }
	}

	public class DeleteWatchResponse : ResponseBase, IDeleteWatchResponse
	{
		public bool Found { get; internal set; }
		public string Id { get; internal set; }
		public int Version { get; internal set; }
	}
}
