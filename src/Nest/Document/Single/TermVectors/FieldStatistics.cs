using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class FieldStatistics
	{
		[DataMember(Name ="doc_count")]
		public int DocumentCount { get; internal set; }

		[DataMember(Name ="sum_doc_freq")]
		public long SumOfDocumentFrequencies { get; internal set; }

		[DataMember(Name ="sum_ttf")]
		public long SumOfTotalTermFrequencies { get; internal set; }
	}
}
