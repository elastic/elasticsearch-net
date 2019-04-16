using System.Runtime.Serialization;

namespace Nest
{
	public class SampleDiversity
	{
		[DataMember(Name ="field")]
		public Field Field { get; set; }

		[DataMember(Name ="max_docs_per_value")]
		public int? MaxDocumentsPerValue { get; set; }
	}
}
