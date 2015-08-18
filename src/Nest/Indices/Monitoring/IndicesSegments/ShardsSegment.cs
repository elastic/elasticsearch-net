using Newtonsoft.Json;
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using System;
using System.Linq;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ShardsSegment.Json))]
	public class ShardsSegment
	{
		[JsonProperty(PropertyName = "num_committed_segments")]
		public int CommittedSegments { get; internal set; }

		[JsonProperty(PropertyName = "num_search_segments")]
		public int SearchSegments { get; internal set; }

		[JsonProperty(PropertyName = "routing")]
		public ShardSegmentRouting Routing { get; internal set; }

		[JsonProperty]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, Segment> Segments { get; internal set; }

		internal class Json : Json<ShardsSegment>
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