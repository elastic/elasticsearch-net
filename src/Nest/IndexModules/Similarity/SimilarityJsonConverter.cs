using System;
using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	internal class SimilarityFormatter : IJsonFormatter<ISimilarity>
	{
		public ISimilarity Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			var count = 0;
			string type = null;
			while (segmentReader.ReadIsInObject(ref count))
			{
				if (segmentReader.ReadPropertyName() == "type")
				{
					type = reader.ReadString();
					break;
				}
			}

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			// TODO: Remove the UpperInvariant and match case sensitive with AutomataDictionary
			switch (type.ToUpperInvariant())
			{
				case "BM25":
					return Deserialize<BM25Similarity>(ref segmentReader, formatterResolver);
				case "LMDIRICHLET":
					return Deserialize<LMDirichletSimilarity>(ref segmentReader, formatterResolver);
				case "DFR":
					return Deserialize<DFRSimilarity>(ref segmentReader, formatterResolver);
				case "DFI":
					return Deserialize<DFISimilarity>(ref segmentReader, formatterResolver);
				case "IB":
					return Deserialize<IBSimilarity>(ref segmentReader, formatterResolver);
				case "LMJELINEKMERCER":
					return Deserialize<LMJelinekMercerSimilarity>(ref segmentReader, formatterResolver);
				case "SCRIPTED":
					return Deserialize<ScriptedSimilarity>(ref segmentReader, formatterResolver);
				default:
					var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
					var dict = formatter.Deserialize(ref segmentReader, formatterResolver);
					return new CustomSimilarity(dict);
			}
		}

		public void Serialize(ref JsonWriter writer, ISimilarity value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		private static TSimilarity Deserialize<TSimilarity>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TSimilarity : ISimilarity
		{
			var formatter = formatterResolver.GetFormatter<TSimilarity>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
