using System;
using Utf8Json;

namespace Nest
{
	internal class NormalizerFormatter : IJsonFormatter<INormalizer>
	{
		public INormalizer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize<CustomNormalizer>(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, INormalizer value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		private static TNormalizer Deserialize<TNormalizer>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TNormalizer : INormalizer
		{
			var formatter = formatterResolver.GetFormatter<TNormalizer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
