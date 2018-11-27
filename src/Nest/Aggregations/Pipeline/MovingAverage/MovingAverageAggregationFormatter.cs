using System;
using Elasticsearch.Net;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class MovingAverageAggregationFormatter : IJsonFormatter<IMovingAverageAggregation>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "format", 0 },
			{ "gap_policy", 1 },
			{ "minimize", 2 },
			{ "predict", 3 },
			{ "window", 4 },
			{ "settings", 5 },
			{ "model", 6 },
			{ "buckets_path", 7 }
		};

		private static readonly AutomataDictionary ModelDictionary = new AutomataDictionary
		{
			{ "linear", 0 },
			{ "simple", 1 },
			{ "ewma", 2 },
			{ "holt", 3 },
			{ "holt_winters", 4 }
		};

		public IMovingAverageAggregation Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			var aggregation = new MovingAverageAggregation();
			ArraySegment<byte> model;
			ArraySegment<byte> modelSegment;

			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (AutomataDictionary.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							aggregation.Format = reader.ReadString();
							break;
						case 1:
							aggregation.GapPolicy = formatterResolver.GetFormatter<GapPolicy?>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 2:
							aggregation.Minimize = reader.ReadBoolean();
							break;
						case 3:
							aggregation.Predict = reader.ReadInt32();
							break;
						case 4:
							aggregation.Window = reader.ReadInt32();
							break;
						case 5:
							modelSegment = reader.ReadNextBlockSegment();
							break;
						case 6:
							model = reader.ReadStringSegmentUnsafe();
							break;
						case 7:
							var path = reader.ReadString();
							if (!string.IsNullOrEmpty(path))
								aggregation.BucketsPath = new SingleBucketsPath(path);
							break;
					}
				}
			}

			if (model != default && ModelDictionary.TryGetValue(model, out var modelValue))
			{
				var modelReader = new JsonReader(modelSegment.Array, modelSegment.Offset);
				switch (modelValue)
				{
					case 0:
						aggregation.Model = formatterResolver.GetFormatter<LinearModel>()
							.Deserialize(ref modelReader, formatterResolver);
						break;
					case 1:
						aggregation.Model = formatterResolver.GetFormatter<SimpleModel>()
							.Deserialize(ref modelReader, formatterResolver);
						break;
					case 2:
						aggregation.Model = formatterResolver.GetFormatter<EwmaModel>()
							.Deserialize(ref modelReader, formatterResolver);
						break;
					case 3:
						aggregation.Model = formatterResolver.GetFormatter<HoltLinearModel>()
							.Deserialize(ref modelReader, formatterResolver);
						break;
					case 4:
						aggregation.Model = formatterResolver.GetFormatter<HoltWintersModel>()
							.Deserialize(ref modelReader, formatterResolver);
						break;
				}
			}

			return aggregation;
		}

		public void Serialize(ref JsonWriter writer, IMovingAverageAggregation value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var propertyWritten = false;

			if (value.BucketsPath != null)
			{
				writer.WritePropertyName("buckets_path");
				var formatter = formatterResolver.GetFormatter<IBucketsPath>();
				formatter.Serialize(ref writer, value.BucketsPath, formatterResolver);
				propertyWritten = true;
			}
			if (value.GapPolicy != null)
			{
				if (propertyWritten)
					writer.WriteValueSeparator();

				writer.WritePropertyName("gap_policy");
				writer.WriteString(value.GapPolicy.GetStringValue());
				propertyWritten = true;
			}
			if (!value.Format.IsNullOrEmpty())
			{
				if (propertyWritten)
					writer.WriteValueSeparator();

				writer.WritePropertyName("format");
				writer.WriteString(value.Format);
				propertyWritten = true;
			}
			if (value.Window != null)
			{
				if (propertyWritten)
					writer.WriteValueSeparator();

				writer.WritePropertyName("window");
				writer.WriteInt32(value.Window.Value);
				propertyWritten = true;
			}
			if (value.Minimize != null)
			{
				if (propertyWritten)
					writer.WriteValueSeparator();

				writer.WritePropertyName("minimize");
				writer.WriteBoolean(value.Minimize.Value);
				propertyWritten = true;
			}
			if (value.Predict != null)
			{
				if (propertyWritten)
					writer.WriteValueSeparator();

				writer.WritePropertyName("predict");
				writer.WriteInt32(value.Predict.Value);
				propertyWritten = true;
			}
			if (value.Model != null)
			{
				if (propertyWritten)
					writer.WriteValueSeparator();

				writer.WritePropertyName("model");
				writer.WriteString(value.Model.Name);
				writer.WriteValueSeparator();
				writer.WritePropertyName("settings");
				var formatter = formatterResolver.GetFormatter<IMovingAverageModel>();
				formatter.Serialize(ref writer, value.Model, formatterResolver);
			}
			writer.WriteEndObject();
		}
	}
}
