using System;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class SourceFilterFormatter : IJsonFormatter<SourceFilter>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "includes", 0 },
			{ "excludes", 1 }
		};

		public SourceFilter Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.Null)
				return null;

			var filter = new SourceFilter();
			switch (token)
			{
				case JsonToken.String:
					filter.Includes = new[] { reader.ReadString() };
					break;
				case JsonToken.BeginArray:
					var include = formatterResolver.GetFormatter<string[]>()
						.Deserialize(ref reader, formatterResolver);
					filter.Includes = include;
					break;
				default:
					var count = 0;
					while (reader.ReadIsInObject(ref count))
					{
						var propertyName = reader.ReadPropertyNameSegmentRaw();
						if (Fields.TryGetValue(propertyName, out var value))
						{
							var includeExclude = formatterResolver.GetFormatter<string[]>()
								.Deserialize(ref reader, formatterResolver);

							switch (value)
							{
								case 0:
									filter.Includes = includeExclude;
									break;
								case 1:
									filter.Excludes = includeExclude;
									break;
							}
						}
					}
					break;
			}

			return filter;
		}

		public void Serialize(ref JsonWriter writer, SourceFilter value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
