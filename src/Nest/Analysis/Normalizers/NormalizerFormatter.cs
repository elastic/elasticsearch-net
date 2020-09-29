// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;


namespace Nest
{
	internal class NormalizerFormatter : IJsonFormatter<INormalizer>
	{
		public INormalizer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize<CustomNormalizer>(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, INormalizer value, IJsonFormatterResolver formatterResolver) =>
			DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ICustomNormalizer>()
				.Serialize(ref writer, value as ICustomNormalizer, formatterResolver);

		private static TNormalizer Deserialize<TNormalizer>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TNormalizer : INormalizer
		{
			var formatter = formatterResolver.GetFormatter<TNormalizer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
