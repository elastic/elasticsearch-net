using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGrokProcessorPatternsResponse : IResponse
	{
		IReadOnlyDictionary<string, string> Patterns { get; }
	}

	public class GrokProcessorPatternsResponse : ResponseBase, IGrokProcessorPatternsResponse
	{
		[DataMember(Name ="patterns")]
		public IReadOnlyDictionary<string, string> Patterns { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
