using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GrokProcessorPatternsResponse : ResponseBase
	{
		[DataMember(Name ="patterns")]
		public IReadOnlyDictionary<string, string> Patterns { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
