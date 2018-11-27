using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class Token
	{
		[DataMember(Name ="end_offset")]
		public int EndOffset { get; internal set; }

		[DataMember(Name ="payload")]
		public string Payload { get; internal set; }

		[DataMember(Name ="position")]
		public int Position { get; internal set; }

		[DataMember(Name ="start_offset")]
		public int StartOffset { get; internal set; }
	}
}
