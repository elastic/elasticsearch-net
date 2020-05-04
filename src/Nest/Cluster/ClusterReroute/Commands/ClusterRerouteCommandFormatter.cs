// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	internal class ClusterRerouteCommandFormatter : IJsonFormatter<IClusterRerouteCommand>
	{
		private static readonly AutomataDictionary Commands = new AutomataDictionary
		{
			{ "allocate_replica", 0 },
			{ "allocate_empty_primary", 1 },
			{ "allocate_stale_primary", 2 },
			{ "move", 3 },
			{ "cancel", 4 }
		};

		public IClusterRerouteCommand Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			IClusterRerouteCommand command = null;

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (Commands.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							command = Deserialize<AllocateReplicaClusterRerouteCommand>(ref reader, formatterResolver);
							break;
						case 1:
							command = Deserialize<AllocateEmptyPrimaryRerouteCommand>(ref reader, formatterResolver);
							break;
						case 2:
							command = Deserialize<AllocateStalePrimaryRerouteCommand>(ref reader, formatterResolver);
							break;
						case 3:
							command = Deserialize<MoveClusterRerouteCommand>(ref reader, formatterResolver);
							break;
						case 4:
							command = Deserialize<CancelClusterRerouteCommand>(ref reader, formatterResolver);
							break;
					}
				}
				else
					reader.ReadNext();
			}

			return command;
		}

		public void Serialize(ref JsonWriter writer, IClusterRerouteCommand value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Name);

			switch (value.Name)
			{
				case "allocate_replica":
					Serialize<IAllocateReplicaClusterRerouteCommand>(ref writer, value, formatterResolver);
					break;
				case "allocate_empty_primary":
					Serialize<IAllocateEmptyPrimaryRerouteCommand>(ref writer, value, formatterResolver);
					break;
				case "allocate_stale_primary":
					Serialize<IAllocateStalePrimaryRerouteCommand>(ref writer, value, formatterResolver);
					break;
				case "move":
					Serialize<IMoveClusterRerouteCommand>(ref writer, value, formatterResolver);
					break;
				case "cancel":
					Serialize<ICancelClusterRerouteCommand>(ref writer, value, formatterResolver);
					break;
				default:
					var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IClusterRerouteCommand>();
					formatter.Serialize(ref writer, value, formatterResolver);
					break;
			}

			writer.WriteEndObject();
		}

		private static void Serialize<TCommand>(ref JsonWriter writer, IClusterRerouteCommand value, IJsonFormatterResolver formatterResolver)
			where TCommand : class, IClusterRerouteCommand
		{
			var formatter = formatterResolver.GetFormatter<TCommand>();
			formatter.Serialize(ref writer, value as TCommand, formatterResolver);
		}

		private static TCommand Deserialize<TCommand>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TCommand : IClusterRerouteCommand
		{
			var formatter = formatterResolver.GetFormatter<TCommand>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
