/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;
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
