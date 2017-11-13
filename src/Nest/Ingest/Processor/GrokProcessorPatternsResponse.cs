using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGrokProcessorPatternsResponse : IResponse
	{
		IReadOnlyDictionary<string, string> Patterns { get; }
	}

	public class GrokProcessorPatternsResponse : ResponseBase, IGrokProcessorPatternsResponse
	{
		[JsonProperty("patterns")]
		public IReadOnlyDictionary<string, string> Patterns { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
