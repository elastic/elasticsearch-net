using Utf8Json;
using Utf8Json.Internal;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class ClusterRerouteCommandFormatter : IJsonFormatter<IClusterRerouteCommand>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
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
				if (AutomataDictionary.TryGetValue(propertyName, out var value))
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

			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IClusterRerouteCommand>();
			formatter.Serialize(ref writer, value, formatterResolver);

			writer.WriteEndObject();
		}

		private static TCommand Deserialize<TCommand>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TCommand : IClusterRerouteCommand
		{
			var formatter = formatterResolver.GetFormatter<TCommand>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
