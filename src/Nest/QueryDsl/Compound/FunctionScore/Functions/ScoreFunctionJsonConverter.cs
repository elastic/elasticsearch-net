using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class ScoreFunctionJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var function = value as IScoreFunction;
			if (function == null) return;

			writer.WriteStartObject();
			{
				if (function.Filter != null)
				{
					writer.WritePropertyName("filter");
					serializer.Serialize(writer, function.Filter);
				}

				var write = WriteDecay(writer, function as IDecayFunction, serializer)
					|| WriteFieldValueFactor(writer, function as IFieldValueFactorFunction, serializer)
					|| WriteRandomScore(writer, function as IRandomScoreFunction, serializer)
					|| WriteScriptScore(writer, function as IScriptScoreFunction, serializer)
					|| WriteWeightFunction(writer, function as IWeightFunction, serializer);
				if (!write) throw new Exception($"Can not write function score json for {function.GetType().Name}");

				if (function.Weight.HasValue)
				{
					writer.WritePropertyName("weight");
					serializer.Serialize(writer, function.Weight.Value);
				}

			}
			writer.WriteEndObject();
		}

		//rely on weight getting written later
		private bool WriteWeightFunction(JsonWriter writer, IWeightFunction value, JsonSerializer serializer) => value != null;

		private bool WriteScriptScore(JsonWriter writer, IScriptScoreFunction value, JsonSerializer serializer)
		{
			if (value == null) return false;
			writer.WritePropertyName("script_score");
			writer.WriteStartObject();
			{
				writer.WriteProperty(serializer, "script", value.Script);
			}
			writer.WriteEndObject();
			return true;
		}

		private bool WriteRandomScore(JsonWriter writer, IRandomScoreFunction value, JsonSerializer serializer)
		{
			if (value == null) return false;
			writer.WritePropertyName("random_score");
			writer.WriteStartObject();
			{
				writer.WriteProperty(serializer, "seed", value.Seed);
			}
			writer.WriteEndObject();
			return true;
		}
		private bool WriteFieldValueFactor(JsonWriter writer, IFieldValueFactorFunction value, JsonSerializer serializer)
		{
			if (value == null) return false;
			writer.WritePropertyName("field_value_factor");
			writer.WriteStartObject();
			{
				writer.WriteProperty(serializer, "field", value.Field);
				writer.WriteProperty(serializer, "factor", value.Factor);
				writer.WriteProperty(serializer, "missing", value.Missing);
				writer.WriteProperty(serializer, "modifier", value.Modifier);
			}
			writer.WriteEndObject();
			return true;
		}
		private bool WriteDecay(JsonWriter writer, IDecayFunction decay, JsonSerializer serializer)
		{
			if (decay == null) return false;

			writer.WritePropertyName(decay.DecayType);
			writer.WriteStartObject();
			{
				writer.WritePropertyName(serializer.GetConnectionSettings().Inferrer.Field(decay.Field));
				writer.WriteStartObject();
				{
					var write = WriteNumericDecay(writer, decay as IDecayFunction<double?, double?>, serializer)
								|| WriteDateDecay(writer, decay as IDecayFunction<DateMath, Time>, serializer)
								|| WriteGeoDecay(writer, decay as IDecayFunction<GeoLocation, Distance>, serializer);
					if (!write) throw new Exception($"Can not write decay function json for {decay.GetType().Name}");

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
			return true;

		}

		private bool WriteNumericDecay(JsonWriter writer, IDecayFunction<double?, double?> value, JsonSerializer serializer)
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

		private bool WriteDateDecay(JsonWriter writer, IDecayFunction<DateMath, Time> value, JsonSerializer serializer)
		{
			if (value == null || value.Field.IsConditionless()) return false;
			if (value.Origin != null)
			{
				writer.WritePropertyName("origin");
				serializer.Serialize(writer, value.Origin);
			}
			writer.WritePropertyName("scale");
			serializer.Serialize(writer, value.Scale);
			if (value.Offset != null)
			{
				writer.WritePropertyName("offset");
				serializer.Serialize(writer, value.Offset);
			}
			return true;

		}

		private bool WriteGeoDecay(JsonWriter writer, IDecayFunction<GeoLocation, Distance> value, JsonSerializer serializer)
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
			var jo = JObject.Load(reader);
			QueryContainer filter = jo.Property("filter")?.Value.ToObject<QueryContainer>(serializer);
			double? weight = jo.Property("weight")?.Value.ToObject<double?>(); ;
			IScoreFunction function = null;
			foreach (var prop in jo.Properties())
			{
				switch (prop.Name)
				{
					case "exp":
					case "gauss":
					case "linear":
						var properties = prop.Value.Value<JObject>().Properties().ToList();
						var fieldProp = properties.First(p => p.Name != "multi_value_mode");
						var field = fieldProp.Name;
						var f = this.ReadDecayFunction(prop.Name, fieldProp.Value.Value<JObject>(), serializer);
						f.Field = field;
						var mv = properties.FirstOrDefault(p => p.Name == "multi_value_mode")?.Value;
						if (mv != null)
							f.MultiValueMode = serializer.Deserialize<MultiValueMode>(mv.CreateReader());
						function = f;

						break;
					case "random_score":
						function = FromJson.ReadAs<RandomScoreFunction>(prop.Value.Value<JObject>().CreateReader(), null, null, serializer);
						break;
					case "field_value_factor":
						function = FromJson.ReadAs<FieldValueFactorFunction>(prop.Value.Value<JObject>().CreateReader(), null, null, serializer);
						break;
					case "script_score":
						function = FromJson.ReadAs<ScriptScoreFunction>(prop.Value.Value<JObject>().CreateReader(), null, null, serializer);
						break;
				}
			}
			if (function == null && weight.HasValue) function = new WeightFunction { Weight = weight };
			else if (function == null) return null; //throw new Exception("error deserializing function score function");
			function.Weight = weight;
			function.Filter = filter;
			return function;
		}

		private static IDictionary<string, Type> DecayTypeMapping = new Dictionary<string, Type>
		{
			{ "exp_numeric", typeof(ExponentialDecayFunction) },
			{ "exp_date", typeof(ExponentialDateDecayFunction) },
			{ "exp_geo", typeof(ExponentialGeoDecayFunction) },
			{ "gauss_numeric", typeof(GaussDecayFunction) },
			{ "gauss_date", typeof(GaussDateDecayFunction) },
			{ "gauss_geo", typeof(GaussGeoDecayFunction) },
			{ "linear_numeric", typeof(LinearDecayFunction) },
			{ "linear_date", typeof(LinearDateDecayFunction) },
			{ "linear_geo", typeof(LinearGeoDecayFunction) },
		};

		private IDecayFunction ReadDecayFunction(string type, JObject o, JsonSerializer serializer)
		{
			var origin = o.Property("origin")?.Value.Type;
			if (origin == null) return null;
			var subType = "numeric";
			switch (origin)
			{
				case JTokenType.String:
					subType = "date";
					break;
				case JTokenType.Object:
					subType = "geo";
					break;
			}
			var t = DecayTypeMapping[$"{type}_{subType}"];
			return FromJson.Read(o.CreateReader(), t, null, serializer) as IDecayFunction;
		}

	}
}
