using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ShardsSegment.Json))]
	public class ShardsSegment
	{
		[JsonProperty("num_committed_segments")]
		public int CommittedSegments { get; internal set; }

		[JsonProperty("num_search_segments")]
		public int SearchSegments { get; internal set; }

		[JsonProperty("routing")]
		public ShardSegmentRouting Routing { get; internal set; }

		[JsonProperty]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, Segment>))]
		public IReadOnlyDictionary<string, Segment> Segments { get; internal set; } =
			EmptyReadOnly<string, Segment>.Dictionary;

		internal class Json : JsonConverterBase<ShardsSegment>
		{
			public override void WriteJson(JsonWriter writer, ShardsSegment value, JsonSerializer serializer) { }

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
											JsonSerializer serializer)
			{
				if (reader.TokenType == JsonToken.StartArray)
				{
					var list = new List<ShardsSegment>();
					serializer.Populate(reader, list);
					return list.First();
				}

				var o = new ShardsSegment();
				serializer.Populate(reader, o);
				return o;
			}
		}
	}
}
