using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class TermVector
	{
		[JsonProperty("field_statistics")]
		public FieldStatistics FieldStatistics { get; internal set; }

		[JsonProperty("terms")]
		public IReadOnlyDictionary<string, TermVectorTerm> Terms { get; internal set; } =
			EmptyReadOnly<string, TermVectorTerm>.Dictionary;
	}
}
