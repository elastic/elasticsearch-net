using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class FieldStatistics
	{
		[DataMember(Name ="doc_count")]
		public int DocumentCount { get; internal set; }

		[DataMember(Name ="sum_doc_freq")]
		public int SumOfDocumentFrequencies { get; internal set; }

		[DataMember(Name ="sum_ttf")]
		public int SumOfTotalTermFrequencies { get; internal set; }
	}
}
