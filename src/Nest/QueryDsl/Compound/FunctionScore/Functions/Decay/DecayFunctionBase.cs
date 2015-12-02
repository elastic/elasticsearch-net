using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(DecayFunctionJsonConverter))]
	public interface IDecayFunction : IFunctionScoreFunction
	{
		string DecayType { get; }

		Field Field { get; set; }

		[JsonProperty(PropertyName = "decay")]
		double? Decay { get; set; }

		[JsonProperty(PropertyName = "multi_value_mode")]
		MultiValueMode? MultiValueMode { get; set; }


	}
	public interface IDecayFunction<TOrigin, TScale> : IDecayFunction
	{

		[JsonProperty(PropertyName = "origin")]
		TOrigin Origin { get; set; }

		[JsonProperty(PropertyName = "scale")]
		TScale Scale { get; set; }

		[JsonProperty(PropertyName = "offset")]
		TScale Offset { get; set; }

	}


	public abstract class DecayFunctionBase<TOrigin, TScale> : FunctionScoreFunctionBase, IDecayFunction<TOrigin, TScale>
	{
		protected abstract string DecayType { get; }

		string IDecayFunction.DecayType => this.DecayType;

		public Field Field { get; set; }

		public TOrigin Origin { get; set; }

		public TScale Scale { get; set; }

		public TScale Offset { get; set; }

		public double? Decay { get; set; }

		public MultiValueMode? MultiValueMode { get; set; }
	}

	public abstract class DecayFunctionBaseDescriptor<TDescriptor, TOrigin, TScale, T>
		: FunctionScoreFunctionBaseDescriptor<TDescriptor, IDecayFunction<TOrigin, TScale>, T>, IDecayFunction<TOrigin, TScale>
		where TDescriptor : DecayFunctionBaseDescriptor<TDescriptor, TOrigin, TScale, T>, IDecayFunction<TOrigin, TScale>
		where T : class
	{
		protected abstract string DecayType { get; }

		string IDecayFunction.DecayType => this.DecayType;

		Field IDecayFunction.Field { get; set; }

		TOrigin IDecayFunction<TOrigin, TScale>.Origin { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Scale { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Offset { get; set; }

		double? IDecayFunction.Decay { get; set; }

		MultiValueMode? IDecayFunction.MultiValueMode { get; set; }

		public TDescriptor Origin(TOrigin origin) => Assign(a => a.Origin = origin);

		public TDescriptor Scale(TScale scale) => Assign(a => a.Scale = scale);

		public TDescriptor Offset(TScale offset) => Assign(a => a.Offset = offset);

		public TDescriptor Decay(double? decay) => Assign(a => a.Decay = decay);

		public TDescriptor MultiValueMode(MultiValueMode? mode) => Assign(a => a.MultiValueMode = mode);

		public TDescriptor Field(string field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);
	}

	public class DecayFunctionJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var decay = value as IDecayFunction;
			if (decay == null || decay.Field.IsConditionless()) return;

			writer.WriteStartObject();
			{
				if (decay.Filter != null)
				{
					writer.WritePropertyName("filter");
					serializer.Serialize(writer, decay.Filter);
				}
				writer.WritePropertyName(decay.DecayType);
				writer.WriteStartObject();
				{
					writer.WritePropertyName(serializer.GetConnectionSettings().Inferrer.Field(decay.Field));
					writer.WriteStartObject();
					{
						var write = WriteNumeric(writer, value as IDecayFunction<double?, double?>, serializer)
							|| WriteDate(writer, value as IDecayFunction<DateMath, TimeUnitExpression>, serializer)
							|| WriteGeo(writer, value as IDecayFunction<GeoLocation, GeoDistance>, serializer);
						if (!write) throw new Exception($"Can not write decay function json for {value.GetType().Name}");

						if (decay.Decay.HasValue)
						{
							writer.WritePropertyName("decay");
							serializer.Serialize(writer, decay.Decay.Value);
						}
					}
					writer.WriteEndObject();
					if (decay.MultiValueMode.HasValue)
					{
						writer.WritePropertyName("multi_value_mode");
						serializer.Serialize(writer, decay.MultiValueMode.Value);
					}
				}
				writer.WriteEndObject();

				if (decay.Weight.HasValue)
				{
					writer.WritePropertyName("weight");
					serializer.Serialize(writer, decay.Weight.Value);
				}

			}
			writer.WriteEndObject();
		}

		private bool WriteNumeric(JsonWriter writer, IDecayFunction<double?, double?> value, JsonSerializer serializer)
		{
			if (value == null) return false;
			writer.WritePropertyName("origin");
			serializer.Serialize(writer, value.Origin);
			writer.WritePropertyName("scale");
			serializer.Serialize(writer, value.Scale);
			if (value.Offset != null)
			{
				writer.WritePropertyName("offset");
				serializer.Serialize(writer, value.Offset);
			}
			return true;
		}

		private bool WriteDate(JsonWriter writer, IDecayFunction<DateMath, TimeUnitExpression> value, JsonSerializer serializer)
		{
			if (value == null || value.Field.IsConditionless()) return false;
			writer.WritePropertyName("origin");
			serializer.Serialize(writer, value.Origin);
			writer.WritePropertyName("scale");
			serializer.Serialize(writer, value.Scale);
			if (value.Offset != null)
			{
				writer.WritePropertyName("offset");
				serializer.Serialize(writer, value.Offset);
			}
			return true;

		}

		private bool WriteGeo(JsonWriter writer, IDecayFunction<GeoLocation, GeoDistance> value, JsonSerializer serializer)
		{
			if (value == null || value.Field.IsConditionless()) return false;
			writer.WritePropertyName("origin");
			serializer.Serialize(writer, value.Origin);
			writer.WritePropertyName("scale");
			serializer.Serialize(writer, value.Scale);
			if (value.Offset != null)
			{
				writer.WritePropertyName("offset");
				serializer.Serialize(writer, value.Offset);
			}
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}

}