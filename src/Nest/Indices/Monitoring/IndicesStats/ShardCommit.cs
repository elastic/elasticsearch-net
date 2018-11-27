using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardCommit
	{
		[DataMember(Name ="generation")]
		public int Generation { get; internal set; }

		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		[DataMember(Name ="num_docs")]
		public long NumberOfDocuments { get; internal set; }

		[DataMember(Name ="user_data")]
		public IReadOnlyDictionary<string, string> UserData { get; internal set; }
	}
}
