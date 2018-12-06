using System;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class FuzzyQueryFormatter : FieldNameQueryFormatter<FuzzyQuery, IFuzzyQuery>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "value", 0 },
			{ "fuzziness", 1 },
			{ "prefix_length", 2 },
			{ "max_expansions", 3 },
			{ "transpositions", 4 },
			{ "rewrite", 5 },
			{ "_name", 6 },
			{ "boost", 7 },
		};

		public override IFuzzyQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			var count = 0;
			IFuzzyQuery query = null;
			string name = null;
			double? boost = null;
			MultiTermQueryRewrite multiTermQueryRewrite = null;
			int? prefixLength = null;
			int? maxExpansions = null;
			bool? transpositions = null;

			while (reader.ReadIsInObject(ref count))
			{
				var field = reader.ReadPropertyName();
				// ReSharper disable once TooWideLocalVariableScope
				ArraySegment<byte> fuzzinessSegment = default;
				var valueCount = 0;
				while (reader.ReadIsInObject(ref valueCount))
				{
					var property = reader.ReadPropertyNameSegmentRaw();
					if (Fields.TryGetValue(property, out var value))
					{
						switch (value)
						{
							case 0:
							{
								var token = reader.GetCurrentJsonToken();
								switch (token)
								{
									case JsonToken.String:
										var str = reader.ReadString();
										try
										{
											// TODO: possibly nicer way of doing this than brute try?
											var dateTime = JsonSerializer.Deserialize<DateTime>(str, formatterResolver);
											query = new FuzzyDateQuery
											{
												Field = field,
												Value = dateTime
											};
										}
										catch
										{
											query = new FuzzyQuery
											{
												Field = field,
												Value = str
											};
										}
										break;
									case JsonToken.Number:
										query = new FuzzyNumericQuery
										{
											Field = field,
											Value = reader.ReadDouble()
										};
										break;
								}

								if (fuzzinessSegment != default)
								{
									var fuzzinessReader = new JsonReader(fuzzinessSegment.Array, fuzzinessSegment.Offset);
									SetFuzziness(ref fuzzinessReader, query, formatterResolver);
								}
								break;
							}
							case 1:
							{
								if (query != null)
									SetFuzziness(ref reader, query, formatterResolver);
								else
									fuzzinessSegment = reader.ReadNextBlockSegment();
								break;
							}
							case 2:
								prefixLength = reader.ReadInt32();
								break;
							case 3:
								maxExpansions = reader.ReadInt32();
								break;
							case 4:
								transpositions = reader.ReadBoolean();
								break;
							case 5:
								var rewriteFormatter = formatterResolver.GetFormatter<MultiTermQueryRewrite>();
								multiTermQueryRewrite = rewriteFormatter.Deserialize(ref reader, formatterResolver);
								break;
							case 6:
								name = reader.ReadString();
								break;
							case 7:
								boost = reader.ReadDouble();
								break;
						}
					}
				}
			}

			query.PrefixLength = prefixLength;
			query.MaxExpansions = maxExpansions;
			query.Transpositions = transpositions;
			query.Rewrite = multiTermQueryRewrite;
			query.Name = name;
			query.Boost = boost;
			return query;
		}

		private static void SetFuzziness(ref JsonReader reader, IFuzzyQuery query, IJsonFormatterResolver formatterResolver)
		{
			switch (query)
			{
				case FuzzyQuery fuzzyQuery:
					fuzzyQuery.Fuzziness = formatterResolver.GetFormatter<Fuzziness>()
						.Deserialize(ref reader, formatterResolver);
					break;
				case FuzzyDateQuery fuzzyDateQuery:
					fuzzyDateQuery.Fuzziness = formatterResolver.GetFormatter<Time>()
						.Deserialize(ref reader, formatterResolver);
					break;
				case FuzzyNumericQuery fuzzyNumericQuery:
					fuzzyNumericQuery.Fuzziness = reader.ReadDouble();
					break;
			}
		}
	}
}
