// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	internal class SourceFilterFormatter : IJsonFormatter<ISourceFilter>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "includes", 0 },
			{ "excludes", 1 }
		};

		public ISourceFilter Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

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
							var includeExclude = formatterResolver.GetFormatter<Fields>()
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
						else
							reader.ReadNextBlock();
					}
					break;
			}

			return filter;
		}

		public void Serialize(ref JsonWriter writer, ISourceFilter value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ISourceFilter>()
				.Serialize(ref writer, value, formatterResolver);
		}
	}
}
