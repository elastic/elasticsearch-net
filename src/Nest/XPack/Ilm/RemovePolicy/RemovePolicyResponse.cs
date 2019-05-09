using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RemovePolicyResponse : ResponseBase
	{
		[DataMember(Name = "failed_indexes")]
		public IReadOnlyCollection<string> FailedIndexes { get; internal set; } = EmptyReadOnly<string>.Collection;
		[DataMember(Name = "has_failures")]
		public bool HasFailures { get; internal set; }
	}
}
