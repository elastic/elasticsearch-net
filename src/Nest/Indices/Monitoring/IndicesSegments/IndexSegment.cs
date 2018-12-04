using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class IndexSegment
	{
		[DataMember(Name ="shards")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, ShardsSegment>))]
		public IReadOnlyDictionary<string, ShardsSegment> Shards { get; internal set; } =
			EmptyReadOnly<string, ShardsSegment>.Dictionary;
	}
}
