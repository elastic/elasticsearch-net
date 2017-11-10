using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIngestProcessorGrokResponse : IResponse
	{
		IReadOnlyDictionary<string, string> Patterns { get; }
	}

	public class IngestProcessorGrokResponse : ResponseBase, IIngestProcessorGrokResponse
	{
		[JsonProperty("patterns")]
		public IReadOnlyDictionary<string, string> Patterns { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
