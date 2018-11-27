using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class TermVector
	{
		[DataMember(Name ="field_statistics")]
		public FieldStatistics FieldStatistics { get; internal set; }

		[DataMember(Name ="terms")]
		public IReadOnlyDictionary<string, TermVectorTerm> Terms { get; internal set; } =
			EmptyReadOnly<string, TermVectorTerm>.Dictionary;
	}
}
