// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	/// <summary>
	/// Marker interface for alias operation
	/// </summary>
	[JsonFormatter(typeof(AliasActionFormatter))]
	public interface IAliasAction { }

	internal class AliasActionFormatter : IJsonFormatter<IAliasAction>
	{
		private static readonly AutomataDictionary Actions = new AutomataDictionary
		{
			{ "add", 0 },
			{ "remove", 1 },
			{ "remove_index", 2 },
		};

		public IAliasAction Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			var segment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(segment.Array, segment.Offset);

			segmentReader.ReadIsBeginObjectWithVerify();
			var action = segmentReader.ReadPropertyNameSegmentRaw();
			IAliasAction aliasAction = null;

			segmentReader = new JsonReader(segment.Array, segment.Offset);

			if (Actions.TryGetValue(action, out var value))
			{
				switch (value)
				{
					case 0:
						aliasAction = Deserialize<AliasAddAction>(ref segmentReader, formatterResolver);
						break;
					case 1:
						aliasAction = Deserialize<AliasRemoveAction>(ref segmentReader, formatterResolver);
						break;
					case 2:
						aliasAction = Deserialize<AliasRemoveIndexAction>(ref segmentReader, formatterResolver);
						break;
				}
			}

			return aliasAction;
		}

		public void Serialize(ref JsonWriter writer, IAliasAction value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value)
			{
				case IAliasAddAction addAction:
					Serialize(ref writer, addAction, formatterResolver);
					break;
				case IAliasRemoveAction removeAction:
					Serialize(ref writer, removeAction, formatterResolver);
					break;
				case IAliasRemoveIndexAction removeIndexAction:
					Serialize(ref writer, removeIndexAction, formatterResolver);
					break;
				default:
					writer.WriteNull();
					break;
			}
		}

		private static void Serialize<TAliasAction>(ref JsonWriter writer, TAliasAction action,
			IJsonFormatterResolver formatterResolver
		) where TAliasAction : IAliasAction
		{
			var formatter = formatterResolver.GetFormatter<TAliasAction>();
			formatter.Serialize(ref writer, action, formatterResolver);
		}

		private static TAliasAction Deserialize<TAliasAction>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TAliasAction : IAliasAction
		{
			var formatter = formatterResolver.GetFormatter<TAliasAction>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
