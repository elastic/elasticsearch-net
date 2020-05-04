// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(Json))]
	public class ShardsSegment
	{
		[DataMember(Name = "num_committed_segments")]
		public int CommittedSegments { get; internal set; }

		[DataMember(Name = "routing")]
		public ShardSegmentRouting Routing { get; internal set; }

		[DataMember(Name = "num_search_segments")]
		public int SearchSegments { get; internal set; }

		[DataMember(Name = "segments")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, Segment>))]
		public IReadOnlyDictionary<string, Segment> Segments { get; internal set; } =
			EmptyReadOnly<string, Segment>.Dictionary;

		internal class Json : IJsonFormatter<ShardsSegment>
		{
			public ShardsSegment Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			{
				var formatter = DynamicObjectResolver.AllowPrivateExcludeNullCamelCase.GetFormatter<ShardsSegment>();
				ShardsSegment segment = null;

				if (reader.GetCurrentJsonToken() == JsonToken.BeginArray)
				{
					var count = 0;
					while (reader.ReadIsInArray(ref count))
					{
						if (count == 1)
							segment = formatter.Deserialize(ref reader, formatterResolver);
						else
							reader.ReadNextBlock();
					}
				}
				else
					segment = formatter.Deserialize(ref reader, formatterResolver);

				return segment;
			}

			public void Serialize(ref JsonWriter writer, ShardsSegment value, IJsonFormatterResolver formatterResolver)
			{
				var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ShardsSegment>();
				formatter.Serialize(ref writer, value, formatterResolver);
			}
		}
	}
}
