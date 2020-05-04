// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	internal class ScoreFunctionJsonFormatter : IJsonFormatter<IScoreFunction>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "filter", 0 },
			{ "weight", 1 },
			{ "exp", 2 },
			{ "gauss", 2 },
			{ "linear", 2 },
			{ "random_score", 3 },
			{ "field_value_factor", 4 },
			{ "script_score", 5 }
		};

		public IScoreFunction Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			QueryContainer filter = null;
			double? weight = null;
			IScoreFunction function = null;

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (AutomataDictionary.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							var formatter = formatterResolver.GetFormatter<QueryContainer>();
							filter = formatter.Deserialize(ref reader, formatterResolver);
							break;
						case 1:
							weight = reader.ReadDouble();
							break;
						case 2:
							var innerCount = 0;
							MultiValueMode? multiValueMode = null;
							IDecayFunction decayFunction = null;
							while (reader.ReadIsInObject(ref innerCount))
							{
								var functionPropertyName = reader.ReadPropertyName();
								if (functionPropertyName == "multi_value_mode")
									multiValueMode = formatterResolver.GetFormatter<MultiValueMode>()
										.Deserialize(ref reader, formatterResolver);
								else
								{
									var name = propertyName.Utf8String();
									decayFunction = ReadDecayFunction(ref reader, name, formatterResolver);
									decayFunction.Field = functionPropertyName;
								}
							}

							if (decayFunction != null)
							{
								decayFunction.MultiValueMode = multiValueMode;
								function = decayFunction;
							}
							break;
						case 3:
							var randomScoreFormatter = formatterResolver.GetFormatter<RandomScoreFunction>();
							function = randomScoreFormatter.Deserialize(ref reader, formatterResolver);
							break;
						case 4:
							var fieldValueFormatter = formatterResolver.GetFormatter<FieldValueFactorFunction>();
							function = fieldValueFormatter.Deserialize(ref reader, formatterResolver);
							break;
						case 5:
							var scriptFormatter = formatterResolver.GetFormatter<ScriptScoreFunction>();
							function = scriptFormatter.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
			}

			if (function == null)
			{
				if (weight.HasValue)
					function = new WeightFunction();
				else
					return null;
			}

			function.Weight = weight;
			function.Filter = filter;
			return function;
		}

		public void Serialize(ref JsonWriter writer, IScoreFunction value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) return;

			var written = false;

			writer.WriteBeginObject();
			if (value.Filter != null)
			{
				writer.WritePropertyName("filter");
				var formatter = formatterResolver.GetFormatter<QueryContainer>();
				formatter.Serialize(ref writer, value.Filter, formatterResolver);
				written = true;
			}

			switch (value)
			{
				case IDecayFunction decayFunction:
					if (written)
						writer.WriteValueSeparator();

					WriteDecay(ref writer, decayFunction, formatterResolver);
					written = true;
					break;
				case IFieldValueFactorFunction fieldValueFactorFunction:
					if (written)
						writer.WriteValueSeparator();

					WriteFieldValueFactor(ref writer, fieldValueFactorFunction, formatterResolver);
					written = true;
					break;
				case IRandomScoreFunction randomScoreFunction:
					if (written)
						writer.WriteValueSeparator();

					WriteRandomScore(ref writer, randomScoreFunction, formatterResolver);
					written = true;
					break;
				case IScriptScoreFunction scriptScoreFunction:
					if (written)
						writer.WriteValueSeparator();

					WriteScriptScore(ref writer, scriptScoreFunction, formatterResolver);
					written = true;
					break;
				case IWeightFunction _:
					break;
				default:
					throw new Exception($"Can not write function score json for {value.GetType().Name}");
			}

			if (value.Weight.HasValue)
			{
				if(written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("weight");
				writer.WriteDouble(value.Weight.Value);
			}
			writer.WriteEndObject();
		}

		private static void WriteScriptScore(ref JsonWriter writer, IScriptScoreFunction value, IJsonFormatterResolver formatterResolver)
		{
			writer.WritePropertyName("script_score");
			writer.WriteBeginObject();
			writer.WritePropertyName("script");
			var scriptFormatter = formatterResolver.GetFormatter<IScript>();
			scriptFormatter.Serialize(ref writer, value?.Script, formatterResolver);
			writer.WriteEndObject();
		}

		private static void WriteRandomScore(ref JsonWriter writer, IRandomScoreFunction value, IJsonFormatterResolver formatterResolver)
		{
			writer.WritePropertyName("random_score");
			writer.WriteBeginObject();
			if (value.Seed != null)
			{
				writer.WritePropertyName("seed");
				var seedFormatter = formatterResolver.GetFormatter<Union<long, string>>();
				seedFormatter.Serialize(ref writer, value.Seed, formatterResolver);
			}

			if (value.Field != null)
			{
				if (value.Seed != null)
					writer.WriteValueSeparator();

				writer.WritePropertyName("field");
				var fieldFormatter = formatterResolver.GetFormatter<Field>();
				fieldFormatter.Serialize(ref writer, value.Field, formatterResolver);
			}
			writer.WriteEndObject();
		}

		private static void WriteFieldValueFactor(ref JsonWriter writer, IFieldValueFactorFunction value, IJsonFormatterResolver formatterResolver)
		{
			writer.WritePropertyName("field_value_factor");
			var formatter = formatterResolver.GetFormatter<IFieldValueFactorFunction>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		private void WriteDecay(ref JsonWriter writer, IDecayFunction decay, IJsonFormatterResolver formatterResolver)
		{
			writer.WritePropertyName(decay.DecayType);
			writer.WriteBeginObject();

			writer.WritePropertyName(formatterResolver.GetConnectionSettings().Inferrer.Field(decay.Field));
			writer.WriteBeginObject();

			var written = false;

			switch (decay)
			{
				case IDecayFunction<double?, double?> numericDecay:
					WriteNumericDecay(ref writer, ref written, numericDecay);
					break;
				case IDecayFunction<DateMath, Time> dateDecay:
					WriteDateDecay(ref writer, ref written, dateDecay, formatterResolver);
					break;
				case IDecayFunction<GeoLocation, Distance> geoDecay:
					WriteGeoDecay(ref writer, ref written, geoDecay, formatterResolver);
					break;
				default:
					throw new Exception($"Can not write decay function json for {decay.GetType().Name}");
			}

			if (decay.Decay.HasValue)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("decay");
				writer.WriteDouble(decay.Decay.Value);
			}

			writer.WriteEndObject();

			if (decay.MultiValueMode.HasValue)
			{
				writer.WriteValueSeparator();

				writer.WritePropertyName("multi_value_mode");
				formatterResolver.GetFormatter<MultiValueMode>()
					.Serialize(ref writer, decay.MultiValueMode.Value, formatterResolver);
			}

			writer.WriteEndObject();
		}

		private static void WriteNumericDecay(ref JsonWriter writer, ref bool written, IDecayFunction<double?, double?> value)
		{
			if (value.Origin.HasValue)
			{
				writer.WritePropertyName("origin");
				writer.WriteDouble(value.Origin.Value);
				written = true;
			}

			if (value.Scale.HasValue)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("scale");
				writer.WriteDouble(value.Scale.Value);
				written = true;
			}

			if (value.Offset != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("offset");
				writer.WriteDouble(value.Offset.Value);
			}
		}

		private static void WriteDateDecay(ref JsonWriter writer, ref bool written, IDecayFunction<DateMath, Time> value,
			IJsonFormatterResolver formatterResolver
		)
		{
			if (value == null || value.Field.IsConditionless())
				return;

			if (value.Origin != null)
			{
				writer.WritePropertyName("origin");
				var dateMathFormatter = formatterResolver.GetFormatter<DateMath>();
				dateMathFormatter.Serialize(ref writer, value.Origin, formatterResolver);

				written = true;
			}

			if (written)
				writer.WriteValueSeparator();

			writer.WritePropertyName("scale");
			var timeFormatter = formatterResolver.GetFormatter<Time>();
			timeFormatter.Serialize(ref writer, value.Scale, formatterResolver);

			written = true;

			if (value.Offset != null)
			{
				writer.WriteValueSeparator();

				writer.WritePropertyName("offset");
				timeFormatter.Serialize(ref writer, value.Offset, formatterResolver);
			}
		}

		private static void WriteGeoDecay(ref JsonWriter writer, ref bool written, IDecayFunction<GeoLocation, Distance> value,
			IJsonFormatterResolver formatterResolver
		)
		{
			if (value == null || value.Field.IsConditionless())
				return;

			written = true;

			writer.WritePropertyName("origin");
			var locationFormatter = formatterResolver.GetFormatter<GeoLocation>();
			locationFormatter.Serialize(ref writer, value.Origin, formatterResolver);
			writer.WriteValueSeparator();
			writer.WritePropertyName("scale");
			var distanceFormatter = formatterResolver.GetFormatter<Distance>();
			distanceFormatter.Serialize(ref writer, value.Scale, formatterResolver);

			if (value.Offset != null)
			{
				writer.WriteValueSeparator();
				writer.WritePropertyName("offset");
				distanceFormatter.Serialize(ref writer, value.Offset, formatterResolver);
			}
		}

		private static IDecayFunction ReadDecayFunction(ref JsonReader reader, string type, IJsonFormatterResolver formatterResolver)
		{
			var segment = reader.ReadNextBlockSegment();
			var count = 0;
			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			var subType = "numeric";

			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyName();
				if (propertyName == "origin")
				{
					switch (segmentReader.GetCurrentJsonToken())
					{
						case JsonToken.String:
							subType = "date";
							break;
						case JsonToken.BeginObject:
							subType = "geo";
							break;
					}
					break;
				}
			}

			segmentReader = new JsonReader(segment.Array, segment.Offset);

			switch (type)
			{
				case "exp":
				{
					switch (subType)
					{
						case "numeric": return Deserialize<ExponentialDecayFunction>(ref segmentReader, formatterResolver);
						case "date": return Deserialize<ExponentialDateDecayFunction>(ref segmentReader, formatterResolver);
						case "geo": return Deserialize<ExponentialGeoDecayFunction>(ref segmentReader, formatterResolver);
						default: return null;
					}
				}
				case "gauss":
				{
					switch (subType)
					{
						case "numeric": return Deserialize<GaussDecayFunction>(ref segmentReader, formatterResolver);
						case "date": return Deserialize<GaussDateDecayFunction>(ref segmentReader, formatterResolver);
						case "geo": return Deserialize<GaussGeoDecayFunction>(ref segmentReader, formatterResolver);
						default: return null;
					}
				}
				case "linear":
				{
					switch (subType)
					{
						case "numeric": return Deserialize<LinearDecayFunction>(ref segmentReader, formatterResolver);
						case "date": return Deserialize<LinearDateDecayFunction>(ref segmentReader, formatterResolver);
						case "geo": return Deserialize<LinearGeoDecayFunction>(ref segmentReader, formatterResolver);
						default: return null;
					}
				}
				default: return null;
			}
		}

		private static TDecayFunction Deserialize<TDecayFunction>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TDecayFunction : IDecayFunction
		{
			var formatter = formatterResolver.GetFormatter<TDecayFunction>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
