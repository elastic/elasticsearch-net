using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ExplanationDetail
	{
		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		[DataMember(Name ="details")]
		public IReadOnlyCollection<ExplanationDetail> Details { get; internal set; } = EmptyReadOnly<ExplanationDetail>.Collection;

		[DataMember(Name ="value")]
		public float Value { get; internal set; }
	}
}
