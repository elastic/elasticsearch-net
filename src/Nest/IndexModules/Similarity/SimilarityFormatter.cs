using System;
using System.Collections.Generic;
using Elasticsearch.Net;


namespace Nest
{
	internal class SimilarityFormatter : IJsonFormatter<ISimilarity>
	{
		private static readonly AutomataDictionary Similarities = new AutomataDictionary
		{
			{ "BM25", 0 },
			{ "classic", 1 },
			{ "LMDirichlet", 2 },
			{ "DFR", 3 },
			{ "DFI", 4 },
			{ "IB", 5 },
			{ "LMJelinekMercer", 6 },
			{ "scripted", 7 }
		};

		private static readonly byte[] Type = JsonWriter.GetEncodedPropertyNameWithoutQuotation("type");

		public ISimilarity Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			var count = 0;
			ArraySegment<byte> type = default;
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyNameSegmentRaw();

				if (propertyName.EqualsBytes(Type))
				{
					type = segmentReader.ReadStringSegmentUnsafe();
					break;
				}

				segmentReader.ReadNextBlock();
			}

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			if (Similarities.TryGetValue(type, out var value))
			{
				switch (value)
				{
					case 0:
						return Deserialize<BM25Similarity>(ref segmentReader, formatterResolver);
					case 1:
						return Deserialize<ClassicSimilarity>(ref segmentReader, formatterResolver);
					case 2:
						return Deserialize<LMDirichletSimilarity>(ref segmentReader, formatterResolver);
					case 3:
						return Deserialize<DFRSimilarity>(ref segmentReader, formatterResolver);
					case 4:
						return Deserialize<DFISimilarity>(ref segmentReader, formatterResolver);
					case 5:
						return Deserialize<IBSimilarity>(ref segmentReader, formatterResolver);
					case 6:
						return Deserialize<LMJelinekMercerSimilarity>(ref segmentReader, formatterResolver);
					case 7:
						return Deserialize<ScriptedSimilarity>(ref segmentReader, formatterResolver);
				}
			}

			var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
			var dict = formatter.Deserialize(ref segmentReader, formatterResolver);
			return new CustomSimilarity(dict);
		}

		public void Serialize(ref JsonWriter writer, ISimilarity value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Type)
			{
				case "BM25":
					Serialize<IBM25Similarity>(ref writer, value, formatterResolver);
					break;
				case "classic":
					Serialize<IClassicSimilarity>(ref writer, value, formatterResolver);
					break;
				case "LMDirichlet":
					Serialize<ILMDirichletSimilarity>(ref writer, value, formatterResolver);
					break;
				case "DFR":
					Serialize<IDFRSimilarity>(ref writer, value, formatterResolver);
					break;
				case "DFI":
					Serialize<IDFISimilarity>(ref writer, value, formatterResolver);
					break;
				case "IB":
					Serialize<IIBSimilarity>(ref writer, value, formatterResolver);
					break;
				case "LMJelinekMercer":
					Serialize<ILMJelinekMercerSimilarity>(ref writer, value, formatterResolver);
					break;
				case "scripted":
					Serialize<IScriptedSimilarity>(ref writer, value, formatterResolver);
					break;
				default:
					Serialize<ICustomSimilarity>(ref writer, value, formatterResolver);
					break;
			}
		}

		private static void Serialize<TSimilarity>(ref JsonWriter writer, ISimilarity value, IJsonFormatterResolver formatterResolver)
			where TSimilarity : class, ISimilarity
		{
			var formatter = formatterResolver.GetFormatter<TSimilarity>();
			formatter.Serialize(ref writer, value as TSimilarity, formatterResolver);
		}

		private static TSimilarity Deserialize<TSimilarity>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TSimilarity : ISimilarity
		{
			var formatter = formatterResolver.GetFormatter<TSimilarity>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
