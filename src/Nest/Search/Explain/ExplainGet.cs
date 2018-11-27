using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class InstantGet<TDocument> where TDocument : class
	{
		[DataMember(Name ="fields")]
		public FieldValues Fields { get; internal set; }

		[DataMember(Name ="found")]
		public bool Found { get; internal set; }

		[DataMember(Name ="_source")]
		[JsonConverter(typeof(SourceConverter))]
		public TDocument Source { get; internal set; }
	}
}
